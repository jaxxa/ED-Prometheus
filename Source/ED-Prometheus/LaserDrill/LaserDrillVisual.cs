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


        public override void Tick()
        {
            base.Tick();
            if (!base.Destroyed)
            {
                if (Find.TickManager.TicksGame % 20 == 0)
                {
                    this.StartRandomFire();
                }
            }
        }

        private static readonly SimpleCurve DistanceChanceFactor = new SimpleCurve
        {
            {
                new CurvePoint(0f, 1f),
                true
            },
            {
                new CurvePoint(10f, 0.1f),
                true
            }
        };

        private void StartRandomFire()
        {
            IntVec3 c = (from x in GenRadial.RadialCellsAround(base.Position, 25f, true)
                         where x.InBounds(base.Map)
                         select x).RandomElementByWeight((IntVec3 x) => LaserDrillVisual.DistanceChanceFactor.Evaluate(x.DistanceTo(base.Position)));
            FireUtility.TryStartFireIn(c, base.Map, Rand.Range(0.1f, 0.925f));
        }



        public override void Draw()
        {
            base.Comps_PostDraw();
        }

    }
}
