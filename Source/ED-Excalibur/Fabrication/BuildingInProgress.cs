using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class BuildingInProgress
    {
        public string defName;

        public Map DestinationMap;

        public IntVec3 DestinationPosition;

        public string label;

        //public Building ContainedBuilding;
        //public MinifiedThing ContainedMinifiedThing;

        public int WorkRemaining = 100;

        public int NeededWork = 0;
        public int NeededResources = 0;
        public int NeededPower = 0;
        public bool SpentPowerAndMaterials = false;


        public int NumberOfRequestsRemailing = 1;

        public BuildingInProgress(string defName, Map DestinationMap, IntVec3 DestinationPosition, string label)
        {
            this.defName = defName;
            this.DestinationMap = DestinationMap;
            this.DestinationPosition = DestinationPosition;
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
            Widgets.TextArea(_RectQuarter1, this.label, true);

            Rect _RectQuarter2 = _RectTopHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter2, "Work Remaining: " + this.WorkRemaining.ToString(), true);
            
            Rect _RectQuarter3 = _RectBottomHalf.TopHalf();
            Widgets.TextArea(_RectQuarter3, "RU:" + this.NeededResources + " Power: " + this.NeededPower, true);
            
            Rect _RectQuarter4 = _RectBottomHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter4.LeftHalf(), "Number To Build:" + this.NumberOfRequestsRemailing.ToString(), true);


            if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "-"))
            {
                this.NumberOfRequestsRemailing -= 1;
            };


            if (Widgets.ButtonText(_RectQuarter4.RightHalf().RightHalf(), "+"))
            {
                this.NumberOfRequestsRemailing += 1;
            };

            return _RectTotal;


        }
    }
}
