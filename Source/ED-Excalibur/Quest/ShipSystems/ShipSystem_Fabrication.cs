using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest.ShipSystems
{
    class ShipSystem_Fabrication : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 6;
        }

        public override string Name()
        {
            return "Fabrication Systems";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
            if (this.CurrentLevel >= 1)
            {
                //ResearchHelper.QuestComplete("Research_ED_ShieldGenerators");
            }
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("Research_ED_OmniGel");
            }
            if (this.CurrentLevel >= 3)
            {
                ResearchHelper.QuestComplete("Research_ED_Replicator_MK1");
            }
            if (this.CurrentLevel >= 4)
            {
                ResearchHelper.QuestComplete("Research_ED_Replicator_MK2");
            }
            if (this.CurrentLevel >= 5)
            {
                ResearchHelper.QuestComplete("Research_ED_Replicator_MK3");
            }
            if (this.CurrentLevel >= 6)
            {
                ResearchHelper.QuestComplete("Research_ED_MolecularReinforcementCompressor");
            }
        }
    }
}
