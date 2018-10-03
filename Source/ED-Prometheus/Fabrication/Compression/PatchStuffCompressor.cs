/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using RimWorld;
using Verse;

namespace EnhancedDevelopment.Prometheus.Patch.Patches
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
                    //Log.Message("Compressing " + x.defName);

                    StatModifier _MaxHitPoints = x.stuffProps.statFactors.FirstOrFallback(_StatFactor => String.Equals(_StatFactor.stat.defName, "MaxHitPoints"));
                    if (_MaxHitPoints != null)
                    {
                        _MaxHitPoints.value = _MaxHitPoints.value * 10;
                    }
                    else
                    {
                        StatModifier _NewStat = new StatModifier();
                        _NewStat.stat = StatDefOf.MaxHitPoints;
                        _NewStat.value = 1f;
                        _NewStat.value = _NewStat.value * 10;
                        x.stuffProps.statFactors.Add(_NewStat);
                    }
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
*/