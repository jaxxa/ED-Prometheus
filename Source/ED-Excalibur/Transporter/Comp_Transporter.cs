using EnhancedDevelopment.Excalibur.Core;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Transporter
{
    class Comp_Transporter : ThingComp
    {

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            //return base.CompGetGizmosExtra();

            //Add the stock Gizmoes
            foreach (var g in base.CompGetGizmosExtra())
            {
                yield return g;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.ProvideMatrials();
                //    act.icon = UI_ADD_RESOURCES;
                act.defaultLabel = "ProvideMatrials";
                act.defaultDesc = "ProvideMatrials";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }


            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.TransportColonists();
                //    act.icon = UI_ADD_RESOURCES;
                act.defaultLabel = "TransportColonists";
                act.defaultDesc = "TransportColonists";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.TransportThings();
                //    act.icon = UI_ADD_RESOURCES;
                act.defaultLabel = "TransportThings";
                act.defaultDesc = "TransportThings";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.TransportRecall();
                //    act.icon = UI_ADD_RESOURCES;
                act.defaultLabel = "TransportRecall";
                act.defaultDesc = "TransportRecall";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }

        }

        List<Thing> m_TransportBuffer = new List<Thing>();

        private void ProvideMatrials()
        {
            List<Thing> _FoundThings = GenRadial.RadialDistinctThingsAround(this.parent.Position, this.parent.Map, 5, true).Where(x => x.def.category == ThingCategory.Item).Where(x => x.Spawned).Where(x => x.def.defName == "Steel").ToList();

            if (_FoundThings.Any())
            {
                _FoundThings.ForEach(_x =>
                {

                    GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(_x.stackCount);
                    this.DisplayTransportEffect(_x);

                    _x.DeSpawn();

                    // Tell the MapDrawer that here is something thats changed
                    this.parent.Map.mapDrawer.MapMeshDirty(_x.Position, MapMeshFlag.Things, true, false);
                });


            }

        }

        private void TransportColonists()
        {
            // List<Thing> _FoundThings = GenRadial.RadialDistinctThingsAround(this.parent.Position, this.parent.Map, 5, true).Where(x => x.def.category == ThingCategory.Item).Where(x => x.Spawned).Where(x => x.def.defName == "Steel").ToList();


            List<Pawn> _Pawns = this.parent.Map.mapPawns.PawnsInFaction(Faction.OfPlayer).Where<Pawn>(t => t.Position.InHorDistOf(this.parent.Position, 5)).ToList();

            if (_Pawns.Any())
            {
                _Pawns.ForEach(_x =>
                {

                    this.m_TransportBuffer.Add(_x);
                    //GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(_x.stackCount);
                    this.DisplayTransportEffect(_x);

                    _x.DeSpawn();

                    // Tell the MapDrawer that here is something thats changed
                    this.parent.Map.mapDrawer.MapMeshDirty(_x.Position, MapMeshFlag.Things, true, false);
                });


            }
        }

        private void TransportThings()
        {

            List<Thing> _FoundThings = GenRadial.RadialDistinctThingsAround(this.parent.Position, this.parent.Map, 5, true).Where(x => x.def.category == ThingCategory.Item).Where(x => x.Spawned).ToList();

            if (_FoundThings.Any())
            {
                _FoundThings.ForEach(_x =>
                {


                    this.m_TransportBuffer.Add(_x);

                    //GameComponent_Excalibur.Instance.Comp_Quest.AddReserveMaterials(_x.stackCount);
                    this.DisplayTransportEffect(_x);

                    _x.DeSpawn();

                    // Tell the MapDrawer that here is something thats changed
                    this.parent.Map.mapDrawer.MapMeshDirty(_x.Position, MapMeshFlag.Things, true, false);
                });

            }
            
        }

        private void TransportRecall()
        {

            this.m_TransportBuffer.ForEach(_X =>
            {
                GenPlace.TryPlaceThing(_X, this.parent.Position, this.parent.Map, ThingPlaceMode.Near);
                this.DisplayTransportEffect(_X);
            }
            );

            this.m_TransportBuffer.Clear();
        }

        private void DisplayTransportEffect(Thing thingToTransport)
        {
            MoteMaker.MakeStaticMote(thingToTransport.Position, thingToTransport.Map, ThingDefOf.Mote_ExplosionFlash, 10);
        }

    }
}
