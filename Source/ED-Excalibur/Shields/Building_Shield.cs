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

        #endregion //Methods

    }
}