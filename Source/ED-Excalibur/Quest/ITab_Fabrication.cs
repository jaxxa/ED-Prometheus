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


        public TargetingParameters targetingParams = new TargetingParameters();

        //private Comp_ShieldGenerator _CachedComp;


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

        public ITab_Fabrication() : base()
        {
            base.size = ITab_Fabrication.WinSize;
            base.labelKey = "Fabrication";
            //base.tutorTag = "ShieldGenenerator";

            targetingParams.canTargetLocations = true;
        }


        protected override void FillTab()
        {
            int _TargetingBarHeight = 30;

            Vector2 _WindowSize = ITab_Fabrication.WinSize;
            Rect _DrawingSpace = new Rect(0f, 0f, _WindowSize.x, _WindowSize.y).ContractedBy(10f);

            Rect _MainWindow = _DrawingSpace.TopPartPixels(_DrawingSpace.height - _TargetingBarHeight);
            Rect _TargetingBar = _DrawingSpace.BottomPartPixels(_TargetingBarHeight);
            
            if (Widgets.ButtonText(_TargetingBar.LeftHalf(), "Targeting", true, true, true))
            {
                
                Find.Targeter.BeginTargeting(this.targetingParams, delegate (LocalTargetInfo target)
                {
                    this.SelectedCompTransponder.DropLocation = target.Cell;
                }, null, null, null);
            }

            Widgets.Label(_TargetingBar.RightHalf(), this.SelectedCompTransponder.DropLocation.x + " " + this.SelectedCompTransponder.DropLocation.z);

            IntVec3 _DropLocation = this.SelectedCompTransponder.parent.Position;

            if (!IntVec3.Equals(this.SelectedCompTransponder.DropLocation, IntVec3.Invalid))
            {
                _DropLocation = this.SelectedCompTransponder.DropLocation;
            }


            Dialog_Excalibur.DoGuiFabrication(_MainWindow,
                                              true,
                                              _DropLocation,
                                              this.SelectedCompTransponder.parent.Map);

        }

    } //Class

}