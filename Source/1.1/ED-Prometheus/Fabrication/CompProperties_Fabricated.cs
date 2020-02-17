using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Fabrication
{
    class CompProperties_Fabricated : CompProperties
    {

        public CompProperties_Fabricated()
        {
            this.compClass = typeof(Comp_Fabricated);
        }


        public int RequiredPower = 0;
        public int RequiredMaterials = 0;
        public int RequiredWork = 0;
        public bool PreventConstruction = false;
    }
}
