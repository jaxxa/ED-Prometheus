using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur_Transporter : GameComponent_BaseClass, IThingHolder
    {
        //protected
        public ThingOwner innerContainer = null;

        public void GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return this.innerContainer;
        }

        public GameComponent_Excalibur_Transporter()
        {
            Log.Message("Init Transporter");
            this.innerContainer = new ThingOwner<Thing>(this, false, LookMode.Deep);
        }

        public IThingHolder ParentHolder
        {
            get
            {
                return Find.World;
                // return (this.holdingOwner == null) ? null : this.holdingOwner.Owner;
            }
        }


        public virtual bool Accepts(Thing thing)
        {
            return this.innerContainer.CanAcceptAnyOf(thing, true);
        }

        public virtual bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            if (!this.Accepts(thing))
            {
                return false;
            }
            bool flag = false;
            if (thing.holdingOwner != null)
            {
                thing.holdingOwner.TryTransferToContainer(thing, this.innerContainer, thing.stackCount, true);
                flag = true;
            }
            else
            {
                flag = this.innerContainer.TryAdd(thing, true);
            }
            if (flag)
            {
                if (thing.Faction != null && thing.Faction.IsPlayer)
                {
                    // this.contentsKnown = true;
                }
                return true;
            }
            return false;
        }


        public virtual void EjectContents(IntVec3 dropLocation, Map map)
        {
            this.innerContainer.TryDropAll(dropLocation, map, ThingPlaceMode.Near, null, null);
        }



        //IThingHolder IThingHolder.ParentHolder => throw new NotImplementedException();


        public override void ExposeData()
        {
           // throw new NotImplementedException();
        }



        public void ExposeData2()
        {
            Log.Message("Expose2");
            Scribe_Deep.Look<ThingOwner>(ref this.innerContainer, "innerContainer", new object[1]
            {
                            this
            });

            //Scribe_Deep.Look<List<Thing>>(ref this.TransportBuffer, "TransportBuffer");
            //   Scribe_Collections.Look<Thing>(ref this.TransportBuffer, "TransportBuffer", LookMode.Deep);


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


        //  public List<Thing> TransportBuffer = new List<Thing>();

    }
}
