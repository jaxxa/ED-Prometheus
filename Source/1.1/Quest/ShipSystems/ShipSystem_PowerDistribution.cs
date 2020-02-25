using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    class ShipSystem_PowerDistribution : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 5;
        }

        public override string Name()
        {
            return "Power Distribution";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
            if (this.CurrentLevel >= 1)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_PowerDistribution_1_Research");
            }
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_PowerDistribution_2_Research");
            }
            if (this.CurrentLevel >= 3)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_PowerDistribution_3_Research");
            }
            if (this.CurrentLevel >= 4)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_PowerDistribution_4_Research");
            }
            if (this.CurrentLevel >= 5)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_PowerDistribution_5_Research");
            }
        }

        public bool IsShipToSurfacePowerAvalable()
        {
            return this.CurrentLevel >= 3;
        }
    }
}
