﻿using System;
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

        //public override void GameComponentUpdate()
        //{
        //    Log.Message("GC.Update");
        //    base.GameComponentUpdate();
        //}

        private string introText = "You detect a strange transmission on the Comms Console, for the past while you thought you could find a ghost of something in the static, but this time it is clear enough to make out." + Environment.NewLine + 
        "It appears to be Destress Call directed to a specific group and asking them to make contact using an encrypted system on nonstandard frequencies.One of your Researchers thinks that with a bit of work the encryption could be broken and they could come with a device to make contact." + Environment.NewLine +
        "Unlocked Research: Analyse Strange Signal" + Environment.NewLine +
        "-Needs Multi-Analyzer and High Tech Bench";

        private bool sent = false;

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            Log.Message("GC.Tick");

            if (!sent)
            {
                sent = true;

                DiaNode diaNode = new DiaNode(introText);

                DiaOption diaOption = new DiaOption("New Test Option");
                diaOption.action = delegate { Log.Message("Clicked"); };
                diaOption.resolveTree = true;
                diaNode.options.Add(diaOption);

                DiaOption diaOption2 = new DiaOption("New Test Option 2");
                diaOption2.action = delegate { Log.Message("Clicked 2"); };
                diaOption2.resolveTree = true;
                diaNode.options.Add(diaOption2);

                DiaOption diaOption3 = new DiaOption("New Test Option 3");
                diaOption3.action = delegate { Log.Message("Clicked 3"); };
                diaOption3.resolveTree = true;
                diaNode.options.Add(diaOption3);


                Find.WindowStack.Add(new Dialog_NodeTree(diaNode,false,false,"Strange Signal Detected"));

            }



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

