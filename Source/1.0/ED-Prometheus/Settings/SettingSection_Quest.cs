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

        private const int DEFAULT_INITAL_OVERRIDE_DISABLED = -1;

        public int InitialShipSetup_PowerRequired = SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED;
        public int InitialShipSetup_ResourcesRequired = SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED;

        public int Quest_PowerForNanoMaterial = SettingSection_Quest.DEFAULT_INITAL_OVERRIDE_DISABLED;
        public int Quest_ResourceUnitsForNanoMaterial = SettingSection_Quest.DEFAULT_INITAL_OVERRIDE_DISABLED;

        public bool Quest_OverrideConsoleRequired = false;

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

            listing_Standard.Label("-1 indicates a default value set in XML");

            listing_Standard.Gap(12f);

            listing_Standard.Label("Power For Nano Material: " + Quest_PowerForNanoMaterial.ToString());
            listing_Standard.Label("XML Default is: 500");
            listing_Standard.Gap();
            Listing_Standard _listing_Standard_Quest_PowerForNanoMaterial = new Listing_Standard();
            _listing_Standard_Quest_PowerForNanoMaterial.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_Quest_PowerForNanoMaterial.ColumnWidth = 70;
            _listing_Standard_Quest_PowerForNanoMaterial.IntAdjuster(ref Quest_PowerForNanoMaterial, 1, 0);
            _listing_Standard_Quest_PowerForNanoMaterial.NewColumn();
            _listing_Standard_Quest_PowerForNanoMaterial.IntAdjuster(ref Quest_PowerForNanoMaterial, 100, 0);
            _listing_Standard_Quest_PowerForNanoMaterial.NewColumn();
            _listing_Standard_Quest_PowerForNanoMaterial.IntSetter(ref Quest_PowerForNanoMaterial, SettingSection_Quest.DEFAULT_INITAL_OVERRIDE_DISABLED, "Default");
            _listing_Standard_Quest_PowerForNanoMaterial.End();

            listing_Standard.Gap(12f);

            listing_Standard.Label("Resource Units For Nano Material: " + Quest_ResourceUnitsForNanoMaterial.ToString());
            listing_Standard.Label("XML Default is: 100");
            listing_Standard.Gap();
            Listing_Standard _listing_Standard_Quest_ResourceUnitsForNanoMaterial = new Listing_Standard();
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.ColumnWidth = 70;
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.IntAdjuster(ref Quest_ResourceUnitsForNanoMaterial, 1, 0);
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.NewColumn();
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.IntAdjuster(ref Quest_ResourceUnitsForNanoMaterial, 100, 0);
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.NewColumn();
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.IntSetter(ref Quest_ResourceUnitsForNanoMaterial, SettingSection_Quest.DEFAULT_INITAL_OVERRIDE_DISABLED, "Default");
            _listing_Standard_Quest_ResourceUnitsForNanoMaterial.End();

            listing_Standard.GapLine(12f);

            listing_Standard.CheckboxLabeled("Override Console Required", ref Quest_OverrideConsoleRequired, "Allows detecting the initial signal without needing a communication console.");

            listing_Standard.GapLine(12f);

            listing_Standard.End();


        }

        public override void ExposeData()
        {
            //throw new NotImplementedException();
            Scribe_Values.Look<int>(ref InitialShipSetup_PowerRequired, "InitialShipSetup_PowerRequired", SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED);
            Scribe_Values.Look<int>(ref InitialShipSetup_ResourcesRequired, "InitialShipSetup_ResourcesRequired", SettingSection_Quest.DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED);
            Scribe_Values.Look<int>(ref Quest_PowerForNanoMaterial, "Quest_PowerForNanoMaterial", SettingSection_Quest.DEFAULT_INITAL_OVERRIDE_DISABLED);
            Scribe_Values.Look<int>(ref Quest_ResourceUnitsForNanoMaterial, "Quest_ResourceUnitsForNanoMaterial", SettingSection_Quest.DEFAULT_INITAL_OVERRIDE_DISABLED);
            Scribe_Values.Look<bool>(ref Quest_OverrideConsoleRequired, "Quest_OverrideConsoleRequired", false);
        }

        public override string Name()
        {
            return "Quest";
        }
    }
}
