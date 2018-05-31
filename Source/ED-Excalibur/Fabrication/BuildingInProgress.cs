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

        //public Building ContainedBuilding;
        //public MinifiedThing ContainedMinifiedThing;

        public int WorkRemaining = 100;

        public int ResourcesNeeded = 50;

        public int NumberOfRequestsRemailing = 1;

        public BuildingInProgress(string defName, Map DestinationMap, IntVec3 DestinationPosition)
        {
            this.defName = defName;
            this.DestinationMap = DestinationMap;
            this.DestinationPosition = DestinationPosition;
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

            Rect _RectTotal = new Rect(x, y, width, 53f);

            Rect _RectTopHalf = _RectTotal.TopHalf();
            Widgets.TextArea(_RectTopHalf, this.defName + ": " + this.NumberOfRequestsRemailing.ToString() + " : " + this.WorkRemaining.ToString(), true);

            Rect _RectBottomHalf = _RectTotal.BottomHalf();

            if (Widgets.ButtonText(_RectBottomHalf.LeftHalf(), "-"))
            {
                this.NumberOfRequestsRemailing -= 1;
            };


            if (Widgets.ButtonText(_RectBottomHalf.RightHalf(), "+"))
            {
                this.NumberOfRequestsRemailing += 1;

            };

            return _RectTotal;


        }
    }
}
