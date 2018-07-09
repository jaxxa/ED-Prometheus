using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnhancedDevelopment.Excalibur.Core
{
    abstract class GameComponent_BaseClass
    {
        public void TickIfRequired(int currentTick)
        {
            
            if (currentTick % this.GetTickInterval() == 0)
            {
                this.TickOnInterval();
            }
        }

        public virtual void FinalizeInit()
        {

        }

        public abstract int GetTickInterval();

        public abstract void TickOnInterval();

        public abstract void ExposeData();
    }
}
