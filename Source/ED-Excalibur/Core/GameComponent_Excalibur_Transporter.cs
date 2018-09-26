using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Transporter : GameComponent_BaseClass
    {
        public override void ExposeData()
        {

        }

        public void ExposeData_WorldComp()
        {
            Scribe_Collections.Look<Thing>(ref this.TransportBuffer, "TransportBuffer", LookMode.Deep);

        }

        public override int GetTickInterval()
        {
            return int.MaxValue;
        }

        public override void TickOnInterval()
        {
        }


        public List<Thing> TransportBuffer = new List<Thing>();
    }
}
