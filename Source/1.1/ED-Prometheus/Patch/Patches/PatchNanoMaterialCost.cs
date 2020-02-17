using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Verse;
using UnityEngine;
using EnhancedDevelopment.Prometheus.Shields;
using HarmonyLib;

namespace EnhancedDevelopment.Prometheus.Patch.Patches
{
    class PatchNanoMaterialCost : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            
            ThingDef _ThingDef = ThingDef.Named("NanoMaterial");

            CompProperties _CompPoperties = _ThingDef.CompDefFor<EnhancedDevelopment.Prometheus.Fabrication.Comp_Fabricated>();


            EnhancedDevelopment.Prometheus.Fabrication.CompProperties_Fabricated _CastedProperties = _CompPoperties as EnhancedDevelopment.Prometheus.Fabrication.CompProperties_Fabricated;
            _CastedProperties.RequiredMaterials = 5;
            _CastedProperties.RequiredPower = 55;

        }

        protected override string PatchDescription()
        {
            return "PatchNanoMaterialCost";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
        }
        
    }
}
