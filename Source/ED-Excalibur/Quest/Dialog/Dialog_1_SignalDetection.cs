using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Quest.Dialog
{
    class Dialog_1_SignalDetection : Window
    {
        public Dialog_1_SignalDetection()
        {
            this.resizeable = false;
            this.optionalTitle = "Signal Detected";
            //this.CloseButSize = new Vector2(50, 50);

            this.doCloseButton = true;
            this.doCloseX = false;
            this.closeOnClickedOutside = false;

            //this.SetInitialSizeAndPosition();
        }


        public override void DoWindowContents(Rect inRect)
        {

            this.InitialWindowContents(inRect);

            //Widgets.ButtonText(inRect, "Button1", true, false, true);
            // throw new NotImplementedException();
        }

        //public override Vector2 InitialSize
        //{
        //    get
        //    {
        //        return new Vector2(1024f, (float)UI.screenHeight);
        //    }
        //}

        private string introText = "A strange transmission has been detected on the Comms Console, for the past while the operator thought they could hear a ghost of something in the static, elusive enough to slip away whenever they tried to make it out, but this time it was strong enough to be isolated and understood. " + Environment.NewLine + Environment.NewLine +
            "It appears to be a repeating destress call and request for a further contact over a nonstandard encrypted frequency. Included are specifications for the device to make contact.One of the researchers thinks that with a bit of work they can go over the specifications and figure out how to build a device to make contact." + Environment.NewLine + Environment.NewLine +
            "Unlocked Research - Analyse Strange Signal";

        private void InitialWindowContents(Rect canvas)
        {

            //Rect _CloseButtons = Canvas.BottomPartPixels(40);
            //if (Widgets.ButtonText(_CloseButtons.RightPartPixels(100), "Close", true, true, true))
            //{
            //    this.Close(true);
            //}

            Rect _TextArea = canvas.TopPartPixels(canvas.height - this.CloseButSize.y);

            Widgets.TextArea(_TextArea, introText, true);
        }
    }
}
