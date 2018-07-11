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
            act.action = () => GameComponent_Excalibur.Instance.Comp_Quest.ContactExcalibur(this.parent as Building);
            //act.icon = UI_DIRECT_ON;
            act.defaultLabel = "Contact Excalibur";
            act.defaultDesc = "Contact Excalibur";
            act.activateSound = SoundDef.Named("Click");
            //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
            //act.groupKey = 689736;
            yield return act;

            if (DebugSettings.godMode)
            {
                Command_Action act2 = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act2.action = () =>
                {
                    Core.GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(100);
                    Core.GameComponent_Excalibur.Instance.Comp_Quest.AddReservePower(500);
                };
                //act.icon = UI_DIRECT_ON;
                act2.defaultLabel = "Debug Resources";
                act2.defaultDesc = "Debug Resources";
                act2.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act2;
            }
        }
    }
}
