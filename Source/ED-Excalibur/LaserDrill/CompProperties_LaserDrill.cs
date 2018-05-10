using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.LaserDrill
{
    class CompProperties_LaserDrill : CompProperties
    {

        public CompProperties_LaserDrill()
        {
            this.compClass = typeof(Comp_LaserDrill);
        }

        public bool FillMode = false;

    }
}
