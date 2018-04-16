using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Settings
{
    class SettingSection_NanoShields : SettingSection
    {
        public int ShieldChargeLevelMax;

        public int BuildingChargeDelay;
        public int BuildingChargeAmount;
        public int BuildingReservePowerMax;

        public override void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.ColumnWidth = 250f;
            listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            //ShieldChargeLevelMax
            listing_Standard.GapLine(12f);
            listing_Standard.Label("ShieldChargeLevelMax (Default 200):" + ShieldChargeLevelMax.ToString());
            listing_Standard.Gap(12f);
            Listing_Standard _listing_Standard_ShieldChargeLevelMax = new Listing_Standard();
            _listing_Standard_ShieldChargeLevelMax.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_ShieldChargeLevelMax.ColumnWidth = 70;
            _listing_Standard_ShieldChargeLevelMax.IntAdjuster(ref ShieldChargeLevelMax, 5, 1);
            _listing_Standard_ShieldChargeLevelMax.NewColumn();
            _listing_Standard_ShieldChargeLevelMax.IntAdjuster(ref ShieldChargeLevelMax, 20, 1);
            _listing_Standard_ShieldChargeLevelMax.NewColumn();
            _listing_Standard_ShieldChargeLevelMax.IntSetter(ref ShieldChargeLevelMax, 200, "Default");
            _listing_Standard_ShieldChargeLevelMax.End();

            //BuildingChargeDelay
            listing_Standard.GapLine(12f);
            listing_Standard.Label("BuildingChargeDelay (Default 30, measuered in Ticks):" + BuildingChargeDelay.ToString());
            listing_Standard.Gap(12f);
            Listing_Standard _listing_Standard_BuildingChargeDelay = new Listing_Standard();
            _listing_Standard_BuildingChargeDelay.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_BuildingChargeDelay.ColumnWidth = 70;
            _listing_Standard_BuildingChargeDelay.IntAdjuster(ref BuildingChargeDelay, 1, 1);
            _listing_Standard_BuildingChargeDelay.NewColumn();
            _listing_Standard_BuildingChargeDelay.IntAdjuster(ref BuildingChargeDelay, 10, 1);
            _listing_Standard_BuildingChargeDelay.NewColumn();
            _listing_Standard_BuildingChargeDelay.IntSetter(ref BuildingChargeDelay, 30, "Default");
            _listing_Standard_BuildingChargeDelay.End();

            //BuildingChargeAmount
            listing_Standard.GapLine(12f);
            listing_Standard.Label("BuildingChargeAmount (Default 1):" + BuildingChargeAmount.ToString());
            listing_Standard.Gap(12f);
            Listing_Standard _listing_Standard_BuildingChargeAmount = new Listing_Standard();
            _listing_Standard_BuildingChargeAmount.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_BuildingChargeAmount.ColumnWidth = 70;
            _listing_Standard_BuildingChargeAmount.IntAdjuster(ref BuildingChargeAmount, 1, 1);
            _listing_Standard_BuildingChargeAmount.NewColumn();
            _listing_Standard_BuildingChargeAmount.IntAdjuster(ref BuildingChargeAmount, 10, 1);
            _listing_Standard_BuildingChargeAmount.NewColumn();
            _listing_Standard_BuildingChargeAmount.IntSetter(ref BuildingChargeAmount, 1, "Default");
            _listing_Standard_BuildingChargeAmount.End();

            listing_Standard.GapLine(12f);
            listing_Standard.Label("BuildingReservePowerMax (Default 400):" + BuildingReservePowerMax.ToString());
            listing_Standard.Gap(12f);
            // listing_Standard.IntAdjuster(ref BuildingReservePowerMax, 10, 10);
            Listing_Standard _listing_Standard_BuildingReservePowerMax = new Listing_Standard();
            _listing_Standard_BuildingReservePowerMax.Begin(listing_Standard.GetRect(30f));
            _listing_Standard_BuildingReservePowerMax.ColumnWidth = 70;
            _listing_Standard_BuildingReservePowerMax.IntAdjuster(ref BuildingReservePowerMax, 10, 1);
            _listing_Standard_BuildingReservePowerMax.NewColumn();
            _listing_Standard_BuildingReservePowerMax.IntAdjuster(ref BuildingReservePowerMax, 50, 1);
            _listing_Standard_BuildingReservePowerMax.NewColumn();
            _listing_Standard_BuildingReservePowerMax.IntSetter(ref BuildingReservePowerMax, 400, "Default");
            _listing_Standard_BuildingReservePowerMax.End();
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ShieldChargeLevelMax, "ShieldChargeLevelMax", 200);

            Scribe_Values.Look(ref BuildingChargeDelay, "BuildingChargeDelay", 30);
            Scribe_Values.Look(ref BuildingChargeAmount, "BuildingChargeAmount", 1);

            Scribe_Values.Look(ref BuildingReservePowerMax, "BuildingReservePowerMax", 400);
        }

        public override string Name()
        {
            return "Nano Shields";
        }
    }
}
