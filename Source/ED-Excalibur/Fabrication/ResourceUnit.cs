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

            GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(this.stackCount);
            Comp_Transporter.DisplayTransportEffect(this);

            this.DeSpawn(DestroyMode.Vanish);

            // Tell the MapDrawer that here is something thats changed
            this.Map.mapDrawer.MapMeshDirty(this.Position, MapMeshFlag.Things, true, false);
        }
    }
}
