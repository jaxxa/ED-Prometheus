using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Shields
{
    class CompProperties_ShieldUpgrade : CompProperties
    {

        public CompProperties_ShieldUpgrade()
        {
            this.compClass = typeof(Comp_ShieldUpgrade);
        }

        public float m_PowerUsage_Multiplier = 2;
        public float m_FieldIntegrity_Multiplier = 2;
        public float m_Range_Multiplier = 2;

        public bool m_DropPodIntercept = false;
        public bool m_DropPodInterceptIFF = false;
        public bool m_SIFMode = false;

    }
}
