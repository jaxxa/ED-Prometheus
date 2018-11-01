using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    class Mod_EDPrometheus : Verse.Mod
    {

        public static ModSettings_EDPrometheus Settings;

        public Mod_EDPrometheus(ModContentPack content) : base(content)
        {
            Mod_EDPrometheus.Settings = GetSettings<ModSettings_EDPrometheus>();
        }

        public override string SettingsCategory()
        {
            return "ED-Prometheus";
            //return base.SettingsCategory();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }
    }
}
