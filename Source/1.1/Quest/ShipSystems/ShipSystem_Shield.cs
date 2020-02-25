using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    class ShipSystem_Shield : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 3;
        }

        public override string Name()
        {
            return "Shield";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
            if (this.CurrentLevel >= 1)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Shields_1_Research");
            }
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Shields_2_Research");
            }
            if (this.CurrentLevel >= 3)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Shields_3_Research");
            }
        }
    }
}
