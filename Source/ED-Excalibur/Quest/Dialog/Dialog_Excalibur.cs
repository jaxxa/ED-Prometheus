using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Quest.Dialog
{
    class Dialog_Excalibur : Window
    {
        public Dialog_Excalibur()
        {
            this.resizeable = false;
            this.optionalTitle = "E.D.S.N Exclibur";
            //this.CloseButSize = new Vector2(50, 50);

            this.doCloseButton = false;
            this.doCloseX = false;
            this.closeOnClickedOutside = false;
            this.doCloseX = true;
            

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

        //private string introText = "EDETestString".Translate();

        Vector2 m_ScrollPosition = new Vector2();

       // private float m_ViewHeight = 99999f;

        private void InitialWindowContents(Rect Canvas)
        {
            float m_ViewHeight = Core.GameComponent_Excalibur.Instance.Comp_Quest.m_ShipSystems.Count() * 106f;

            Widgets.TextArea(Canvas.TopPartPixels(20), "Ship Status", true);
            Widgets.DrawLineHorizontal(0, 20, Canvas.width);

            GUI.color = Color.white;
            Rect outRect = new Rect(0f, 35f, Canvas.width, Canvas.height - 35f - 35f);
            Rect viewRect = new Rect(0f, 0f, outRect.width - 16f, m_ViewHeight);

            Widgets.BeginScrollView(outRect, ref m_ScrollPosition, viewRect, true);

            float num = 0f;
            for (int i = 0; i < Core.GameComponent_Excalibur.Instance.Comp_Quest.m_ShipSystems.Count(); i++)
            {
                ShipSystem _BuildingInProgress = Core.GameComponent_Excalibur.Instance.Comp_Quest.m_ShipSystems[i];
                Rect rect3 = _BuildingInProgress.DoInterface(0f, num, viewRect.width, i);
                //if (!bill.DeletedOrDereferenced && Mouse.IsOver(rect3))
                //{
                //    result = bill;
                //}
                num += rect3.height + 6f;
                Widgets.DrawLineHorizontal(viewRect.x, num, viewRect.width);
            }


            Widgets.EndScrollView();

            Rect _Footer = Canvas.BottomPartPixels(35f);
            Widgets.TextArea(_Footer.LeftHalf().LeftHalf(), "Nano Materials: " + Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterials.ToString() + " / " + Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget.ToString(), true);


            Listing_Standard _listing_Standard_ShieldChargeLevelMax = new Listing_Standard();
            _listing_Standard_ShieldChargeLevelMax.Begin(_Footer.LeftHalf().RightHalf());
            _listing_Standard_ShieldChargeLevelMax.ColumnWidth = 70;
            _listing_Standard_ShieldChargeLevelMax.IntAdjuster(ref Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget, 1, 1);
            _listing_Standard_ShieldChargeLevelMax.NewColumn();
            _listing_Standard_ShieldChargeLevelMax.IntAdjuster(ref Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget, 10, 1);
            _listing_Standard_ShieldChargeLevelMax.NewColumn();
            _listing_Standard_ShieldChargeLevelMax.IntSetter(ref Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget, 10, "Default");
            _listing_Standard_ShieldChargeLevelMax.End();

            Widgets.DrawLineHorizontal(0, Canvas.height - 35f, Canvas.width);

        }
    }
}
