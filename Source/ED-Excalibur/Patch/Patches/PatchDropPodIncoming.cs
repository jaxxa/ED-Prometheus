using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Verse;
using Harmony;
using UnityEngine;
using EnhancedDevelopment.Excalibur.Shields;
using RimWorld;

namespace EnhancedDevelopment.Excalibur.Patch.Patches
{
    class PatchDropPodIncoming : Patch
    {

        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {

            this.ApplyImpactPatch(harmony);

        }

        protected override string PatchDescription()
        {
            return "PatchDropPodIncoming";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
            //return Mod_EnhancedOptions.Settings.Plant24HEnabled;
        }
        
        private void ApplyImpactPatch(HarmonyInstance harmony)
        {

            //Get the Launch Method
            //Type[] _TypeArray = new Type[] { typeof(Verse.Thing), typeof(Vector3), typeof(LocalTargetInfo), typeof(LocalTargetInfo), typeof(ProjectileHitFlags), typeof(Thing), typeof(ThingDef) };
            MethodInfo _DropPodIncomingImpact = typeof(DropPodIncoming).GetMethod("Impact", BindingFlags.Instance | BindingFlags.NonPublic);
            Patcher.LogNULL(_DropPodIncomingImpact, "_DropPodIncomingImpact");

            //Get the Launch Prefix Patch
            MethodInfo _DropPodIncomingImpactPrefix = typeof(PatchDropPodIncoming).GetMethod("ImpactPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_DropPodIncomingImpactPrefix, "_DropPodIncomingImpactPrefix");

            //Apply the Prefix Patches
            harmony.Patch(_DropPodIncomingImpact, new HarmonyMethod(_DropPodIncomingImpactPrefix), null);
        }

        public static Boolean ImpactPrefix(ref DropPodIncoming __instance)
        {
            //Log.Message("Impact");


            //This looks to include the Player Faction as Bring Friendly to Itself.
            bool _Hostile = __instance.Contents.innerContainer.Any(x => x.Faction.HostileTo(Faction.OfPlayer));

            if (__instance.Map.GetComponent<ShieldManagerMapComp>().WillDropPodBeIntercepted(__instance, _Hostile))
            {
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Vector3 loc = __instance.Position.ToVector3Shifted() + Gen.RandomHorizontalVector(1f);
                        MoteMaker.ThrowDustPuff(loc, __instance.Map, 1.2f);
                    }
                    MoteMaker.ThrowLightningGlow(__instance.Position.ToVector3Shifted(), __instance.Map, 2f);

                    GenExplosion.DoExplosion(__instance.Position, __instance.Map, 2, DamageDefOf.Burn, __instance);
                    __instance.Destroy();

                    //Retuen False so the origional method is not executed.
                    return false;
                }
            }
            return true;
        }
    }
}