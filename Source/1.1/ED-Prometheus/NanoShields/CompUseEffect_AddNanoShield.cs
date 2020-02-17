using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.NanoShields
{
    class CompUseEffect_AddNanoShield : CompUseEffect
    {

        public override void DoEffect(Pawn usedBy)
        {

            CompNanoShield _ShieldComp = usedBy.TryGetComp<CompNanoShield>();

            if (!_ShieldComp.NanoShieldActive)
            {
                _ShieldComp.NanoShieldActive = true;


            }

        }

        public override bool CanBeUsedBy(Pawn p, out string failReason)
        {

            CompNanoShield _ShieldComp = p.TryGetComp<CompNanoShield>();

            if (_ShieldComp.NanoShieldActive)
            {

                failReason = "Already has Shields";

                return false;

            }

            return base.CanBeUsedBy(p, out failReason);
        }

    }
}
