﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Quest.Dialog
{
    class Dialog_3_InitialCharge : Window
    {
        public Dialog_3_InitialCharge()
        {
            this.resizeable = false;
            this.optionalTitle = "Initial Charge";
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

        private string introText = "Charging in Progress";

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