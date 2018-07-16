using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Excalibur.Quest
{
    abstract class ShipSystem
    {
        public abstract String Name();

        public abstract int PowerForRepair();
        public abstract int ResourceUnitsForRepair();
        //public abstract int TimeForRepair();


        public int m_MaxSystemStatus = 100;

        //Persisted
        public int m_SystemStatus = 0;
        public bool m_PriorityRepair = false;

        public void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.m_SystemStatus, "ShipSystem_" + this.Name() + "_SystemStatus");
            Scribe_Values.Look<bool>(ref this.m_PriorityRepair, "ShipSystem_" + this.Name() + "_PriorityRepair");
        }

        public Rect DoInterface(float x, float y, float width, int index)
        {
            //Log.Message("Interface");

            Rect _RectTotal = new Rect(x, y, width, 100f);

            Rect _RectTopHalf = _RectTotal.TopHalf();
            Rect _RectBottomHalf = _RectTotal.BottomHalf();

            Rect _RectQuarter1 = _RectTopHalf.TopHalf();
            Widgets.TextArea(_RectQuarter1, this.Name() + " Status", true);

            Rect _RectQuarter2 = _RectTopHalf.BottomHalf();
            Widgets.TextArea(_RectQuarter2, "System Status: " + this.m_SystemStatus.ToString() + " / " + this.m_MaxSystemStatus.ToString(), true);

            Rect _RectQuarter3 = _RectBottomHalf.TopHalf();
            Widgets.TextArea(_RectQuarter3, "RU:" + this.ResourceUnitsForRepair() + " Power: " + this.PowerForRepair(), true);

            Rect _RectQuarter4 = _RectBottomHalf.BottomHalf();
            //Widgets.TextArea(_RectQuarter4.LeftHalf(), ":" + "TEST", true);

            if (this.m_PriorityRepair)
            {

                if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "Priority Repair: ENABLED"))
                {
                    this.m_PriorityRepair = false;
                };
            }
            else
            {

                if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "Priority Repair: DISABLED"))
                {
                    this.m_PriorityRepair = true;
                };
            }

            //if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "-",true,false,true))
            //{
            //    Log.Message("-");
            //    this.m_SystemStatus -= 1;
            //};

            //if (Widgets.ButtonText(_RectQuarter4.RightHalf().RightHalf(), "+", true, false, true))
            //{
            //    Log.Message("-");
            //    this.m_SystemStatus += 1;
            //};

            return _RectTotal;

        }

        public bool CanPriorityRepair()
        {
            if (this.m_SystemStatus >= this.m_MaxSystemStatus) { return false; }
            if (!this.m_PriorityRepair) { return false; }
            if (this.PowerForRepair() > Core.GameComponent_Excalibur.Instance.Comp_Quest.GetReservePowerAsInt()) { return false; }
            if (this.ResourceUnitsForRepair() > Core.GameComponent_Excalibur.Instance.Comp_Quest.GetReserveMaterials()) { return false; }
            
            return true;
        }

        public void ProgressRepair()
        {
            Core.GameComponent_Excalibur.Instance.Comp_Quest.RequestReservePower(this.PowerForRepair());
            Core.GameComponent_Excalibur.Instance.Comp_Quest.RequestReserveMaterials(this.ResourceUnitsForRepair());

            this.m_SystemStatus += 1;
        }

    }
}
