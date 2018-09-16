using EnhancedDevelopment.Excalibur.Core;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Power
{
    [StaticConstructorOnStartup]
    class Building_QuantumPowerRelay : Building
    {

        private static readonly Vector2 BarSize = new Vector2(1.3f, 0.4f);

        private static readonly Material BatteryBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.9f, 0.85f, 0.2f), false);

        private static readonly Material BatteryBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.3f, 0.3f, 0.3f), false);

        private CompPowerBattery CompPowerBattery
        {
            get
            {
                if (this.m_CompPowerBattery == null)
                {
                    //Log.Message("GettingComp");
                    this.m_CompPowerBattery = base.GetComp<CompPowerBattery>();
                }
                return m_CompPowerBattery;
            }
        }
        private CompPowerBattery m_CompPowerBattery;

        //public Building_QuantumPowerRelay() : base()
        //{

        //    this.m_Comp = base.GetComp<CompPowerBattery>();
        //}

        public override void Draw()
        {
            base.Draw();
            GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
            r.center = this.DrawPos + Vector3.up * 0.1f;
            r.size = Building_QuantumPowerRelay.BarSize;
            r.fillPercent = this.CompPowerBattery.StoredEnergy / this.CompPowerBattery.Props.storedEnergyMax;
            r.filledMat = Building_QuantumPowerRelay.BatteryBarFilledMat;
            r.unfilledMat = Building_QuantumPowerRelay.BatteryBarUnfilledMat;
            r.margin = 0.15f;
            Rot4 rotation = base.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;
            GenDraw.DrawFillableBar(r);
            //if (this.ticksToExplode > 0 && base.Spawned)
            //{
            //    base.Map.overlayDrawer.DrawOverlay(this, OverlayTypes.BurningWick);
            //}
        }

        public override string GetInspectString()
        {
            string _Base = base.GetInspectString();
            _Base = _Base + Environment.NewLine + "Ship Power: " + GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.Power);


            return _Base;
        }

        public override void Tick()
        {
            base.Tick();
            float _HalfEnergy = this.CompPowerBattery.Props.storedEnergyMax / 2;
            int _PowerBlock = (int)(this.CompPowerBattery.Props.storedEnergyMax / 4);

            //Check if need to upload power.
            if (this.CompPowerBattery.StoredEnergy - _PowerBlock >= _HalfEnergy)
            {
                this.CompPowerBattery.DrawPower(_PowerBlock);
                GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.Power, _PowerBlock);
            }

            if (GameComponent_Excalibur.Instance.Comp_Quest.ShipSystem_PowerDistribution.IsShipToSurfacePowerAvalable())
            {
                //Check if need to download power.
                if (this.CompPowerBattery.StoredEnergy + _PowerBlock <= _HalfEnergy)
                {
                    float _ReturnedPower = GameComponent_Excalibur.Instance.Comp_Quest.ResourceRequestReserve(GameComponent_Excalibur_Quest.EnumResourceType.Power, _PowerBlock);
                    this.CompPowerBattery.AddEnergy(_ReturnedPower);
                }
            }
        }

    }
}
