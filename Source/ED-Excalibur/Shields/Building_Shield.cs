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

        public override string GetInspectString()
        {
            return this.GetComp<Comp_ShieldGenerator>().CompInspectStringExtra();
        }

        public Boolean WillInterceptDropPods()
        {
            return this.GetComp<Comp_ShieldGenerator>().m_InterceptDropPod_Active;
        }

        #region Methods
        
        #endregion
        
    }
}