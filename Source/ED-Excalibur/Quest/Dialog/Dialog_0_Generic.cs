using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Prometheus.Quest.Dialog
{
    class Dialog_0_Generic : Window
    {
        private string m_Text;
        private Vector2 m_Position;

        public Dialog_0_Generic(string title, string text)
        {
            this.m_Position = Vector2.zero;
            this.m_Text = text;

            this.resizeable = false;
            this.optionalTitle = title;
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

        private void InitialWindowContents(Rect canvas)
        {

            //Rect _CloseButtons = Canvas.BottomPartPixels(40);
            //if (Widgets.ButtonText(_CloseButtons.RightPartPixels(100), "Close", true, true, true))
            //{
            //    this.Close(true);
            //}

            Text.Font = GameFont.Small;

            Rect _TextArea = canvas.TopPartPixels(canvas.height - this.CloseButSize.y);

            Widgets.TextAreaScrollable(_TextArea, this.m_Text, ref this.m_Position, true);


            Text.Font = GameFont.Medium;

        }
    }
}
