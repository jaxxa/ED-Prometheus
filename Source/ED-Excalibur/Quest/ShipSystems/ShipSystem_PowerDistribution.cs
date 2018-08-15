using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest.ShipSystems
{
    class ShipSystem_PowerDistribution : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 5;
        }

        public override string Name()
        {
            return "Power Distribution Systems";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
           // ResearchHelper.QuestComplete("Research_ED_OmniGel");
        }
    }
}
