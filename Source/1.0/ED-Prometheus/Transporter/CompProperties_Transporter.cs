using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Transporter
{
    class CompProperties_Transporter : CompProperties
    {
        public CompProperties_Transporter()
        {
            this.compClass = typeof(Comp_Transporter);
        }

    }
}
