/*using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class Comp_CompressedStuff : ThingComp
    {

        public CompProperties_CompressedStuff Properties;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            Log.Message("Tick");
            base.PostSpawnSetup(respawningAfterLoad);

            this.Properties = ((CompProperties_CompressedStuff)this.props);
        }

        public override void CompTick()
        {
           
            base.CompTick();
            Log.Message("Tick");
        }

    }
}
*/