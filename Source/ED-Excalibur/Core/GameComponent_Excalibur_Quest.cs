using EnhancedDevelopment.Excalibur.Fabrication;
using EnhancedDevelopment.Excalibur.Power;
using EnhancedDevelopment.Excalibur.Quest;
using EnhancedDevelopment.Excalibur.Quest.Dialog;
using EnhancedDevelopment.Excalibur.Quest.ShipSystems;
using EnhancedDevelopment.Excalibur.Settings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Quest : GameComponent_BaseClass
    {

        public enum EnumResourceType
        {
            Power,
            ResourceUnits,
            NanoMaterials,
            DropPods,
            UtilityDrones,
            SolarCells
        }

        #region Constructor

        public GameComponent_Excalibur_Quest() : base()
        {

            this.m_ResourcesStored.Add(EnumResourceType.Power, 0);
            this.m_ResourcesStored.Add(EnumResourceType.ResourceUnits, 0);
            this.m_ResourcesStored.Add(EnumResourceType.NanoMaterials, 0);
            this.m_ResourcesStored.Add(EnumResourceType.DropPods, 0);
            this.m_ResourcesStored.Add(EnumResourceType.UtilityDrones, 0);
            this.m_ResourcesStored.Add(EnumResourceType.SolarCells, 0);

            this.m_ShipSystems.Add(new ShipSystem_Fabrication());
            this.m_ShipSystems.Add(new ShipSystem_PowerDistribution());
            this.m_ShipSystems.Add(new ShipSystem_PowerGeneration());
            this.m_ShipSystems.Add(new ShipSystem_Regeneration());
            this.m_ShipSystems.Add(new ShipSystem_Resourcing());
            this.m_ShipSystems.Add(new ShipSystem_Shield());
            this.m_ShipSystems.Add(new ShipSystem_Tactical());
            this.m_ShipSystems.Add(new ShipSystem_Transport());


        }

        #endregion //Constructor

        #region Fields

        public int m_QuestStatus = 0;
        private int m_ReservesMaterials = 0;

        private List<ResourceUnit> m_ResourcesToTransport = new List<ResourceUnit>();

        #endregion //Fields

        #region Overrides

        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.m_QuestStatus, "m_QuestStatus");

            this.m_ResourcesStored.ToList().ForEach(x =>
            {
                //Log.Message("Scribe Resources " + x.ToString());

                int _Temp = x.Value;
                Scribe_Values.Look<int>(ref _Temp, "m_ResourcesStored_" + x.Key.ToString());
                this.m_ResourcesStored[x.Key] = _Temp;
            }
             );

            this.m_ShipSystems.ForEach(s => s.ExposeData());

            //throw new NotImplementedException();
        }

        public override void TickOnInterval()
        {
            switch (m_QuestStatus)
            {
                case 0:

                    if (CommsConsoleUtility.PlayerHasPoweredCommsConsole())
                    {
                        m_QuestStatus++;
                        this.ContactExcalibur();
                    }
                    break;
                case 7:

                    int _NanoMaterialPowerRequiredToBuild = 100;
                    int _NanoMaterialResourceUnitsRequiredToBuild = 10;

                    if (this.NanoMaterials < this.NanoMaterialsTarget && this.m_ResourcesStored[EnumResourceType.Power] >= _NanoMaterialPowerRequiredToBuild && this.m_ReservesMaterials >= _NanoMaterialResourceUnitsRequiredToBuild)
                    {
                        this.NanoMaterials += 100;
                        this.ResourceRequestReserve(EnumResourceType.Power, _NanoMaterialPowerRequiredToBuild);
                        this.RequestReserveMaterials(_NanoMaterialResourceUnitsRequiredToBuild);
                    }

                    break;
                default:

                    break;
            }


            if (this.m_ResourcesToTransport.Any())
            {
                int _ResourceStackSizeAdded = 0;

                this.m_ResourcesToTransport.ForEach(r =>
                {
                    if (!r.Destroyed)
                    {
                        _ResourceStackSizeAdded += r.stackCount;
                        GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(r.stackCount);

                        Transporter.Comp_Transporter.DisplayTransportEffect(r);

                        IntVec3 _Position = r.Position;
                        Map _Map = r.Map;

                        r.Destroy(DestroyMode.Vanish);

                        // Tell the MapDrawer that here is something thats changed
                        _Map.mapDrawer.MapMeshDirty(_Position, MapMeshFlag.Things, true, false);
                    }
                }
                );

                Messages.Message("Transported " + _ResourceStackSizeAdded.ToString() + " resources.", MessageTypeDefOf.TaskCompletion);

            }

            this.m_ResourcesToTransport.Clear();
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();


            this.UpdateAllResearch();
        }

        public override int GetTickInterval()
        {
            return 2000;
        }

        #endregion //Overrides

        #region Resourcing

        private Dictionary<EnumResourceType, int> m_ResourcesStored = new Dictionary<EnumResourceType, int>();


        public int ResourceGetReserveStatus(EnumResourceType resourceType)
        {
            return this.m_ResourcesStored[resourceType];
        }

        public void ResourceAddToReserves(EnumResourceType resourceType, int ammount)
        {
            this.m_ResourcesStored[resourceType] = this.m_ResourcesStored[resourceType] + ammount;
        }

        public int ResourceRequestReserve(EnumResourceType resourceType, int ammount)
        {
            if (this.m_ResourcesStored[resourceType] >= ammount)
            {
                this.m_ResourcesStored[resourceType] -= ammount;
                return ammount;
            }
            else
            {
                int _Temp = this.m_ResourcesStored[resourceType];
                this.m_ResourcesStored[resourceType] -= _Temp;
                return _Temp;
            }
        }
   

        //RU

        public int GetReserveMaterials()
        {
            return this.m_ReservesMaterials;
        }

        public void AddReserveMaterials(int ammount)
        {
            this.m_ReservesMaterials += ammount;
        }

        public float RequestReserveMaterials(int ammount)
        {
            if (this.m_ReservesMaterials >= ammount)
            {
                this.m_ReservesMaterials -= ammount;
                return ammount;
            }
            else
            {
                int _Temp = this.m_ReservesMaterials;
                this.m_ReservesMaterials -= _Temp;
                return _Temp;
            }
        }

        //Nano Materials

        public int NanoMaterials = 100;
        public int NanoMaterialsTarget = 100;


        #endregion //Resourcing

        #region Tagging RU

        public void TagMaterialsForTransport(ResourceUnit resource)
        {
            if (!this.m_ResourcesToTransport.Contains(resource))
            {
                this.m_ResourcesToTransport.Add(resource);
            }
        }

        #endregion

        #region QuestCommunication

        public void ContactExcalibur(Building contactSource = null)
        {

            Log.Message("Contacting Excalibur");

            //Updating Quest Status


            //Dsiplaying Message
            switch (m_QuestStatus)
            {
                case 0: //Debug starting quest
                    m_QuestStatus++;

                    break;
                case 1: //Signal Detection
                    m_QuestStatus++;

                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_1_SignalDetection".Translate(), "EDE_Dialog_1_SignalDetection".Translate()));
                    break;

                case 2: //Decoded 
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_2_FirstContact".Translate(), "EDE_Dialog_2_FirstContact".Translate()));

                    Building_QuantumPowerRelay _PowerBuilding = (Building_QuantumPowerRelay)ThingMaker.MakeThing(ThingDef.Named("QuantumPowerRelay"), null);
                    List<Thing> _Things = new List<Thing>();
                    _Things.Add(_PowerBuilding);

                    DropPodUtility.DropThingsNear(contactSource.Position, contactSource.Map, _Things);

                    break;
                case 3: //Charging

                    if (this.m_ResourcesStored[EnumResourceType.Power] >= Mod_EDExcalibur.Settings.Quest.InitialShipSetup_PowerRequired)
                    {
                        m_QuestStatus++;
                        this.ContactExcalibur();
                    }
                    else
                    {
                        Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_3_InitialCharge".Translate(), String.Format("EDE_Dialog_3_InitialCharge".Translate(), this.m_ResourcesStored[EnumResourceType.Power].ToString(), Mod_EDExcalibur.Settings.Quest.InitialShipSetup_PowerRequired.ToString())));
                    }

                    break;
                case 4:

                    if (this.m_ReservesMaterials >= Mod_EDExcalibur.Settings.Quest.InitialShipSetup_ResourcesRequired)
                    {
                        m_QuestStatus++;
                        this.ContactExcalibur();
                    }
                    else
                    {
                        Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_4_NeedResources".Translate(), String.Format("EDE_Dialog_4_NeedResources".Translate(), this.m_ReservesMaterials.ToString(), Mod_EDExcalibur.Settings.Quest.InitialShipSetup_ResourcesRequired.ToString())));
                    }

                    break;
                case 5:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_5_ExecutingBurn".Translate(), "EDE_Dialog_5_ExecutingBurn".Translate()));
                    break;
                case 6:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_6_ShipStabilised".Translate(), "EDE_Dialog_6_ShipStabilised".Translate()));
                    break;

                case 7:
                    //Long Term Contact

                    Find.WindowStack.Add(new Dialog_Excalibur());
                    break;

                default:
                    //Find.WindowStack.Add(new Dialog_Excalibur());

                    Find.WindowStack.Add(new Dialog_0_Generic("EDETestString", "EDETestString".Translate()));
                    m_QuestStatus = 1;
                    break;
            }
            this.UpdateAllResearch();
        }

        #endregion

        #region Ship Status

        public List<ShipSystem> m_ShipSystems = new List<ShipSystem>();

        #endregion

        public void UpdateAllResearch()
        {
            ResearchHelper.UpdateQuestStatusResearch();
            Core.GameComponent_Excalibur.Instance.Comp_Quest.m_ShipSystems.ForEach(s => s.ApplyRequiredResearchUnlocks());
        }
    }
}
