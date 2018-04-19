using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Excalibur
{
    class Dialog_Excalibur : Window
    {
        public Dialog_Excalibur()
        {
            this.resizeable = true;
            //this.SetInitialSizeAndPosition();
        }


        public override void DoWindowContents(Rect inRect)
        {

            Widgets.ButtonText(inRect, "Button1", true, false, true);
           // throw new NotImplementedException();
        }

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(1024f, (float)UI.screenHeight);
            }
        }
    }
}
