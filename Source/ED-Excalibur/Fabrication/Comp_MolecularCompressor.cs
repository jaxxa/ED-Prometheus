using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class Comp_MolecularCompressor : ThingComp
    {

        public CompProperties_MolecularCompressor Properties;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            this.Properties = ((CompProperties_MolecularCompressor)this.props);
        }

        public override void CompTick()
        {
            base.CompTick();
            Log.Message("Tick");
        }

    }
}
