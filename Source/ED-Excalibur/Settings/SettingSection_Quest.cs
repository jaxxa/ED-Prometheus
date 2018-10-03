using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    class SettingSection_Quest : SettingSection
    {

        private const int DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED = 10000;
        private const int DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED = 500;
        
        public int InitialShipSetup_PowerRequired = SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED;
        public int InitialShipSetup_ResourcesRequired = SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED;

        public override void DoSettingsWindowContents(Rect canvas)
        {
            // Widgets.ButtonText(canvas, "Button1", true, false, true);

            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.ColumnWidth = 250f;
            listing_Standard.Begin(canvas);

            listing_Standard.GapLine(12f);

            listing_Standard.Label("InitialShipSetup_PowerRequired: " + InitialShipSetup_PowerRequired.ToString());
            listing_Standard.Gap();
            Listing_Standard _listing_Standard_InitialShipSetup_PowerRequired = new Listing_Standard();
            _listing_Standard_InitialShipSetup_PowerRequired.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_InitialShipSetup_PowerRequired.ColumnWidth = 70;
            _listing_Standard_InitialShipSetup_PowerRequired.IntAdjuster(ref InitialShipSetup_PowerRequired, 100, 0);
            _listing_Standard_InitialShipSetup_PowerRequired.NewColumn();
            _listing_Standard_InitialShipSetup_PowerRequired.IntAdjuster(ref InitialShipSetup_PowerRequired, 500, 0);
            _listing_Standard_InitialShipSetup_PowerRequired.NewColumn();
            _listing_Standard_InitialShipSetup_PowerRequired.IntSetter(ref InitialShipSetup_PowerRequired, SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED, "Default");
            _listing_Standard_InitialShipSetup_PowerRequired.End();

            listing_Standard.GapLine(12f);
            listing_Standard.Label("InitialShipSetup_ResourcesRequired: " + InitialShipSetup_ResourcesRequired.ToString());
            listing_Standard.Gap();
            Listing_Standard _listing_Standard_InitialShipSetup_ResourcesRequired = new Listing_Standard();
            _listing_Standard_InitialShipSetup_ResourcesRequired.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_InitialShipSetup_ResourcesRequired.ColumnWidth = 70;
            _listing_Standard_InitialShipSetup_ResourcesRequired.IntAdjuster(ref InitialShipSetup_ResourcesRequired, 10, 0);
            _listing_Standard_InitialShipSetup_ResourcesRequired.NewColumn();
            _listing_Standard_InitialShipSetup_ResourcesRequired.IntAdjuster(ref InitialShipSetup_ResourcesRequired, 100, 0);
            _listing_Standard_InitialShipSetup_ResourcesRequired.NewColumn();
            _listing_Standard_InitialShipSetup_ResourcesRequired.IntSetter(ref InitialShipSetup_ResourcesRequired, SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED, "Default");
            _listing_Standard_InitialShipSetup_ResourcesRequired.End();

            listing_Standard.GapLine(12f);

            listing_Standard.End();


        }

        public override void ExposeData()
        {
            //throw new NotImplementedException();
            Scribe_Values.Look<int>(ref InitialShipSetup_PowerRequired, "InitialShipSetup_PowerRequired", SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED);
            Scribe_Values.Look<int>(ref InitialShipSetup_ResourcesRequired, "InitialShipSetup_ResourcesRequired", SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED);
        }

        public override string Name()
        {
            return "Quest";
        }
    }
}
