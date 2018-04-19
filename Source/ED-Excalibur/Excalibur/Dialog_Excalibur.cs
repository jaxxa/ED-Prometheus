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
        public override void DoWindowContents(Rect inRect)
        {

            Widgets.ButtonText(inRect, "Button1", true, false, true);
           // throw new NotImplementedException();
        }
    }
}
