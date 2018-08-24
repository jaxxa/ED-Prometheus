using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class ThingForDeployment
    {
        public string defName;

        public string label;

        public int WorkRemaining = 100;

        public int TotalNeededWork = 100;
        public int TotalNeededResources = 100;
        public int TotalNeededPower = 100;
        public bool ConstructionInProgress = false;

        public int UnitsAvalable = 0;
        public int UnitsRequestedAditional = 0;

        public ThingForDeployment(string defName, string label)
        {
            this.defName = defName;
            this.label = label;
        }
        
        public void InitiateDrop(IntVec3 dropLocation, Map dropMap)
        {
            if (this.UnitsAvalable >= 1)
            {
                Building _ContainedBuilding = (Building)ThingMaker.MakeThing(ThingDef.Named(this.defName), null);
                MinifiedThing _ContainedMinifiedThing = _ContainedBuilding.MakeMinified();
                List<Thing> _Things = new List<Thing>();
                _Things.Add(_ContainedMinifiedThing);
                this.UnitsAvalable -= 1;

                DropPodUtility.DropThingsNear(dropLocation, dropMap, _Things);
            }
        }

        public Rect DoInterface(float x, float y, float width, int index, IntVec3 dropLocation = new IntVec3(), Map dropMap = null)
        {

            Rect _RectTotal = new Rect(x, y, width, 100f);

            Rect _RectTopHalf = _RectTotal.TopHalf();
            Rect _RectBottomHalf = _RectTotal.BottomHalf();

            Rect _RectQuarter1 = _RectTopHalf.TopHalf();
            if (this.ConstructionInProgress)
            {
                Widgets.TextArea(_RectQuarter1, this.label + " - In Progress", true);
            }
            else
            {
                Widgets.TextArea(_RectQuarter1, this.label, true);
            }

            Rect _RectQuarter2 = _RectTopHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter2, "Work: " + this.WorkRemaining.ToString() + " / " + this.TotalNeededWork.ToString(), true);

            Rect _RectQuarter3 = _RectBottomHalf.TopHalf();
            Widgets.TextArea(_RectQuarter3, "RU:" + this.TotalNeededResources + " Power: " + this.TotalNeededPower, true);

            Rect _RectQuarter4 = _RectBottomHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter4.LeftHalf(), "Avalable: " + this.UnitsAvalable.ToString() + " Requested: " + this.UnitsRequestedAditional.ToString(), true);
            
            if (dropMap != null && 
                this.UnitsAvalable >= 1)
            {
                if (Widgets.ButtonText(_RectQuarter4.RightHalf().RightHalf(), "Deploy", true, false, true))
                {
                    //Log.Message("Drop");
                    this.InitiateDrop(dropLocation, dropMap);
                }
            }

            if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf().LeftHalf(), "-"))
            {
                if (this.UnitsRequestedAditional > 0)
                {
                    this.UnitsRequestedAditional -= 1;
                }
            };

            if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf().RightHalf(), "+"))
            {
                this.UnitsRequestedAditional += 1;
            };

            return _RectTotal;


        }
    }
}
