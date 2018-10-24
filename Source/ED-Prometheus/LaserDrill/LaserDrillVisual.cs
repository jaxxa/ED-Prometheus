using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.LaserDrill
{
    class LaserDrillVisual : ThingWithComps
    {

        private static readonly FloatRange AngleRange = new FloatRange(-12f, 12f);
        public int duration = 600;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            Log.Message("Laser");
            float _Angle = LaserDrillVisual.AngleRange.RandomInRange;
            //int _StartTick = Find.TickManager.TicksGame;
            this.GetComp<CompAffectsSky>().StartFadeInHoldFadeOut(30, this.duration - 30 - 15, 15, 1f);
            this.GetComp<CompOrbitalBeam>().StartAnimation(this.duration, 10, _Angle);

            MoteMaker.MakeBombardmentMote(this.Position, this.Map);
            MoteMaker.MakePowerBeamMote(this.Position, this.Map);
        }
        
        public override void Draw()
        {
            base.Comps_PostDraw();
        }

    }
}
