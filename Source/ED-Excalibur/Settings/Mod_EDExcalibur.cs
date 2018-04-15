using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Settings
{
    class Mod_EDExcalibur : Verse.Mod
    {

        public static ModSettings_EDExcalibur Settings;

        public Mod_EDExcalibur(ModContentPack content) : base(content)
        {
            Mod_EDExcalibur.Settings = GetSettings<ModSettings_EDExcalibur>();
        }

        public override string SettingsCategory()
        {
            return "ED-Excalibur";
            //return base.SettingsCategory();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }
    }
}
