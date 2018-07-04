using EnhancedDevelopment.Excalibur.Core;
using EnhancedDevelopment.Excalibur.Transporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class ResourceUnit : ThingWithComps
    {
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            //Tag this as a candidate for Transport.
            GameComponent_Excalibur.Instance.Comp_Quest.TagMaterialsForTransport(this);
        }

        public override void TickLong()
        {
            //Tick ling has not been implimented in base Class
            // base.TickLong();

            //Tag this as a candidate for Transport.
            GameComponent_Excalibur.Instance.Comp_Quest.TagMaterialsForTransport(this);

        }

    }
}
