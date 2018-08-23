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

            this.ThingForDeployment.Where(b => b.UnitsRequestedAditional >= 0).ToList().ForEach(b =>
            {
                //this.BuildingsUnderConstruction.Remove(b);
                if (!b.ConstructionInProgress)
                {
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.ResourceUnits, b.NeededResources);
                    GameComponent_Excalibur.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Excalibur_Quest.EnumResourceType.Power, b.NeededPower);
                }
            });

            //Gets the Currently under construction Thing
            ThingForDeployment _CurrentlyUnderConstruction = this.ThingForDeployment.FirstOrFallback(t => t.ConstructionInProgress, null);

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
                ThingForDeployment _ThingToStart = this.ThingForDeployment.Where(b => b.UnitsRequestedAditional >= 1).RandomElement();

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

        public List<ThingForDeployment> ThingForDeployment = new List<ThingForDeployment>();


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
            for (int i = 0; i < ThingForDeployment.Count; i++)
            {
                ThingForDeployment _BuildingInProgress = this.ThingForDeployment[i];
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

            DefDatabase<ThingDef>.AllDefs.ToList().ForEach(x =>
            {
                Fabrication.CompProperties_Fabricated _FabricationCompPropeties = x.GetCompProperties<Fabrication.CompProperties_Fabricated>();
                if (_FabricationCompPropeties != null && x.researchPrerequisites.All(r => r.IsFinished || string.Equals(r.defName, "Research_ED_Excalibur_Quest_Unlock")))
                {
                    if (!this.ThingForDeployment.Any(b => string.Equals(b.defName, x.defName)))
                    {
                        ThingForDeployment _NewThing = new ThingForDeployment(x.defName, x.label);
                        _NewThing.WorkRemaining = _FabricationCompPropeties.RequiredWork;
                        _NewThing.NeededWork = _FabricationCompPropeties.RequiredWork;
                        _NewThing.NeededPower = _FabricationCompPropeties.RequiredPower;
                        _NewThing.NeededResources = _FabricationCompPropeties.RequiredMaterials;

                        this.ThingForDeployment.Add(_NewThing);
                    }

                }
            });

            this.ThingForDeployment.OrderBy(x => x.label);
        }
    }

} // Class


