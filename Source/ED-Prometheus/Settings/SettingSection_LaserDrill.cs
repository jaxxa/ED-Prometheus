using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    class SettingSection_LaserDrill : SettingSection
    {

        private const int DEFAULT_REQUIRED_DRILL_SCANNING = 500;
        private const int DEFAULT_REQUIRED_DRILL_SHIP_POWER = 10000;

        //Fields
        public int RequiredDrillScanning = SettingSection_LaserDrill.DEFAULT_REQUIRED_DRILL_SCANNING;
        public int RequiredDrillShipPower = SettingSection_LaserDrill.DEFAULT_REQUIRED_DRILL_SHIP_POWER;
       // public bool AllowSimultaneousDrilling = false;


        public override void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.ColumnWidth = 250f;
            listing_Standard.Begin(canvas);

            listing_Standard.GapLine(12f);

            listing_Standard.Label("Drill Scanning Time: " + RequiredDrillScanning.ToString());
            listing_Standard.Gap();
            Listing_Standard _listing_Standard_RequiredDrillScanning = new Listing_Standard();
            _listing_Standard_RequiredDrillScanning.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_RequiredDrillScanning.ColumnWidth = 70;
            _listing_Standard_RequiredDrillScanning.IntAdjuster(ref RequiredDrillScanning, 10, 10);
            _listing_Standard_RequiredDrillScanning.NewColumn();
            _listing_Standard_RequiredDrillScanning.IntAdjuster(ref RequiredDrillScanning, 100, 100);
            _listing_Standard_RequiredDrillScanning.NewColumn();
            _listing_Standard_RequiredDrillScanning.IntSetter(ref RequiredDrillScanning, SettingSection_LaserDrill.DEFAULT_REQUIRED_DRILL_SCANNING, "Default");
            _listing_Standard_RequiredDrillScanning.End();

            listing_Standard.GapLine(12f);

            
            listing_Standard.Label("Required Ship Power: " + RequiredDrillShipPower.ToString());
            listing_Standard.Gap();
            Listing_Standard _listing_Standard_RequiredRequiredDrillShipPower = new Listing_Standard();
            _listing_Standard_RequiredRequiredDrillShipPower.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_RequiredRequiredDrillShipPower.ColumnWidth = 70;
            _listing_Standard_RequiredRequiredDrillShipPower.IntAdjuster(ref RequiredDrillShipPower, 100, 100);
            _listing_Standard_RequiredRequiredDrillShipPower.NewColumn();
            _listing_Standard_RequiredRequiredDrillShipPower.IntAdjuster(ref RequiredDrillShipPower, 1000, 1000);
            _listing_Standard_RequiredRequiredDrillShipPower.NewColumn();
            _listing_Standard_RequiredRequiredDrillShipPower.IntSetter(ref RequiredDrillShipPower, SettingSection_LaserDrill.DEFAULT_REQUIRED_DRILL_SHIP_POWER, "Default");
            _listing_Standard_RequiredRequiredDrillShipPower.End();
            

            listing_Standard.GapLine(12f);

            //listing_Standard.Label("Allow Simultaneous Drilling:");
            //listing_Standard.Gap(12f);
            //listing_Standard.CheckboxLabeled("Allow Simultaneous Drilling", ref AllowSimultaneousDrilling, "True if you want to allow Multiple Drills at once.");
            //listing_Standard.GapLine(12f);

            listing_Standard.End();

        }

        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref RequiredDrillScanning, "RequiredDrillScanning", SettingSection_LaserDrill.DEFAULT_REQUIRED_DRILL_SCANNING);
            Scribe_Values.Look<int>(ref RequiredDrillShipPower, "RequiredDrillShipPower", SettingSection_LaserDrill.DEFAULT_REQUIRED_DRILL_SHIP_POWER);
            //Scribe_Values.Look<bool>(ref AllowSimultaneousDrilling, "AllowSimultaneousDrilling", false);
        }

        public override string Name()
        {
            return "Laser Drill";
        }
    }
}
