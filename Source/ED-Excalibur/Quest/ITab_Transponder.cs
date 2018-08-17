using EnhancedDevelopment.Excalibur.Core;
using EnhancedDevelopment.Excalibur.Power;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest
{
    class ITab_Transponder : ITab
    {

        //private Comp_ShieldGenerator _CachedComp;

        //public ITab_ShieldGenerator() : base()
        //{
        //    _CachedComp = 

        //}


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
            base.labelKey = "TransponderTab";
            //base.tutorTag = "ShieldGenenerator";
        }

        protected override void FillTab()
        {

            Vector2 winSize = ITab_Transponder.WinSize;
            float x = winSize.x;
            Vector2 winSize2 = ITab_Transponder.WinSize;
            Rect rect = new Rect(0f, 0f, x, winSize2.y).ContractedBy(10f);
            //Rect rect2 = rect;
            //Text.Font = GameFont.Medium;
            //Widgets.Label(rect2, "Shield Generator Label Rec2");
            //if (ITab_Art.cachedImageSource != this.SelectedCompArt || ITab_Art.cachedTaleRef != this.SelectedCompArt.TaleRef)
            //{
            //    ITab_Art.cachedImageDescription = this.SelectedCompArt.GenerateImageDescription();
            //    ITab_Art.cachedImageSource = this.SelectedCompArt;
            //    ITab_Art.cachedTaleRef = this.SelectedCompArt.TaleRef;
            //}
            //Rect rect3 = rect;
            //rect3.yMin += 35f;
            //Text.Font = GameFont.Small;
            //Widgets.Label(rect3, "ShieldGenerator Rec3");

            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = 250f;
            _Listing_Standard.Begin(rect);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Power: " + GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.Power).ToString());
            _Listing_Standard.Label("Materials: " + GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits).ToString());
            _Listing_Standard.Label("Buildings: " + GameComponent_Excalibur.Instance.Comp_Fabrication.BuildingsUnderConstruction.Count.ToString());

            _Listing_Standard.Gap(12f);

            //if (_Listing_Standard.ButtonText("Deploy Power Relay", "Hightlight"))
            //{
            //    GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(7);
            //    GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding("QuantumPowerRelay", this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
            //}

            //if (_Listing_Standard.ButtonText("Deploy Shield Charger", "Hightlight"))
            //{
            //    GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(7);
            //    GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding("Building_Shield_Charger", this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
            //}

            //if (_Listing_Standard.ButtonText("Deploy Shield Generator", "Hightlight"))
            //{
            //    GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(7);
            //    GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding("Building_ShieldStandard", this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
            //}

            _Listing_Standard.End();
        }

    } //Class

}