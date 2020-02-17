using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    class CompUseEffect_ContactPrometheus : CompUseEffect
    {
        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);
            //base.parent.GetComp<Comp_EDSNTransponder>().StartWick(null);

            GameComponent_Prometheus.Instance.Comp_Quest.ContactPrometheus(this.parent as Thing);
        }

    }
}
