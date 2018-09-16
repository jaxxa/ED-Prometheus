using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest.ShipSystems
{
    class ShipSystem_Transport : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 4;
        }

        public override string Name()
        {
            return "Transport";
        }

        public override void ApplyRequiredResearchUnlocks()
        {

        }

        public bool IsTransporterUnlocked()
        {
            return this.CurrentLevel >= 1;
        }
    }
}
