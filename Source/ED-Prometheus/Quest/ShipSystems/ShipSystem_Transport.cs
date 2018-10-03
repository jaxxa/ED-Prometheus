using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    class ShipSystem_Transport : ShipSystem
    {
        public override int GetMaxLevel()
        {
            //return 4;
            return 1;
        }

        public override string Name()
        {
            return "Transport";
        }

        public override void ApplyRequiredResearchUnlocks()
        {

            if (this.CurrentLevel >= 1)
            {
                ResearchHelper.QuestComplete("Research_ED_Prometheus_Transport_1");
            }
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("Research_ED_Prometheus_Transport_2");
            }
            if (this.CurrentLevel >= 3)
            {
                ResearchHelper.QuestComplete("Research_ED_Prometheus_Transport_3");
            }
            if (this.CurrentLevel >= 4)
            {
                ResearchHelper.QuestComplete("Research_ED_Prometheus_Transport_4");
            }
        }

        public bool IsTransporterUnlocked()
        {
            return this.CurrentLevel >= 1;
        }
    }
}
