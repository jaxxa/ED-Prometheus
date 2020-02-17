using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;
using EnhancedDevelopment.Prometheus.Core;

namespace EnhancedDevelopment.Prometheus.Quest.Dialog
{
    class Dialog_Prometheus : Window
    {

        #region Enumerations

        public enum EnumDialogTabSelection
        {
            SystemStatus,
            Fabrication
        }

        #endregion

        #region Fields

        static Vector2 m_ScrollPosition = new Vector2();
        int m_TabSelectionHeight = 30;
        int m_FooterSectionHeight = 20;

        private EnumDialogTabSelection m_CurrentTab;

        #endregion

        #region Constructor

        public Dialog_Prometheus()
        {
            this.resizeable = false;

            this.optionalTitle = "E.D.S.N Exclibur - Fabrication";
            this.m_CurrentTab = EnumDialogTabSelection.Fabrication;

            //this.CloseButSize = new Vector2(50, 50);

            this.doCloseButton = false;
            this.doCloseX = false;
            this.closeOnClickedOutside = false;
            this.doCloseX = true;


            //this.SetInitialSizeAndPosition();
        }

        #endregion

        #region Override Methods

        public override void DoWindowContents(Rect inRect)
        {

            this.DoGuiWholeWindowContents(inRect);

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

        #endregion

        #region Gui Methods

        private void DoGuiWholeWindowContents(Rect totalCanvas)
        {
            // Headder --------------------------------------------------------
            Rect _TabSelectionRect = totalCanvas.TopPartPixels(m_TabSelectionHeight);
            this.DoGuiHeadder(_TabSelectionRect);

            // Main Content ---------------------------------------------------

            Rect _WindowContent = new Rect(totalCanvas.xMin, _TabSelectionRect.yMax, totalCanvas.width, totalCanvas.height - m_TabSelectionHeight - m_FooterSectionHeight);

            if (this.m_CurrentTab == EnumDialogTabSelection.SystemStatus)
            {
                this.DoGuiSystemStatus(_WindowContent);
            }
            else if (this.m_CurrentTab == EnumDialogTabSelection.Fabrication)
            {
                Dialog_Prometheus.DoGuiFabrication(_WindowContent, false);
            }

            // Footer (System Status) -----------------------------------------

            Rect _Footer = totalCanvas.BottomPartPixels(m_FooterSectionHeight);
            this.DoGuiFooter(_Footer);

        }

        public void DoGuiHeadder(Rect rectContentWindow)
        {

            // Headder to Select Tabs -----------------------------------------
            

            WidgetRow _ButtonWidgetRow = new WidgetRow(rectContentWindow.x, rectContentWindow.y, UIDirection.RightThenDown, 99999f, 4f);
            if (_ButtonWidgetRow.ButtonText("Fabrication", null, true, true))
            {
                this.optionalTitle = "E.D.S.N Exclibur - Fabrication";
                this.m_CurrentTab = EnumDialogTabSelection.Fabrication;
            }

            if (_ButtonWidgetRow.ButtonText("System Status", null, true, true))
            {
                this.optionalTitle = "E.D.S.N Exclibur - System Status";
                this.m_CurrentTab = EnumDialogTabSelection.SystemStatus;
            }
            Widgets.DrawLineHorizontal(rectContentWindow.xMin, rectContentWindow.yMax, rectContentWindow.width);

        }

        public void DoGuiFooter(Rect rectContentWindow)
        {

            Widgets.DrawLineHorizontal(rectContentWindow.xMin, rectContentWindow.yMin, rectContentWindow.width);
            Widgets.TextArea(rectContentWindow, GameComponent_Prometheus_Quest.GetSingleLineResourceStatus(), true);

        }

        public void DoGuiSystemStatus(Rect rectContentWindow)
        {

            float _ViewContentHeight = (Core.GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems.Count() + 1) * ShipSystem.m_Height + 6f;


            GUI.color = Color.white;
            Rect outRect = new Rect(rectContentWindow.x, rectContentWindow.y, rectContentWindow.width, rectContentWindow.height - m_FooterSectionHeight);
            Rect viewRect = new Rect(0f, 0f, outRect.width - 16f, _ViewContentHeight);

            //Above scroll view
            Widgets.DrawLineHorizontal(outRect.xMin, outRect.yMin, outRect.width);

            Widgets.BeginScrollView(outRect, ref m_ScrollPosition, viewRect, true);

            float num = 0f;
            for (int i = 0; i < Core.GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems.Count(); i++)
            {
                ShipSystem _BuildingInProgress = Core.GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems[i];
                Rect rect3 = _BuildingInProgress.DoInterface(0f, num, viewRect.width, i);
                //if (!bill.DeletedOrDereferenced && Mouse.IsOver(rect3))
                //{
                //    result = bill;
                //}
                num += rect3.height + 6f;
                Widgets.DrawLineHorizontal(viewRect.x, num, viewRect.width);
            }

            Widgets.EndScrollView();
        }

        public static void DoGuiFabrication(Rect rectContentWindow, bool showOnlyActiveThings, IntVec3 dropLocation = new IntVec3(), Map dropMap = null)
        {
            Core.GameComponent_Prometheus.Instance.Comp_Fabrication.AddNewBuildingsUnderConstruction();

            GameComponent_Prometheus.Instance.Comp_Fabrication.DoListing(rectContentWindow, ref Dialog_Prometheus.m_ScrollPosition, ref viewHeight, showOnlyActiveThings, dropLocation, dropMap);

        }

        static float viewHeight = 1000f;

        #endregion

    }
}
