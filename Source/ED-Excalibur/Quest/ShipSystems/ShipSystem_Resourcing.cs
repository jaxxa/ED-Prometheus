using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    class ShipSystem_Resourcing : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 4;
        }

        public override string Name()
        {
            return "Resourcing";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
           // ResearchHelper.QuestComplete("Research_ED_OmniGel");
        }
    }
}
