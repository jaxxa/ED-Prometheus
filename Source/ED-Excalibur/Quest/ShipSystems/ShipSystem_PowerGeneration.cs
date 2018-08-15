using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest.ShipSystems
{
    class ShipSystem_PowerGeneration : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 4;
        }

        public override string Name()
        {
            return "Power Generation Systems";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
           // ResearchHelper.QuestComplete("Research_ED_OmniGel");
        }
    }
}
