using EnhancedDevelopment.Excalibur.Excalibur.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur
{
    class GameComponent_Excalibur : Verse.GameComponent
    {

        #region Variables


        private static int m_QuestStatus = 0;



        //--Nano Shields
        //--Saved
        public static int ChargeLevelCurrent = 200;

        //--Not Saved
        public static int ChargeLevelMax = 1000;

        //public static GameComponent_QuantumShield GameComp;
        #endregion

        public GameComponent_Excalibur(Game game)
        {
            // GameComponent_QuantumShield.GameComp = this;
        }

        //public override void GameComponentUpdate()
        //{
        //    Log.Message("GC.Update");
        //    base.GameComponentUpdate();
        //}

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            Log.Message("GC.Tick");

            this.NanoShieldTick();
        }
        
        public static void ContactExcalibur()
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

         //   Find.WindowStack.Add(new Dialog_2_FirstContact());



        }
        
        #region Nano Shields

        private void NanoShieldTick()
        {

            //Only Run every 20 Ticks.
            int currentTick = Find.TickManager.TicksGame;
            if (currentTick % 20 != 0)
            {
                return;
            }

            GameComponent_Excalibur.ReturnCharge(1);
            //Log.Message("GameCompTick");

        }

        public static int RequestCharge(int chargeToRequest)
        {
            if (GameComponent_Excalibur.ChargeLevelCurrent > chargeToRequest)
            {
                GameComponent_Excalibur.ChargeLevelCurrent -= chargeToRequest;
                return chargeToRequest;
            }
            else
            {
                int _Temp = GameComponent_Excalibur.ChargeLevelCurrent;
                GameComponent_Excalibur.ChargeLevelCurrent = 0;
                return _Temp;
            }
        }

        public static void ReturnCharge(int chargeLevel)
        {
            GameComponent_Excalibur.ChargeLevelCurrent += chargeLevel;

            if (GameComponent_Excalibur.ChargeLevelCurrent > GameComponent_Excalibur.ChargeLevelMax)
            {
                GameComponent_Excalibur.ChargeLevelCurrent = GameComponent_Excalibur.ChargeLevelMax;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref GameComponent_Excalibur.ChargeLevelCurrent, "ChargeLevelCurrent");
        }

        public static string GetInspectStringStatus()
        {
            return "Global Quantum Charge: " + GameComponent_Excalibur.ChargeLevelCurrent.ToString() + " / " + GameComponent_Excalibur.ChargeLevelMax.ToString();
        }


        #endregion //Nano Shields



    }
}

