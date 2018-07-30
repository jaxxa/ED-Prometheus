using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace EnhancedDevelopment.Excalibur.Shields
{
    [StaticConstructorOnStartup]
    class ShieldManagerMapComp : MapComponent
    {

        public static readonly SoundDef HitSoundDef = SoundDef.Named("Shields_HitShield");

        private List<Projectile> m_Projectiles = new List<Projectile>();

        public ShieldManagerMapComp(Map map) : base(map)
        {
            this.map = map;
            //   map.components.<ShieldManagerMapComp>();
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            // Log.Message("MapCompTick");
        }

        public bool WillDropPodBeIntercepted(DropPodIncoming dropPodToTest)
        {
            IEnumerable<Building_Shield> _ShieldBuildings = map.listerBuildings.AllBuildingsColonistOfClass<Building_Shield>();
            if (_ShieldBuildings.Any(x =>
                                       {

                                           float _Distance = Vector3.Distance(dropPodToTest.Position.ToVector3(), x.Position.ToVector3());

                                           float _Radius = x.GetComp<Comp_ShieldGenerator>().m_Field_Radius_Selected;

                                           if (_Distance <= _Radius && x.WillInterceptDropPods())
                                           {
                                               return true;
                                           }
                                           return false;

                                       }))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool WillProjectileBeBlocked(Verse.Projectile projectile)
        {

            IEnumerable<Building_Shield> _ShieldBuildings = map.listerBuildings.AllBuildingsColonistOfClass<Building_Shield>();
            //Log.Message("Buildings: " + _ShieldBuildings.Count().ToString());

            //if (_ShieldBuildings.Any(x => (Vector3.Distance(projectile.ExactPosition, x.Position.ToVector3()) <= 5.0f)))
            if (_ShieldBuildings.Any(x =>
            {
                Vector3 _Projetile2DPosition = new Vector3(projectile.ExactPosition.x, 0, projectile.ExactPosition.z);
                float _Distance = Vector3.Distance(_Projetile2DPosition, x.Position.ToVector3());

                //Log.Message("Projectile:" + _Projetile2DPosition.ToString());
                //Log.Message("Shield:" + x.Position.ToVector3());

                //Log.Message("Distance: " + _Distance.ToString());
                float _Radius = x.GetComp<Comp_ShieldGenerator>().m_Field_Radius_Selected;

                if (_Distance <= _Radius)
                {
                    return ShieldManagerMapComp.CorrectAngleToIntercept(projectile, x);
                }
                return false;
            }))
            {
                //Log.Message("Blocked");

                //On hit effects
                MoteMaker.ThrowLightningGlow(projectile.ExactPosition, this.map, 0.5f);
                //On hit sound
                HitSoundDef.PlayOneShot((SoundInfo)new TargetInfo(projectile.Position, projectile.Map, false));

                projectile.Destroy();
                return true;
            }

            return false;
        }




        public static Boolean CorrectAngleToIntercept(Projectile pr, Building_Shield shieldBuilding)
        {
            //Detect proper collision using angles
            Quaternion targetAngle = pr.ExactRotation;

            Vector3 projectilePosition2D = pr.ExactPosition;
            projectilePosition2D.y = 0;

            Vector3 shieldPosition2D = shieldBuilding.Position.ToVector3();
            shieldPosition2D.y = 0;

            Quaternion shieldProjAng = Quaternion.LookRotation(projectilePosition2D - shieldPosition2D);

            if ((Quaternion.Angle(targetAngle, shieldProjAng) > 90))
            {
                return true;
            }

            return false;
        }


    }
}
