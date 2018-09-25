using EnhancedDevelopment.Excalibur.Core;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnhancedDevelopment.Excalibur.Transporter
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

            GameComponent_Excalibur.Instance.Comp_Transporter.ExposeData2();
        }

        public override void FinalizeInit()
        {
        }


    }
}
