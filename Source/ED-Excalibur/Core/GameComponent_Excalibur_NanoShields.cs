using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Core
{
    class GameComponent_Prometheus_NanoShields : GameComponent_BaseClass
    {
        //----------------Nano Shields----------------
        //Saved
        public int ChargeLevelCurrent = 200;

        //--Not Saved
        public int ChargeLevelMax = 1000;


        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.ChargeLevelCurrent, "ChargeLevelCurrent");
        }

        public override void TickOnInterval()
        {
            GameComponent_Prometheus.Instance.Comp_Shields.ReturnCharge(1);
            //Log.Message("GameCompTick");
        }

        public int RequestCharge(int chargeToRequest)
        {
            if (this.ChargeLevelCurrent > chargeToRequest)
            {
                this.ChargeLevelCurrent -= chargeToRequest;
                return chargeToRequest;
            }
            else
            {
                int _Temp = this.ChargeLevelCurrent;
                this.ChargeLevelCurrent = 0;
                return _Temp;
            }
        }

        public void ReturnCharge(int chargeLevel)
        {
            this.ChargeLevelCurrent += chargeLevel;

            if (this.ChargeLevelCurrent > this.ChargeLevelMax)
            {
                this.ChargeLevelCurrent = this.ChargeLevelMax;
            }
        }

        public string GetInspectStringStatus()
        {
            return "Global Quantum Charge: " + this.ChargeLevelCurrent.ToString() + " / " + this.ChargeLevelMax.ToString();
        }

        public override int GetTickInterval()
        {
            return 20;
        }
    }
}
