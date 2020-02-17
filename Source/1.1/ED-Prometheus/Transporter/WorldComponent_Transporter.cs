using EnhancedDevelopment.Prometheus.Core;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnhancedDevelopment.Prometheus.Transporter
{
    class WorldComponent_Transporter : WorldComponent
    {
        public WorldComponent_Transporter(World world) : base(world)
        {
        }

        public override void WorldComponentUpdate()
        {
        }

        public override void WorldComponentTick()
        {
        }

        public override void ExposeData()
        {

            GameComponent_Prometheus.Instance.Comp_Transporter.ExposeData_WorldComp();
        }

        public override void FinalizeInit()
        {
        }


    }
}
