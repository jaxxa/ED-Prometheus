using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    class SettingSection_Shields : SettingSection
    {

        public override void DoSettingsWindowContents(Rect canvas)
        {
            Widgets.ButtonText(canvas, "Work in Progress", true, false, true);
        }

        public override void ExposeData()
        {
            //throw new NotImplementedException();
        }

        public override string Name()
        {
            return "Shields";
        }
    }
}
