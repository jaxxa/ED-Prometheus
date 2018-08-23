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
    class ITab_Fabrication : ITab
    {

        //private Comp_ShieldGenerator _CachedComp;

        //public ITab_ShieldGenerator() : base()
        //{
        //    _CachedComp = 

        //}

        private static readonly Vector2 WinSize = new Vector2(420f, 480f);

        private float viewHeight = 1000f;

        private Vector2 scrollPosition = default(Vector2);

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
            
            Func<List<FloatMenuOption>> _RecipeOptionsMaker = delegate
            {

                List<FloatMenuOption> _List = new List<FloatMenuOption>();

                DefDatabase<ThingDef>.AllDefs.ToList().ForEach(x =>
                {
                    Fabrication.CompProperties_Fabricated _FabricationCompPropeties = x.GetCompProperties<Fabrication.CompProperties_Fabricated>();
                    if (_FabricationCompPropeties != null && x.researchPrerequisites.All(r => r.IsFinished || string.Equals(r.defName, "Research_ED_Excalibur_Quest_Unlock")))
                    {
                                                                        
                        _List.Add(new FloatMenuOption(x.label + " - RU: " + _FabricationCompPropeties.RequiredMaterials + " P: " + _FabricationCompPropeties.RequiredPower , delegate {
                            //GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding(x, this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
                           // GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding(x);
                        },MenuOptionPriority.Default,null,null,29f, (Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, x)));
                    }
                }
                );

                return _List;
            };

            //GameComponent_Excalibur.Instance.Comp_Fabrication.DoListing(_MainWindow, _RecipeOptionsMaker, ref scrollPosition, ref viewHeight);
            GameComponent_Excalibur.Instance.Comp_Fabrication.DoListing(_MainWindow, ref scrollPosition, ref viewHeight);


            Widgets.TextArea(_InfoBar, "RU:" + GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits) + " Power: " + GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.Power).ToString(), true);
            

        }
        
    } //Class

}