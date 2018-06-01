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
        }

        public override void TickOnInterval()
        {
            if (this.BuildingsUnderConstruction.Any())
            {

                BuildingInProgress _BuildingToSpawn = this.BuildingsUnderConstruction.FirstOrDefault();
                _BuildingToSpawn.WorkRemaining -= 1;

                if (_BuildingToSpawn.WorkRemaining <= 0)
                {
                    Log.Message("Dropping");

                    _BuildingToSpawn.NumberOfRequestsRemailing -= 1;
                    if (_BuildingToSpawn.NumberOfRequestsRemailing <= 0)
                    {
                        this.BuildingsUnderConstruction.Remove(_BuildingToSpawn);
                    }

                    _BuildingToSpawn.WorkRemaining = _BuildingToSpawn.NeededWork;
                    GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(-_BuildingToSpawn.NeededResources);
                    GameComponent_Excalibur.Instance.Comp_Quest.AddReservePower(-_BuildingToSpawn.NeededPower);

                    List<Thing> _Things = _BuildingToSpawn.InitiateDrop();
                    DropPodUtility.DropThingsNear(_BuildingToSpawn.DestinationPosition, _BuildingToSpawn.DestinationMap, _Things);

                    if (this.BuildingsUnderConstruction.Any())
                    {

                        BuildingInProgress _NextBuilding = this.BuildingsUnderConstruction.First();
                        _BuildingToSpawn.WorkRemaining = 100;
                    }



                }
            }

        }

        //-------------------------------------------

        public List<BuildingInProgress> BuildingsUnderConstruction = new List<BuildingInProgress>();

        public void OrderBuilding(ThingDef buildingDef, IntVec3 position, Map map)
        {
            CompProperties_Fabricated _Properties = buildingDef.GetCompProperties<CompProperties_Fabricated>();
            BuildingInProgress _NewBuilding = new BuildingInProgress(buildingDef.defName, map, position);
            _NewBuilding.WorkRemaining = _Properties.RequiredWork;
            _NewBuilding.NeededWork = _Properties.RequiredWork;
            _NewBuilding.NeededPower = _Properties.RequiredPower;
            _NewBuilding.NeededResources = _Properties.RequiredMaterials;

            BuildingsUnderConstruction.Add(_NewBuilding);
        }









        //-------------------------UI --------------------


        public void DoListing(Rect rect, Func<List<FloatMenuOption>> recipeOptionsMaker, ref Vector2 scrollPosition, ref float viewHeight)
        {
            Bill result = null;
            GUI.BeginGroup(rect);
            Text.Font = GameFont.Small;
            if (BuildingsUnderConstruction.Count < 15)
            {
                Rect rect2 = new Rect(0f, 0f, 150f, 29f);
                if (Widgets.ButtonText(rect2, "AddBuilding".Translate(), true, false, true))
                {
                    Find.WindowStack.Add(new FloatMenu(recipeOptionsMaker()));
                }
                UIHighlighter.HighlightOpportunity(rect2, "AddBuilding");
            }
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = Color.white;
            Rect outRect = new Rect(0f, 35f, rect.width, rect.height - 35f);
            Rect viewRect = new Rect(0f, 0f, outRect.width - 16f, viewHeight);
            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect, true);
            float num = 0f;
            for (int i = 0; i < BuildingsUnderConstruction.Count; i++)
            {
                BuildingInProgress _BuildingInProgress = this.BuildingsUnderConstruction[i];
                Rect rect3 = _BuildingInProgress.DoInterface(0f, num, viewRect.width, i);
                //if (!bill.DeletedOrDereferenced && Mouse.IsOver(rect3))
                //{
                //    result = bill;
                //}
                num += rect3.height + 6f;
            }
            if (Event.current.type == EventType.Layout)
            {
                viewHeight = num + 60f;
            }
            Widgets.EndScrollView();
            GUI.EndGroup();
        }







    } // Class
}

