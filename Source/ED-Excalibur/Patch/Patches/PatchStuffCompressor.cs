using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using RimWorld;
using Verse;

namespace EnhancedDevelopment.Excalibur.Patch.Patches
{
    class PatchStuffCompressor : Patch
    {
        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {


            DefDatabase<ThingDef>.AllDefs.ToList().ForEach(x =>
            {
                Fabrication.CompProperties_CompressedStuff _CompressedStuffCompPropeties = x.GetCompProperties<Fabrication.CompProperties_CompressedStuff>();

                if (_CompressedStuffCompPropeties != null)
                {
                    Log.Message("Compressing " + x.defName);
                    //x.statBases.ForEach(y => Log.Message(y.stat.defName));

                    //Log.Message("HP" + x.GetStatValueAbstract(StatDefOf.MaxHitPoints, x));
                    //x.SetStatBaseValue(StatDefOf.MaxHitPoints, 9954);
                    //Log.Message("HP" + x.GetStatValueAbstract(StatDefOf.MaxHitPoints, x));


                    // x.equippedStatOffsets.ForEach(y => Log.Message(y.stat.defName));
                    x.stuffProps.statFactors.ForEach(_StatFactor => Log.Message(_StatFactor.stat.defName + " - " + _StatFactor.value.ToString()));

                    StatModifier _MaxHitPoints = x.stuffProps.statFactors.First(_StatFactor => String.Equals(_StatFactor.stat.defName, "MaxHitPoints"));

                    if (_MaxHitPoints != null)
                    {
                        _MaxHitPoints.value = _MaxHitPoints.value * 10;
                    }
                    x.stuffProps.statFactors.ForEach(_StatFactor => Log.Message(_StatFactor.stat.defName + " - " + _StatFactor.value.ToString()));

                    //StatModifier _MaxHP = x.statBases.FirstOrDefault(y => string.Equals(y.stat.defName, "MaxHitPoints"));
                    //_MaxHP.value = _MaxHP.value * 100;
                }
            });

        }

        protected override string PatchDescription()
        {
            return "Stuff Compressor";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
        }
    }
}
