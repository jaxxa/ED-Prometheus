using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnhancedDevelopment.Excalibur.Core
{
    abstract class GameComponent_BaseClass
    {
        public abstract void Tick();

        public abstract void ExposeData();
    }
}
