using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Transporter : GameComponent_BaseClass, IThingHolder
    {

        //public List<Thing> TransportBuffer = new List<Thing>();

        public ThingOwner innerContainer;

        bool savePawnsWithReferenceMode = false;

        private List<Thing> tmpThings = new List<Thing>();

        private List<Pawn> tmpSavedPawns = new List<Pawn>();


        public GameComponent_Excalibur_Transporter()
        {
            this.innerContainer = new ThingOwner<Thing>(this);
        }

        public override void ExposeData()
        {
            //Scribe_Deep.Look<List<Thing>>(ref this.TransportBuffer, "TransportBuffer");
            //Scribe_Collections.Look<Thing>(ref this.TransportBuffer, "TransportBuffer", LookMode.Deep);



            if (this.savePawnsWithReferenceMode && Scribe.mode == LoadSaveMode.Saving)
            {
                this.tmpThings.Clear();
                this.tmpThings.AddRange(this.innerContainer);
                this.tmpSavedPawns.Clear();
                for (int i = 0; i < this.tmpThings.Count; i++)
                {
                    Pawn pawn = this.tmpThings[i] as Pawn;
                    if (pawn != null)
                    {
                        this.innerContainer.Remove(pawn);
                        this.tmpSavedPawns.Add(pawn);
                    }
                }
                this.tmpThings.Clear();
            }
            //Scribe_Values.Look<bool>(ref this.savePawnsWithReferenceMode, "savePawnsWithReferenceMode", false, false);
            if (this.savePawnsWithReferenceMode)
            {
                Scribe_Collections.Look<Pawn>(ref this.tmpSavedPawns, "tmpSavedPawns", LookMode.Reference, new object[0]);
            }
            Scribe_Deep.Look<ThingOwner>(ref this.innerContainer, "innerContainer", new object[1]
            {
                this
            });
            //Scribe_Values.Look<int>(ref this.openDelay, "openDelay", 110, false);
            //Scribe_Values.Look<bool>(ref this.leaveSlag, "leaveSlag", false, false);
            if (this.savePawnsWithReferenceMode)
            {
                if (Scribe.mode != LoadSaveMode.PostLoadInit && Scribe.mode != LoadSaveMode.Saving)
                {
                    return;
                }
                for (int j = 0; j < this.tmpSavedPawns.Count; j++)
                {
                    this.innerContainer.TryAdd(this.tmpSavedPawns[j], true);
                }
                this.tmpSavedPawns.Clear();
            }


        }

        public override int GetTickInterval()
        {
            return int.MaxValue;
            //throw new NotImplementedException();
        }

        public override void TickOnInterval()
        {
            //throw new NotImplementedException();
        }




        void IThingHolder.GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        ThingOwner IThingHolder.GetDirectlyHeldThings()
        {
            return this.innerContainer;
        }


        public IThingHolder ParentHolder
        {
            get
            {
                return null;
            }
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return this.innerContainer;
        }
    }
}

