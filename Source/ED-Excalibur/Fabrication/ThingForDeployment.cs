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

        //public Map DestinationMap;

        //public IntVec3 DestinationPosition;

        public string label;

        //public Building ContainedBuilding;
        //public MinifiedThing ContainedMinifiedThing;

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
            //this.DestinationMap = DestinationMap;
            //this.DestinationPosition = DestinationPosition;
            this.label = label;
        }


        public List<Thing> InitiateDrop()
        {

            Building _ContainedBuilding = (Building)ThingMaker.MakeThing(ThingDef.Named(this.defName), null);
            MinifiedThing _ContainedMinifiedThing = _ContainedBuilding.MakeMinified();
            List<Thing> _Things = new List<Thing>();
            _Things.Add(_ContainedMinifiedThing);
            return _Things;
        }

        //protected virtual void DoConfigInterface(Rect rect, Color baseColor)
        //{
        //    rect.yMin += 29f;
        //    Vector2 center = rect.center;
        //    float y = center.y;
        //    float num = rect.xMax - (rect.yMax - y);
        //    Widgets.InfoCardButton(num - 12f, y - 12f, this.ContainedBuilding);
        //}


        public Rect DoInterface(float x, float y, float width, int index)
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
            Widgets.TextArea(_RectQuarter4.LeftHalf(), "Avalable: " + this.UnitsAvalable.ToString() +  " Requested: " + this.UnitsRequestedAditional.ToString(), true);

            if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "-"))
            {
                if (this.UnitsRequestedAditional > 0)
                {
                    this.UnitsRequestedAditional -= 1;
                }
            };

            if (Widgets.ButtonText(_RectQuarter4.RightHalf().RightHalf(), "+"))
            {
                this.UnitsRequestedAditional += 1;
            };

            return _RectTotal;


        }
    }
}
