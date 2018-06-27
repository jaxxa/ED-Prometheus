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




        private static readonly Vector2 WinSize = new Vector2(500f, 400f);

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


            Func<List<FloatMenuOption>> recipeOptionsMaker = delegate
            {

                List<FloatMenuOption> _List = new List<FloatMenuOption>();

                DefDatabase<ThingDef>.AllDefs.ToList().ForEach(x =>
                {
                    Fabrication.CompProperties_Fabricated _FabricationCompPropeties = x.GetCompProperties<Fabrication.CompProperties_Fabricated>();
                    if (_FabricationCompPropeties != null)
                    {
                        //Log.Message(x.defName);

                        _List.Add(new FloatMenuOption(x.defName, delegate {
                            GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding(x, this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
                        }));
                    }
                }
                );

                //_List.Add(new FloatMenuOption("Add Relay", delegate
                //{
                //    GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding("QuantumPowerRelay", this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
                //}));
                //_List.Add(new FloatMenuOption("Add Building_Shield_Charger", delegate
                //{
                //    GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding("Building_Shield_Charger", this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
                //}));
                //_List.Add(new FloatMenuOption("Add Building_ShieldStandard", delegate
                //{
                //    GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding("Building_ShieldStandard", this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
                //}));

                return _List;
            };

            GameComponent_Excalibur.Instance.Comp_Fabrication.DoListing(_DrawingSpace, recipeOptionsMaker, ref scrollPosition, ref viewHeight);
            
        }
        
    } //Class

}