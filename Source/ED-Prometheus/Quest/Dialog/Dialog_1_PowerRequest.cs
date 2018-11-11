using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;
using EnhancedDevelopment.Prometheus.Power;
using static EnhancedDevelopment.Prometheus.Core.GameComponent_Prometheus_Quest;

namespace EnhancedDevelopment.Prometheus.Quest.Dialog
{
    class Dialog_1_PowerRequest : Window
    {
        private string m_Text;
        private Vector2 m_Position;
        Building ContactSource;

        public Dialog_1_PowerRequest(string title, string text, Building contactSource)
        {
            this.ContactSource = contactSource;
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

            GameFont _Previous = Text.Font;

            Text.Font = GameFont.Tiny;
            IntVec2 _Size = new IntVec2(150, 40);
            if (Widgets.ButtonText(new Rect(inRect.xMax - _Size.x, inRect.yMax - _Size.z, _Size.x, _Size.z), "Request Aditional Relay, 300RU, 700P", true, false, true))
            {
                Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(EnumResourceType.ResourceUnits, -300);
                Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(EnumResourceType.Power, -700);
                Building_QuantumPowerRelay _PowerBuilding = (Building_QuantumPowerRelay)ThingMaker.MakeThing(ThingDef.Named("QuantumPowerRelay"), null);
                List<Thing> _Things = new List<Thing>();
                _Things.Add(_PowerBuilding);

                DropPodUtility.DropThingsNear(this.ContactSource.Position, this.ContactSource.Map, _Things);

            }
            Text.Font = _Previous;

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
