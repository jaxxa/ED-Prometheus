using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class CompProperties_Fabricated : CompProperties
    {

        public CompProperties_Fabricated()
        {
            this.compClass = typeof(Comp_Fabricated);
        }

    }
}
