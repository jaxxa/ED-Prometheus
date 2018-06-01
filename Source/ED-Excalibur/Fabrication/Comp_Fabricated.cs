using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class Comp_Fabricated : ThingComp
    {
        public CompProperties_Fabricated Properties;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            this.Properties = ((CompProperties_Fabricated)this.props);
        }

    }
}
