using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

using RimWorld;
using Verse;
using Verse.Sound;

//using EnhancedDevelopment.Shields.ShieldUtils;

namespace EnhancedDevelopment.Excalibur.Shields
{
    [StaticConstructorOnStartup]
    public class Building_Shield : Building
    {

        #region Methods

        /*
        /// <summary>
        /// Finds all projectiles at the position and destroys them
        /// </summary>
        /// <param name="square">The current square to protect</param>
        /// <param name="flag_direct">Block direct Fire</param>
        /// <param name="flag_indirect">Block indirect Fire</param>
        virtual public void ProtectSquare(IntVec3 square)
        {
            //Ignore squares outside the map
            if (!square.InBounds(this.Map))
            {
                return;
            }
            List<Thing> things = this.Map.thingGrid.ThingsListAt(square);
            List<Thing> thingsToDestroy = new List<Thing>();

            for (int i = 0, l = things.Count(); i < l; i++)
            {

                if (things[i] != null && things[i] is Projectile)
                {
                    //Assign to variable
                    Projectile pr = (Projectile)things[i];
                   // if (!pr.Destroyed && ((this.m_BlockIndirect_Avalable && this.m_BlockDirect_Active && pr.def.projectile.flyOverhead) || (this.m_BlockDirect_Avalable && this.m_BlockIndirect_Active && !pr.def.projectile.flyOverhead)))
                    {
                        bool wantToIntercept = true;

                        //Check OverShoot
                        if (pr.def.projectile.flyOverhead)
                        {
                            if (this.WillTargetLandInRange(pr))
                            {
                                //Log.Message("Fly Over");
                            }
                            else
                            {
                                wantToIntercept = false;
                                //Log.Message("In Range");
                            }
                        }


                        if (wantToIntercept)
                        {

                            //Detect proper collision using angles
                            Quaternion targetAngle = pr.ExactRotation;

                            Vector3 projectilePosition2D = pr.ExactPosition;
                            projectilePosition2D.y = 0;

                            Vector3 shieldPosition2D = EnhancedDevelopment.Excalibur.Shields.Utilities.VectorsUtils.IntVecToVec(this.Position);

                            shieldPosition2D.y = 0;

                            Quaternion shieldProjAng = Quaternion.LookRotation(projectilePosition2D - shieldPosition2D);


                            if ((Quaternion.Angle(targetAngle, shieldProjAng) > 90))
                            {

                                //On hit effects
                                MoteMaker.ThrowLightningGlow(pr.ExactPosition, this.Map, 0.5f);
                                //On hit sound
                                HitSoundDef.PlayOneShot((SoundInfo)new TargetInfo(this.Position, this.Map, false));

                                //Damage the shield
                                ProcessDamage(pr.def.projectile.DamageAmount);
                                //add projectile to the list of things to be destroyed
                                thingsToDestroy.Add(pr);
                            }
                        }
                        else
                        {
                            //Log.Message("Skip");
                        }

                    }
                }
            }
            foreach (Thing currentThing in thingsToDestroy)
            {
                currentThing.Destroy();
            }

        }

        /// <summary>
        /// Damage the Shield
        /// </summary>
        /// <param name="damage">The amount of damage to give</param>
        public void ProcessDamage(int damage)
        {
            //Prevent negative health
            if (this.FieldIntegrity_Current <= 0)
                return;
            this.FieldIntegrity_Current -= (int)(((float)damage) * powerToDamage);
        }

        /// <summary>
        /// Checks if the projectile will land within the shield or pass over.
        /// </summary>
        /// <param name="projectile">The specific projectile that is being checked</param>
        /// <returns>True if the projectile will land close, false if it will be far away.</returns>
        public bool WillTargetLandInRange(Projectile projectile)
        {
            Vector3 targetLocation = GetTargetLocationFromProjectile(projectile);

            if (Vector3.Distance(this.Position.ToVector3(), targetLocation) > this.m_Field_Radius)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Vector3 GetTargetLocationFromProjectile(Projectile projectile)
        {
            FieldInfo fieldInfo = projectile.GetType().GetField("destination", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            Vector3 reoveredVector = (Vector3)fieldInfo.GetValue(projectile);
            return reoveredVector;
        }

        private void interceptPods()
        {
            if (this.m_InterceptDropPod_Avalable && this.m_InterceptDropPod_Active)
            {
                IEnumerable<Thing> dropPods = this.Map.listerThings.ThingsOfDef(ThingDefOf.ActiveDropPod);

                if (dropPods != null)
                {
                    //Test to protect the entire map from droppods.
                    //IEnumerable<Thing> closeFires = dropPods.Where<Thing>(t => t.Position.InHorDistOf(this.position, 9999999.0f));

                    IEnumerable<Thing> closeFires = dropPods.Where<Thing>(t => t.Position.InHorDistOf(this.Position, this.m_Field_Radius));

                    foreach (RimWorld.ActiveDropPod currentDropPod in closeFires.ToList())
                    {
                        //currentDropPod.Destroy();

                        GenExplosion.DoExplosion(currentDropPod.Position, this.Map, 1, DamageDefOf.Bomb, currentDropPod);

                        currentDropPod.Destroy(DestroyMode.Vanish);


                        // BodyPartDamageInfo bodyPartDamageInfo = new BodyPartDamageInfo(new BodyPartHeight?(), new BodyPartDepth?(BodyPartDepth.Outside));
                        //new ExplosionInfo()
                        //{
                        //    center = currentDropPod.Position,
                        //    radius = 1,
                        //    dinfo = new DamageInfo(DamageDefOf.Flame, 10, currentDropPod)

                        //}.DoExplosion();

                    }
                }
            }
        }*/

        #endregion

        #region UI

        /*
        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (this.IsActive())
            {
                stringBuilder.AppendLine("Shield: " + this.FieldIntegrity_Current + "/" + this.m_FieldIntegrity_Max);
            }
            else if (this.CurrentStatus == EnumShieldStatus.Initilising)
            {
                //stringBuilder.AppendLine("Initiating shield: " + ((warmupTicks * 100) / recoverWarmup) + "%");
                stringBuilder.AppendLine("Ready in " + Math.Round(GenTicks.TicksToSeconds(m_WarmupTicksRemaining)) + " seconds.");
                //stringBuilder.AppendLine("Ready in " + m_warmupTicksCurrent + " seconds.");
            }
            else
            {
                stringBuilder.AppendLine("Shield disabled!");
            }

            if (m_Power != null)
            {
                string text = m_Power.CompInspectStringExtra();
                if (!text.NullOrEmpty())
                {
                    stringBuilder.Append(text);
                }
                else
                {
                    stringBuilder.Append("Error, No Power Comp Text.");
                }
            }
            else
            {
                stringBuilder.Append("Error, No Power Comp.");
            }

            return stringBuilder.ToString();
        }*/


              
        #endregion

    }
}