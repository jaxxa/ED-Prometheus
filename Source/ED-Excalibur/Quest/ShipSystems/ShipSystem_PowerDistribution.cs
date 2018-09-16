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
            return "Power Distribution";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
            if (this.CurrentLevel >= 2)
            {
                ResearchHelper.QuestComplete("Research_ED_LaserSteamGeyser_Drill");
            }
            //if (this.CurrentLevel >= 3)
            //{
            //    ResearchHelper.QuestComplete("Research_ED_Power_SpaceToSpace");
            //}
            //if (this.CurrentLevel >= 4)
            //{
            //    ResearchHelper.QuestComplete("Research_ED_Power_SpaceToGround");
            //}
        }

        public bool IsShipToSurfacePowerAvalable()
        {
            return this.CurrentLevel >= 3;
        }
    }
}
