using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest.ShipSystems
{
    class ShipSystem_Computer : ShipSystem
    {
        public override string Name()
        {
            return "Computer";
        }

        public override int GetMaxSystemStatus()
        {
            return 10;
        }

        protected override void ApplyResearchUnlocks()
        {
            Log.Message("Unlocking Computer");
            ResearchHelper.QuestComplete("Research_ED_OmniGel");


        }
    }
}
