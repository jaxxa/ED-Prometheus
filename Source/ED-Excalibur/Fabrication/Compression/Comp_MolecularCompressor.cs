using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Fabrication
{
    class Comp_MolecularCompressor : ThingComp
    {

        public CompProperties_MolecularCompressor Properties;

        const int STUFF_AMMOUNT_REQUIRED = 10;

        CompPowerTrader m_Power;




        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            this.Properties = ((CompProperties_MolecularCompressor)this.props);

            this.m_Power = this.parent.GetComp<CompPowerTrader>();
        }

        public override void CompTick()
        {
            base.CompTick();

            if (this.m_Power.PowerOn)
            {
                this.TrySpawnReinforcedStuff();
            }

            Log.Message("Tick");
        }


        private void TrySpawnReinforcedStuff()
        {

            //Thing _NewResource = ThingMaker.MakeThing(ThingDef.Named("PlasteelReinforced"));
            Thing _OldResourceThing = this.GetValidThingStack();
            if (_OldResourceThing == null) { return; }

            ThingDef _NewResourceDef = this.GetReinforcedVersion(_OldResourceThing);
            if (_NewResourceDef == null) { return; }

            _OldResourceThing.SplitOff(Comp_MolecularCompressor.STUFF_AMMOUNT_REQUIRED);

            Thing _NewResource = ThingMaker.MakeThing(_NewResourceDef);
            GenPlace.TryPlaceThing(_NewResource, parent.InteractionCell, this.parent.Map, ThingPlaceMode.Near);

        }


        private Thing GetValidThingStack()
        {
            // List<IntVec3> _Cells = Enumerable.ToList<IntVec3>(Enumerable.Where<IntVec3>(GenAdj.CellsAdjacentCardinal((this.parent), (Func<IntVec3, bool>)(c => GenGrid.InBounds(c, this.parent.Map))));

            IEnumerable<IntVec3> _Cells = GenAdj.CellsAdjacentCardinal((this.parent));

            List<Thing> _CloseThings = new List<Thing>();

            foreach (IntVec3 _Cell in _Cells)
            {
                _CloseThings.AddRange(GridsUtility.GetThingList(_Cell, this.parent.Map));
            }


            foreach (Thing _TempThing in _CloseThings)
            {

                if (_TempThing.stackCount >= Comp_MolecularCompressor.STUFF_AMMOUNT_REQUIRED)
                {
                    ThingDef _ReinforcedVersion = this.GetReinforcedVersion(_TempThing);

                    if (_ReinforcedVersion != null)
                    {
                        return _TempThing;
                    }
                }
            }

            return null;
        }


        ThingDef GetReinforcedVersion(Thing sourceStuff)
        {
            string _DefName = "Compressed" + sourceStuff.def.defName;

            return ThingDef.Named(_DefName);

        }
    }
}
