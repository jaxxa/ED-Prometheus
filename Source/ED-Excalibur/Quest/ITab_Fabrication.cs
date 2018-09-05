using EnhancedDevelopment.Excalibur.Core;
using EnhancedDevelopment.Excalibur.Power;
using EnhancedDevelopment.Excalibur.Quest.Dialog;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest
{
    class ITab_Fabrication : ITab
    {

        //private Comp_ShieldGenerator _CachedComp;

        //public ITab_ShieldGenerator() : base()
        //{
        //    _CachedComp = 

        //}

        private static readonly Vector2 WinSize = new Vector2(420f, 480f);

        private Comp_EDSNTransponder SelectedCompTransponder
        {
            get
            {
                Thing thing = Find.Selector.SingleSelectedThing;
                MinifiedThing minifiedThing = thing as MinifiedThing;
                if (minifiedThing != null)
                {
                    thing = minifiedThing.InnerThing;
                }
                if (thing == null)
                {
                    return null;
                }
                return thing.TryGetComp<Comp_EDSNTransponder>();
            }
        }

        public override bool IsVisible
        {
            get
            {
                return this.SelectedCompTransponder != null;
            }
        }

        public ITab_Fabrication()
        {
            base.size = ITab_Fabrication.WinSize;
            base.labelKey = "Fabrication";
            //base.tutorTag = "ShieldGenenerator";
        }

        protected override void FillTab()
        {

            Vector2 _WindowSize = ITab_Fabrication.WinSize;
            Rect _DrawingSpace = new Rect(0f, 0f, _WindowSize.x, _WindowSize.y).ContractedBy(10f);

            Rect _MainWindow = _DrawingSpace.TopPartPixels(_DrawingSpace.height - 25);
            Rect _InfoBar = _DrawingSpace.BottomPartPixels(25);


            Dialog_Excalibur.DoGuiFabrication(_MainWindow, 
                                           true,
                                           this.SelectedCompTransponder.parent.Position, 
                                           this.SelectedCompTransponder.parent.Map);

        }
        
    } //Class

}