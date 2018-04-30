using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Quest.Dialog
{
    class Dialog_4_NeedResources : Window
    {
        public Dialog_4_NeedResources()
        {
            this.resizeable = false;
            this.optionalTitle = "Need Resources";
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

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(1024f, (float)UI.screenHeight);
            }
        }

        private string introText = "A.L.I.X. contacts you stating that materials will be required to continue to repair the manoeuvring thrusters to stabilise ship. She has modified the ground building fabrication system so that it can now construct replacement parts for the ship, but it will require the raw resources in order to do so, specifically metal. " + Environment.NewLine + 
            "There was enough materials remaining to repair the Transporter Beaming System.She provides you with instructions on modifying your existing Transponder so it can be used for targeting the Transport Beams.You can then use this system to provide Metal to the E.D.S.N Excalibur to continue repairs.";

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
