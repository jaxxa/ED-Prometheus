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
            //Scribe_Deep.Look<List<Thing>>(ref this.TransportBuffer, "TransportBuffer");
            Scribe_Collections.Look<Thing>(ref this.TransportBuffer, "TransportBuffer", LookMode.Deep);

 
        }

        public override int GetTickInterval()
        {
            return int.MaxValue;
           //throw new NotImplementedException();
        }

        public override void TickOnInterval()
        {
            //throw new NotImplementedException();
        }


        public List<Thing> TransportBuffer = new List<Thing>();
    }
}
