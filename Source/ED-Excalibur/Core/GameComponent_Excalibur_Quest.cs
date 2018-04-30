using EnhancedDevelopment.Excalibur.Quest.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Quest : GameComponent_BaseClass
    {
        //----------------Quest-----------------------
        private int m_QuestStatus = 0;

        private int m_ReservesPower = 0;
        private int m_ReservesMaterials = 0;

        private int SolarPannels = 2;
        private int MAX_SOLAR_PANNELS = 100;



        public override void ExposeData()
        {
            //throw new NotImplementedException();
        }

        public override void Tick()
        {
            int currentTick = Find.TickManager.TicksGame;
            if (currentTick % 2000 != 0)
            {
                return;
            }

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
        
        public void ContactExcalibur()
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
                    break;
                case 3:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_3_InitialCharge", "EDE_Dialog_3_InitialCharges".Translate()));
                    break;
                case 4:
                    m_QuestStatus++;
                    Find.WindowStack.Add(new Dialog_0_Generic("EDE_Dialog_4_NeedResources", "EDE_Dialog_4_NeedResources".Translate()));
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

    }
}
