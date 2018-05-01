using EnhancedDevelopment.Excalibur.Power;
using EnhancedDevelopment.Excalibur.Quest.Dialog;
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
        private int m_QuestStatus = 0;

        private float m_ReservesPower = 0;
        private int m_ReservesMaterials = 0;

        private int SolarPannels = 2;
        private int MAX_SOLAR_PANNELS = 100;

        public void AddReservePower(float ammount)
        {
            this.m_ReservesPower += ammount;
        }

        public void AddReserveMaterials(int ammount)
        {
            this.m_ReservesMaterials += ammount;
        }

        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.m_QuestStatus, "m_QuestStatus");
            //throw new NotImplementedException();
        }

        public override void TickOnInterval()
        {
            Log.Message("QuestTick:" + this.m_ReservesPower.ToString());
            switch (m_QuestStatus)
            {
                case 0:
                    //TODO: Check Prerequisites - Comms Console with Power.
                    EnhancedDevelopment.Excalibur.Quest.ResearchHelper.QuestUnlock("ED_Excalibur_AnalyseStrangeSignal");
                    m_QuestStatus++;
                    this.ContactExcalibur();
                    break;

                default:

                    break;
            }
        }
        
        public void ContactExcalibur(Building contactSource = null)
        {

            Log.Message("Contacting Excalibur");

            switch (m_QuestStatus)
            {
                case 1:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_1_SignalDetection", "EDE_Dialog_1_SignalDetection".Translate()));
                    break;
                case 2:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_2_FirstContact", "EDE_Dialog_2_FirstContact".Translate()));
                    
                    Building_QuantumPowerRelay _PowerBuilding = (Building_QuantumPowerRelay)ThingMaker.MakeThing(ThingDef.Named("QuantumPowerRelay"), null);
                    List<Thing> _Things = new List<Thing>();
                    _Things.Add(_PowerBuilding);
                    
                    DropPodUtility.DropThingsNear(contactSource.Position, contactSource.Map, _Things);

                    break;
                case 3:
                    
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_3_InitialCharge", "EDE_Dialog_3_InitialCharge".Translate() + " " + this.m_ReservesPower.ToString() + " / " + "10,000"));

                    if (this.m_ReservesPower > 10000.0f)
                    {
                        m_QuestStatus++;
                    }
                    break;
                case 4:
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_4_NeedResources", "EDE_Dialog_4_NeedResources".Translate() + " Resources " + this.m_ReservesMaterials.ToString() + " / 20"));
                    
                    if (this.m_ReservesMaterials > 20 )
                    {
                        m_QuestStatus++;
                    }
                    break;
                case 5:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_5_ExecutingBurn", "EDE_Dialog_5_ExecutingBurn".Translate()));
                    break;
                case 6:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_6_ShipStabilised", "EDE_Dialog_6_ShipStabilised".Translate()));
                    break;

                default:
                    //Find.WindowStack.Add(new Dialog_Excalibur());

                    Find.WindowStack.Add(new Dialog_0_Generic("EDETestString", "EDETestString".Translate()));
                    break;
            }

        }

        public override int GetTickInterval()
        {
            return 2000;
        }
    }
}
