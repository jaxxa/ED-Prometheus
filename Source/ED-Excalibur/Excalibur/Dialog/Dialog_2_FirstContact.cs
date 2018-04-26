using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Excalibur.Dialog
{
    class Dialog_2_FirstContact : Window
    {
        public Dialog_2_FirstContact()
        {
            this.resizeable = false;
            this.optionalTitle = "First Contact";
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

        private string introText = "You make contact with the source of the strange signal. The character on the other end explains that she is an Artificial Intelligence called A.L.I.X (Adaptive Logistical Intelligence Experiment). She was serving on an Experimental Command Carrier called the E.D.S.N. Excalibur where she was to manage repair and logistical support for ground teams and smaller ships in the group." +
            "Unfortunately some unknown calamity befell the ship and left it drifting dead through the vastness of space for many years until recently when it drifted close enough to this Solar System to be captured by the Stars Gravity Well and into a decaying orbit where it will eventually hit the star." + Environment.NewLine + Environment.NewLine +
            "Fortunately the backup solar panels provided just enough power to Maintenance Sector 7G where A.L.I.X.was located who was then able to salvage enough equipment to send out the general destress signal you picked up with instructions on secure contact." + Environment.NewLine + Environment.NewLine +
            "A.L.I.X.has a plan to save the ship (and herself) from hitting the sun but needs your help the first thing she needs is power to run the Auto Repair System and then fire the Manoeuvring thrusters to bring the ship into a stable orbit of your Rimworld. " + Environment.NewLine + Environment.NewLine +
            "Because the ship was designed to support long term ground operations it has a number of prefabricated buildings that can be deployed to the surface of a planet.One of these is the Starship Class Power Relay that can be used to send power from a ground installation to a compatible space craft." + Environment.NewLine + Environment.NewLine +
            "In exchange saving the E.D.S.N Excalibur, A.L.I.X.offers to help your Colony by providing use of the ships systems as they are brought online." +
            "--- Unlocks Drop of 'Starship Class Power Relay'";

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
