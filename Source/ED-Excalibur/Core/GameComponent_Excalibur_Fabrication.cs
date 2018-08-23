using EnhancedDevelopment.Excalibur.Fabrication;
using EnhancedDevelopment.Excalibur.Settings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Fabrication : GameComponent_BaseClass
    {
        public override void ExposeData()
        {
            //throw new NotImplementedException();
        }

        public override int GetTickInterval()
        {
            return 30;
        } //GetTickInterval

        public override void TickOnInterval()
        {

            this.BuildingsUnderConstruction.Where(b => b.UnitsRequestedAditional >= 0).ToList().ForEach(b =>
            {
                //this.BuildingsUnderConstruction.Remove(b);
                if (!b.ConstructionInProgress)
                {
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits, b.NeededResources);
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.Power, b.NeededPower);
                }
            });

            //Gets the Currently under construction Thing
            ThingForDeployment _CurrentlyUnderConstruction = this.BuildingsUnderConstruction.FirstOrFallback(t => t.ConstructionInProgress, null);

            if (_CurrentlyUnderConstruction != null)
            {
                //Continue currently under construction
                _CurrentlyUnderConstruction.WorkRemaining--;

                //Check if Finished
                if (_CurrentlyUnderConstruction.WorkRemaining <= 0)
                {
                    //Finish one Unit
                    _CurrentlyUnderConstruction.ConstructionInProgress = false;
                    _CurrentlyUnderConstruction.UnitsAvalable++;
                    _CurrentlyUnderConstruction.WorkRemaining = _CurrentlyUnderConstruction.NeededWork;
                }

            }
            else
            {
                ThingForDeployment _ThingToStart = this.BuildingsUnderConstruction.Where(b => b.UnitsRequestedAditional >= 1).RandomElement();

                if (_ThingToStart != null &&
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.Power) >= _ThingToStart.NeededPower &&
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits) >= _ThingToStart.NeededResources)
                {

                    _ThingToStart.ConstructionInProgress = true;
                    _ThingToStart.UnitsRequestedAditional--;
                    _ThingToStart.WorkRemaining = _ThingToStart.NeededWork;

                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits, -_ThingToStart.NeededResources);
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.Power, -_ThingToStart.NeededPower);
                }

            }


        } //TickOnInterval

        //-------------------------------------------

        public List<ThingForDeployment> BuildingsUnderConstruction = new List<ThingForDeployment>();

        /*
        public void OrderBuilding(ThingDef buildingDef)
        {
            CompProperties_Fabricated _Properties = buildingDef.GetCompProperties<CompProperties_Fabricated>();
            ThingForDeployment _NewBuilding = new ThingForDeployment(buildingDef.defName, buildingDef.label);
            _NewBuilding.WorkRemaining = _Properties.RequiredWork;
            _NewBuilding.NeededWork = _Properties.RequiredWork;
            _NewBuilding.NeededPower = _Properties.RequiredPower;
            _NewBuilding.NeededResources = _Properties.RequiredMaterials;

            BuildingsUnderConstruction.Add(_NewBuilding);
        } //OrderBuilding
        */

        //-------------------------UI --------------------


        public void DoListing(Rect rect, ref Vector2 scrollPosition, ref float viewHeight)
        {
            //Bill result = null;
            //GUI.BeginGroup(rect);
            //Text.Font = GameFont.Small;
            //if (BuildingsUnderConstruction.Count < 15)
            //{
            //    Rect rect2 = new Rect(0f, 0f, 150f, 29f);
            //    if (Widgets.ButtonText(rect2, "AddBuilding".Translate(), true, false, true))
            //    {
            //        Log.Message("Click");
            //        Find.WindowStack.Add(new FloatMenu(recipeOptionsMaker()));
            //    }
            //    UIHighlighter.HighlightOpportunity(rect2, "AddBuilding");
            //}
            //Text.Anchor = TextAnchor.UpperLeft;
            //GUI.color = Color.white;
            Rect outRect = new Rect(0f, 35f, rect.width, rect.height - 35f);
            Rect viewRect = new Rect(0f, 0f, outRect.width - 16f, viewHeight);
            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect, true);
            float num = 0f;
            for (int i = 0; i < BuildingsUnderConstruction.Count; i++)
            {
                ThingForDeployment _BuildingInProgress = this.BuildingsUnderConstruction[i];
                Rect rect3 = _BuildingInProgress.DoInterface(0f, num, viewRect.width, i);
                //if (!bill.DeletedOrDereferenced && Mouse.IsOver(rect3))
                //{
                //    result = bill;
                //}
                num += rect3.height + 6f;
                Widgets.DrawLineHorizontal(viewRect.x, num, viewRect.width);
            }
            if (Event.current.type == EventType.Layout)
            {
                viewHeight = num + 60f;
            }
            Widgets.EndScrollView();
            //GUI.EndGroup();
        } //DoListing

        public void AddNewBuildingsUnderConstruction()
        {
            //List<FloatMenuOption> _List = new List<FloatMenuOption>();

            DefDatabase<ThingDef>.AllDefs.ToList().ForEach(x =>
            {
                Fabrication.CompProperties_Fabricated _FabricationCompPropeties = x.GetCompProperties<Fabrication.CompProperties_Fabricated>();
                if (_FabricationCompPropeties != null && x.researchPrerequisites.All(r => r.IsFinished || string.Equals(r.defName, "Research_ED_Excalibur_Quest_Unlock")))
                {
                    //BuildingInProgress _Temp = new BuildingInProgress(x, x.label);
                    if (!this.BuildingsUnderConstruction.Any(b => string.Equals(b.defName, x.defName)))
                    {
                        ThingForDeployment _NewThing = new ThingForDeployment(x.defName, x.label);
                        _NewThing.WorkRemaining = _FabricationCompPropeties.RequiredWork;
                        _NewThing.NeededWork = _FabricationCompPropeties.RequiredWork;
                        _NewThing.NeededPower = _FabricationCompPropeties.RequiredPower;
                        _NewThing.NeededResources = _FabricationCompPropeties.RequiredMaterials;

                        this.BuildingsUnderConstruction.Add(_NewThing);
                    }
                    /*
                _List.Add(new FloatMenuOption(x.label + " - RU: " + _FabricationCompPropeties.RequiredMaterials + " P: " + _FabricationCompPropeties.RequiredPower, delegate
                {
                        // GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding(x, this.SelectedCompTransponder.parent.Position, this.SelectedCompTransponder.parent.Map);
                        GameComponent_Excalibur.Instance.Comp_Fabrication.OrderBuilding(x);
                }, MenuOptionPriority.Default, null, null, 29f, (Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, x)));
    */
                }
            });

            this.BuildingsUnderConstruction.OrderBy(x => x.label);
        }
    }

} // Class


