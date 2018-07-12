using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest
{
    abstract class ShipSystem
    {
        public abstract String Name();


        public Rect DoInterface(float x, float y, float width, int index)
        {

            Rect _RectTotal = new Rect(x, y, width, 100f);

            Rect _RectTopHalf = _RectTotal.TopHalf();
            Rect _RectBottomHalf = _RectTotal.BottomHalf();

            Rect _RectQuarter1 = _RectTopHalf.TopHalf();
            Widgets.TextArea(_RectQuarter1, this.Name() + " Status", true);

            Rect _RectQuarter2 = _RectTopHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter2, "Work Remaining: " + "TEST", true);

            Rect _RectQuarter3 = _RectBottomHalf.TopHalf();
            Widgets.TextArea(_RectQuarter3, "RU:" + "TEST" + " Power: " + "TEST", true);

            Rect _RectQuarter4 = _RectBottomHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter4.LeftHalf(), "Number To Build:" + "TEST", true);


            if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "-"))
            {
               // this.NumberOfRequestsRemailing -= 1;
            };


            if (Widgets.ButtonText(_RectQuarter4.RightHalf().RightHalf(), "+"))
            {
                //this.NumberOfRequestsRemailing += 1;
            };

            return _RectTotal;


        }

    }
}
