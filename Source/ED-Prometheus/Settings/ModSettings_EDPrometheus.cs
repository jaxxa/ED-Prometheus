using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    class ModSettings_EDPrometheus : ModSettings
    {
        public List<SettingSection> m_Settings;

        public SettingSection_Shields Shields = new SettingSection_Shields();
        public SettingSection_NanoShields NanoShields = new SettingSection_NanoShields();
        public SettingSection_LaserDrill LaserDrill = new SettingSection_LaserDrill();
        public SettingSection_Quest Quest = new SettingSection_Quest();
        
        public ModSettings_EDPrometheus()
        {
            this.m_Settings = new List<SettingSection>() { Shields, NanoShields, LaserDrill, Quest};
        }

        public override void ExposeData()
        {
            base.ExposeData();

            this.m_Settings.ForEach(s => s.ExposeData());
            
            //            Scribe_Values.Look<bool>(ref ShowLettersThreatBig, "ShowLettersThreatBig", true, true);
        }

        private SettingSection m_CurrentSetting = null;

        public void DoSettingsWindowContents(Rect canvas)
        {

            Rect _ButtonRect = new Rect(350f, 0f, 150f, 35f);

            if (Widgets.ButtonText(_ButtonRect, "Select Page", true, false, true))
            {

                List<FloatMenuOption> list = new List<FloatMenuOption>();

                foreach (SettingSection _CurrentSettings in this.m_Settings)
                {
                    list.Add(new FloatMenuOption(_CurrentSettings.Name(), delegate
                    {
                        this.m_CurrentSetting = _CurrentSettings;
                    }, MenuOptionPriority.Default, null, null, 0f, null, null));

                    Find.WindowStack.Add(new FloatMenu(list));
                }

            }
            
            if (this.m_CurrentSetting != null)
            {
                Text.Font = GameFont.Medium;
                Widgets.Label(new Rect(510f, 0f, 150f, 35f), this.m_CurrentSetting.Name());
                Text.Font = GameFont.Small;

                this.m_CurrentSetting.DoSettingsWindowContents(canvas);
            }

        }
    }
}

