using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.NanoShields
{
    class GameComponent_NanoShield : Verse.GameComponent
    {

        #region Variables
        //--Saved
        public static int ChargeLevelCurrent = 200;

        //--Not Saved
        public static int ChargeLevelMax = 1000;

        //public static GameComponent_QuantumShield GameComp;
        #endregion

        public GameComponent_NanoShield(Game game)
        {
            // GameComponent_QuantumShield.GameComp = this;
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            //Only Run every 20 Ticks.
            int currentTick = Find.TickManager.TicksGame;
            if (currentTick % 20 != 0)
            {
                return;
            }

            GameComponent_NanoShield.ReturnCharge(1);
            //Log.Message("GameCompTick");

        }

        public static int RequestCharge(int chargeToRequest)
        {
            if (GameComponent_NanoShield.ChargeLevelCurrent > chargeToRequest)
            {
                GameComponent_NanoShield.ChargeLevelCurrent -= chargeToRequest;
                return chargeToRequest;
            }
            else
            {
                int _Temp = GameComponent_NanoShield.ChargeLevelCurrent;
                GameComponent_NanoShield.ChargeLevelCurrent = 0;
                return _Temp;
            }
        }

        public static void ReturnCharge(int chargeLevel)
        {
            GameComponent_NanoShield.ChargeLevelCurrent += chargeLevel;

            if (GameComponent_NanoShield.ChargeLevelCurrent > GameComponent_NanoShield.ChargeLevelMax)
            {
                GameComponent_NanoShield.ChargeLevelCurrent = GameComponent_NanoShield.ChargeLevelMax;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref GameComponent_NanoShield.ChargeLevelCurrent, "ChargeLevelCurrent");
        }

        public static string GetInspectStringStatus()
        {
            return "Global Quantum Charge: " + GameComponent_NanoShield.ChargeLevelCurrent.ToString() + " / " + GameComponent_NanoShield.ChargeLevelMax.ToString();
        }
    }
}

