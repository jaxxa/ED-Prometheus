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

        private void InitialWindowContents(Rect totalCanvas)
        {
            int _TabSelectionHeight = 60;
            Rect _TabSelectionRect = totalCanvas.TopPartPixels(_TabSelectionHeight);


            Widgets.ButtonText(_TabSelectionRect, "TestButton", true, false, true);

            WidgetRow _ButtonWidgetRow = new WidgetRow(_TabSelectionRect.x, _TabSelectionRect.y - 5, UIDirection.RightThenDown, 99999f, 4f);
            if (_ButtonWidgetRow.ButtonText("Buildings", null, true, true))
            {
                //Find.WindowStack.Add(new Dialog_BillConfig(this, ((Thing)base.billStack.billGiver).Position));
            }

            if (_ButtonWidgetRow.ButtonText("System Status", null, true, true))
            {
                //Find.WindowStack.Add(new Dialog_BillConfig(this, ((Thing)base.billStack.billGiver).Position));
            }

            Widgets.DrawLineHorizontal(_TabSelectionRect.xMin, _TabSelectionRect.yMax, _TabSelectionRect.width);



            Rect _WindowContent = totalCanvas.BottomPartPixels(totalCanvas.height - _TabSelectionHeight);



            float m_ViewHeight = (Core.GameComponent_Excalibur.Instance.Comp_Quest.m_ShipSystems.Count() + 1) * ShipSystem.m_Height + 6f;

            Widgets.TextArea(_WindowContent.TopPartPixels(20), "Ship Status", true);

            GUI.color = Color.white;
            Rect outRect = new Rect(_WindowContent.x, _WindowContent.y + 35f, _WindowContent.width, _WindowContent.height - 35f - 35f);
            Rect viewRect = new Rect(0f, 0f, outRect.width - 16f, m_ViewHeight);

            //Above scroll view
            Widgets.DrawLineHorizontal(outRect.xMin, outRect.yMin , outRect.width);

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

            Rect _Footer = _WindowContent.BottomPartPixels(35f);

            Widgets.DrawLineHorizontal(_Footer.xMin, _Footer.yMin, _Footer.width);

            Widgets.TextArea(_Footer.LeftHalf().LeftHalf(), "Nano Materials: " + Core.GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(Core.GameComponent_Excalibur_Quest.EnumResourceType.NanoMaterials).ToString() + " / " + Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget.ToString(), true);


            Listing_Standard _listing_Standard_ShieldChargeLevelMax = new Listing_Standard();
            _listing_Standard_ShieldChargeLevelMax.Begin(_Footer.LeftHalf().RightHalf());
            _listing_Standard_ShieldChargeLevelMax.ColumnWidth = 70;
            _listing_Standard_ShieldChargeLevelMax.IntAdjuster(ref Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget, 1, 1);
            _listing_Standard_ShieldChargeLevelMax.NewColumn();
            _listing_Standard_ShieldChargeLevelMax.IntAdjuster(ref Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget, 10, 1);
            _listing_Standard_ShieldChargeLevelMax.NewColumn();
            _listing_Standard_ShieldChargeLevelMax.IntSetter(ref Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterialsTarget, 10, "Default");
            _listing_Standard_ShieldChargeLevelMax.End();


        }
    }
}
