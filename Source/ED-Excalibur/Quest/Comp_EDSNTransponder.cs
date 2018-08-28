using EnhancedDevelopment.Excalibur.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest
{
    [StaticConstructorOnStartup]
    class Comp_EDSNTransponder : ThingComp
    {

        private static Texture2D UI_Contact;

        static Comp_EDSNTransponder()
        {

            UI_Contact = ContentFinder<Texture2D>.Get("UI/Quest/UI_Contact", true);
        }

        public override string CompInspectStringExtra()
        {
            String _NewString = GameComponent_Excalibur_Quest.GetSingleLineResourceStatus();
            return _NewString + base.CompInspectStringExtra();
        }


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
            act.icon = UI_Contact;
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
                    Core.GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits, 50);
                    Core.GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.Power ,500);
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
