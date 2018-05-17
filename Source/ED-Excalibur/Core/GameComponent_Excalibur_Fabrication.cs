using EnhancedDevelopment.Excalibur.Fabrication;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Fabrication : GameComponent_BaseClass
    {
        public override void ExposeData()
        {
            //throw new NotImplementedException();
        }

        public override int GetTickInterval()
        {
            return 120;
        }

        public override void TickOnInterval()
        {
            if (this.BuildingsUnderConstruction.Any())
            {
                Log.Message("Dropping");
                BuildingInProgress _BuildingToSpawn = this.BuildingsUnderConstruction.FirstOrDefault();
                this.BuildingsUnderConstruction.Remove(_BuildingToSpawn);


                List<Thing> _Things = new List<Thing>();
                _Things.Add(_BuildingToSpawn.ContainedMinifiedThing);

                DropPodUtility.DropThingsNear(_BuildingToSpawn.DestinationPosition, _BuildingToSpawn.DestinationMap, _Things);
            }


        }
        
        //-------------------------------------------

        public List<BuildingInProgress> BuildingsUnderConstruction = new List<BuildingInProgress>();
        
        public void OrderBuilding(string buildingName, IntVec3 position, Map map)
        {
            BuildingInProgress _NewBuilding = new BuildingInProgress(buildingName, map, position);
            BuildingsUnderConstruction.Add(_NewBuilding);
        }
        
    }
}
