using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    class ShipSystem_Fabrication : ShipSystem
    {
        public static ShipSystem_Fabrication instance;

        public ShipSystem_Fabrication()
        {
            ShipSystem_Fabrication.instance = this;
        }

        public override int GetMaxLevel()
        {
            return 6;
        }

        public override string Name()
        {
            return "Fabrication System";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
            if (this.CurrentLevel >= 1)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Fabrication_1_Research");
            }
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Fabrication_2_Research");
            }
            if (this.CurrentLevel >= 3)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Fabrication_3_Research");
            }
            if (this.CurrentLevel >= 4)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Fabrication_4_Research");
            }
            if (this.CurrentLevel >= 5)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Fabrication_5_Research");
            }
            if (this.CurrentLevel >= 6)
            {
                ResearchHelper.QuestComplete("ED_Prometheus_Fabrication_6_Research");
            }
        }
        
    }
}
