using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Excalibur
{
    static class ResearchHelper
    {
        public static void QuestUnlock(string researchName)
        {
            ResearchProjectDef _Quest = DefDatabase<ResearchProjectDef>.GetNamed(researchName);
            _Quest.prerequisites.Remove(_Quest);
        }



    }
}
