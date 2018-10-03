using EnhancedDevelopment.Prometheus.Core;
using EnhancedDevelopment.Prometheus.Power;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    class ITab_Transponder : ITab
    {

        private static readonly Vector2 WinSize = new Vector2(500f, 400f);

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

        public ITab_Transponder()
        {
            base.size = ITab_Transponder.WinSize;
            base.labelKey = "Transponder";
        }

        protected override void FillTab()
        {

            Vector2 winSize = ITab_Transponder.WinSize;
            float x = winSize.x;
            Vector2 winSize2 = ITab_Transponder.WinSize;
            Rect rect = new Rect(0f, 0f, x, winSize2.y).ContractedBy(10f);

            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = rect.width;
            _Listing_Standard.Begin(rect);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(GameComponent_Prometheus_Quest.GetSingleLineResourceStatus());

            _Listing_Standard.Gap(12f);

            _Listing_Standard.End();
        }

    } //Class

}