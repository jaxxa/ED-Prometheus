using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest.ShipSystems
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
                ResearchHelper.QuestComplete("Research_ED_Excalibur_Fabrication_1");
            }
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("Research_ED_Excalibur_Fabrication_2");
            }
            if (this.CurrentLevel >= 3)
            {
                ResearchHelper.QuestComplete("Research_ED_Excalibur_Fabrication_3");
            }
            if (this.CurrentLevel >= 4)
            {
                ResearchHelper.QuestComplete("Research_ED_Excalibur_Fabrication_4");
            }
            if (this.CurrentLevel >= 5)
            {
                ResearchHelper.QuestComplete("Research_ED_Excalibur_Fabrication_5");
            }
            if (this.CurrentLevel >= 6)
            {
                ResearchHelper.QuestComplete("Research_ED_Excalibur_Fabrication_6");
            }
        }
        
    }
}
