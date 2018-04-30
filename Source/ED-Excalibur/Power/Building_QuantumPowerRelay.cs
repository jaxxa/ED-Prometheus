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
    class Building_QuantumPowerRelay : Building
    {

        private static readonly Vector2 BarSize = new Vector2(1.3f, 0.4f);

        private static readonly Material BatteryBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.9f, 0.85f, 0.2f), false);

        private static readonly Material BatteryBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.3f, 0.3f, 0.3f), false);
        
        private CompPowerBattery Comp
        {
            get
            {
                if (this.m_Comp == null)
                {
                    //Log.Message("GettingComp");
                    this.m_Comp = base.GetComp<CompPowerBattery>();
                }
                return m_Comp;
            }
        }
        private CompPowerBattery m_Comp;

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
            r.fillPercent = this.Comp.StoredEnergy / this.Comp.Props.storedEnergyMax;
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

        public override void Tick()
        {
            base.Tick();

            this.Comp.AddEnergy(1.0f);
            

            if (this.Comp.StoredEnergy >= 100.0f)
            {
                this.Comp.DrawPower(100.0f);
                GameComponent_Excalibur.Instance.Quest.AddReservePower(100.0f);
            }
            //Equilise Power

        }

    }
}
