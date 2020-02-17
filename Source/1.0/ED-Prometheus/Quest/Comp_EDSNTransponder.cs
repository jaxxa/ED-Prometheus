using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    [StaticConstructorOnStartup]
    class Comp_EDSNTransponder : ThingComp
    {

        public IntVec3 DropLocation = IntVec3.Invalid;

        private static Texture2D UI_Contact;

        static Comp_EDSNTransponder()
        {

            UI_Contact = ContentFinder<Texture2D>.Get("UI/Quest/UI_Contact", true);
        }

        public override string CompInspectStringExtra()
        {
            String _NewString = GameComponent_Prometheus_Quest.GetSingleLineResourceStatus();
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
            act.action = () => GameComponent_Prometheus.Instance.Comp_Quest.ContactPrometheus(this.parent as Building);
            act.icon = UI_Contact;
            act.defaultLabel = "Contact Prometheus";
            act.defaultDesc = "Contact Prometheus";
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

                    if (KeyBindingDefOf.ModifierIncrement_10x.IsDown || KeyBindingDefOf.ModifierIncrement_100x.IsDown)
                    {                    
                        Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, 500);
                        Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, 5000);
                        Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, 10);

                    }
                    else
                    {
                        Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, 50);
                        Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, 500);
                        Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, 1);
                    }

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

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look<IntVec3>(ref this.DropLocation, "DropLocation");
        }
    }
}
