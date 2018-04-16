using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Settings
{
    abstract class SettingSection
    {
        public abstract string Name();

        public abstract void DoSettingsWindowContents(Rect canvas);

        public abstract void ExposeData();

    }
}
