using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Settings
{
    class ModSettings_EDExcalibur : ModSettings
    {
        public SettingSection_Shields Shields = new SettingSection_Shields();
        public SettingSection_NanoShields NanoShields = new SettingSection_NanoShields();
        public SettingSection_LaserDrill LaserDrill = new SettingSection_LaserDrill();

        public override void ExposeData()
        {
            base.ExposeData();

            this.Shields.ExposeData();
            this.NanoShields.ExposeData();
            this.LaserDrill.ExposeData();
            
            //            Scribe_Values.Look<bool>(ref ShowLettersThreatBig, "ShowLettersThreatBig", true, true);
        }
        
        private SettingSection m_CurrentSetting = null;

        public void DoSettingsWindowContents(Rect canvas)
        {
            
            Rect _ButtonRect = new Rect(300f, 0f, 150f, 35f);

            if (Widgets.ButtonText(_ButtonRect, "Select Page", true, false, true))
            {
                
                List<FloatMenuOption> list = new List<FloatMenuOption>();
                
                list.Add(new FloatMenuOption(this.Shields.Name(), delegate
                {
                    this.m_CurrentSetting = this.Shields;
                }, MenuOptionPriority.Default, null, null, 0f, null, null));

                list.Add(new FloatMenuOption(this.NanoShields.Name(), delegate
                {
                    this.m_CurrentSetting = this.NanoShields;
                }, MenuOptionPriority.Default, null, null, 0f, null, null));

                list.Add(new FloatMenuOption(this.LaserDrill.Name(), delegate
                {
                    this.m_CurrentSetting = this.LaserDrill;
                }, MenuOptionPriority.Default, null, null, 0f, null, null));

                Find.WindowStack.Add(new FloatMenu(list));
            }


            if (this.m_CurrentSetting != null)
            {

                Text.Font = GameFont.Medium;
                Widgets.Label(new Rect(460f, 0f, 150f , 35f), this.m_CurrentSetting.Name());
                Text.Font = GameFont.Small;
                
                this.m_CurrentSetting.DoSettingsWindowContents(canvas);
            }
        }
        
    }
}
