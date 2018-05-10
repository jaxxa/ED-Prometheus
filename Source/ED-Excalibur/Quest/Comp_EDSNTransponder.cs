using EnhancedDevelopment.Excalibur.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest
{
    class Comp_EDSNTransponder : ThingComp
    {
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            //   return base.CompGetGizmosExtra();

            foreach (var g in base.CompGetGizmosExtra())
            {
                yield return g;
            }

            Command_Action act = new Command_Action();
            //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
            act.action = () => GameComponent_Excalibur.Instance.Quest.ContactExcalibur(this.parent as Building);
            //act.icon = UI_DIRECT_ON;
            act.defaultLabel = "Contact Excalibur";
            act.defaultDesc = "Contact Excalibur";
            act.activateSound = SoundDef.Named("Click");
            //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
            //act.groupKey = 689736;
            yield return act;
        }

    }
}
