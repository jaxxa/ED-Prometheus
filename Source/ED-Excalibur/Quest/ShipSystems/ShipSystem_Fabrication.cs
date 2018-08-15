﻿using System;
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
           // ResearchHelper.QuestComplete("Research_ED_OmniGel");
        }
    }
}