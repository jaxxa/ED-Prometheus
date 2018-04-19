using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Excalibur
{
    class Building_EDSNTransponder : Building
    {

        public override IEnumerable<Gizmo> GetGizmos()
        {
            //Add the stock Gizmoes
            foreach (var g in base.GetGizmos())
            {
                yield return g;
            }
            
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.ContactExcalibur();
                //act.icon = UI_DIRECT_ON;
                act.defaultLabel = "Contact Excalibur";
                act.defaultDesc = "Contact Excalibur";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
        }

        public void ContactExcalibur()
        {
            Log.Message("Contacting Excalibur");
        }

    }

}
