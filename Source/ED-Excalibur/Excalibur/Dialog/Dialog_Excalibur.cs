using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Excalibur.Dialog
{
    class Dialog_Excalibur : Window
    {
        public Dialog_Excalibur()
        {
            this.resizeable = true;
            this.optionalTitle = "First Contact";
            //this.SetInitialSizeAndPosition();
        }


        public override void DoWindowContents(Rect inRect)
        {

            this.InitialWindowContents(inRect);

            //Widgets.ButtonText(inRect, "Button1", true, false, true);
            // throw new NotImplementedException();
        }

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(1024f, (float)UI.screenHeight);
            }
        }

        private string introText = "You detect a strange transmission on the Comms Console, for the past while you thought you could find a ghost of something in the static, but this time it is clear enough to make out." + Environment.NewLine +
"It appears to be Destress Call directed to a specific group and asking them to make contact using an encrypted system on nonstandard frequencies.One of your Researchers thinks that with a bit of work the encryption could be broken and they could come with a device to make contact." + Environment.NewLine +
"Unlocked Research: Analyse Strange Signal" + Environment.NewLine +
"-Needs Multi-Analyzer and High Tech Bench";

        private void InitialWindowContents(Rect Canvas)
        {
            Widgets.TextArea(Canvas, introText, true);
        }
    }
}
