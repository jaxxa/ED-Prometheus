using EnhancedDevelopment.Excalibur.Fabrication;
using EnhancedDevelopment.Excalibur.Power;
using EnhancedDevelopment.Excalibur.Quest;
using EnhancedDevelopment.Excalibur.Quest.Dialog;
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

        public GameComponent_Excalibur_Quest() : base()
        {
           // ResearchHelper.UpdateResearch();
        }


        public int m_QuestStatus = 0;

        private float m_ReservesPower = 0;
        private int m_ReservesMaterials = 0;

        public void AddReservePower(float ammount)
        {
            this.m_ReservesPower += ammount;
        }

        public float RequestReservePower(float ammount)
        {
            if (this.m_ReservesPower >= ammount)
            {
                this.m_ReservesPower -= ammount;
                return ammount;
            }
            else
            {
                float _Temp = this.m_ReservesPower;
                this.m_ReservesPower -= _Temp;
                return _Temp;
            }
        }


        public int GetReservePower()
        {
            return (int)this.m_ReservesPower;
        }

        public void AddReserveMaterials(int ammount)
        {
            this.m_ReservesMaterials += ammount;
        }

        public int GetReserveMaterials()
        {
            return this.m_ReservesMaterials;
        }

        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.m_QuestStatus, "m_QuestStatus");
            //throw new NotImplementedException();
        }

        public override void TickOnInterval()
        {
            //Log.Message("QuestTick:" + this.m_ReservesPower.ToString());
            switch (m_QuestStatus)
            {
                case 0:

                    if (CommsConsoleUtility.PlayerHasPoweredCommsConsole())
                    {
                        m_QuestStatus++;
                        this.ContactExcalibur();
                    }
                    break;

                default:

                    break;
            }


            if (this._Resources.Any())
            {
                int _ResourceStackSizeAdded = 0;

                this._Resources.ForEach(r =>
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

            this._Resources.Clear();
        }

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

                    if (this.m_ReservesPower >= Mod_EDExcalibur.Settings.Quest.InitialShipSetup_PowerRequired)
                    {
                        m_QuestStatus++;
                        this.ContactExcalibur();
                    }
                    else
                    {
                        Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_3_InitialCharge".Translate(), String.Format("EDE_Dialog_3_InitialCharge".Translate(), this.m_ReservesPower.ToString(), Mod_EDExcalibur.Settings.Quest.InitialShipSetup_PowerRequired.ToString())));
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
                        Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_Title_4_NeedResources".Translate(), String.Format("EDE_Dialog_4_NeedResources".Translate(), this.m_ReservesMaterials.ToString(),Mod_EDExcalibur.Settings.Quest.InitialShipSetup_ResourcesRequired.ToString())));
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

                    break;

                default:
                    //Find.WindowStack.Add(new Dialog_Excalibur());

                    Find.WindowStack.Add(new Dialog_0_Generic("EDETestString", "EDETestString".Translate()));
                    m_QuestStatus = 1;
                    break;
            }

            ResearchHelper.UpdateResearch();
        }

        public override int GetTickInterval()
        {
            return 2000;
        }


        private List<ResourceUnit> _Resources = new List<ResourceUnit>();

        public void TagMaterialsForTransport(ResourceUnit resource)
        {
            if (!_Resources.Contains(resource))
            {
                _Resources.Add(resource);
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();

            ResearchHelper.UpdateResearch();
        }

    }
}
