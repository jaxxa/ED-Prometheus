using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EnhancedDevelopment.Prometheus.NanoShields;
using HarmonyLib;
using Verse;

namespace EnhancedDevelopment.Prometheus.Patch.Patches
{
    class PatchThingWithComps_Nano : Patch
    {
        protected override void ApplyPatch(Harmony harmony = null)
        {
            MethodInfo _ThingWithComps_InitializeComps = typeof(Verse.ThingWithComps).GetMethod("InitializeComps", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_ThingWithComps_InitializeComps, "_ThingWithComps_InitializeComps", true);

            //Get the Prefix Patch
            MethodInfo _InitializeCompsPrefix = typeof(PatchThingWithComps_Nano).GetMethod("InitializeCompsPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_InitializeCompsPrefix, "_AddCompPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_ThingWithComps_InitializeComps, new HarmonyMethod(_InitializeCompsPrefix), null);

        }

        protected override string PatchDescription()
        {
            return "PatchThingWithComps_Nano";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static bool InitializeCompsPrefix(ThingWithComps __instance)
        {
            //Only add to Pawns that dont have the come in their def already.
            if (__instance is Pawn &&
                !__instance.def.comps.Any(x => x is CompProperties_NanoShield))
            {
                CompProperties_NanoShield _CompProp = new CompProperties_NanoShield();
                __instance.def.comps.Add(_CompProp);
            }

            return true;
        }

    }
}
