﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

using RimWorld;
using Verse;
using Verse.Sound;

//using EnhancedDevelopment.Shields.ShieldUtils;

namespace EnhancedDevelopment.Prometheus.Shields
{
    [StaticConstructorOnStartup]
    public class Building_Shield : Building
    {

        #region Methods
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            
            if (this.def.defName.Equals("Building_ShieldGenerator"))
            {
                Log.Message("Legacy Shield Spawned, Replacing With New Building_ED_ShieldGenerator");

                IntVec3 _Position = this.Position;
                this.DeSpawn();
                
                GenSpawn.Spawn(ThingDef.Named("Building_ED_ShieldGenerator"), _Position, map);
            }

        }


        public override string GetInspectString()
        {
            return this.GetComp<Comp_ShieldGenerator>().CompInspectStringExtra();
        }

        public bool WillInterceptDropPod(DropPodIncoming dropPodToCheck)
        {
            return this.GetComp<Comp_ShieldGenerator>().WillInterceptDropPod(dropPodToCheck);
        }

        public bool WillProjectileBeBlocked(Projectile projectileToCheck)
        {
            return this.GetComp<Comp_ShieldGenerator>().WillProjectileBeBlocked(projectileToCheck);
        }

        public void TakeDamageFromProjectile(Projectile projectile)
        {
            this.GetComp<Comp_ShieldGenerator>().FieldIntegrity_Current -= projectile.DamageAmount;
        }

        public void RecalculateStatistics()
        {
            //Log.Message("Calculate");
            this.GetComp<Comp_ShieldGenerator>().RecalculateStatistics();
        }               
        
        #endregion //Methods

    }
}