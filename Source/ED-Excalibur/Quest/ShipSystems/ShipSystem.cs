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

        public virtual int NanoMaterialNeededForUpgrade()
        {
            return 100;
        }

        public abstract int GetMaxLevel();

        public virtual void ApplyRequiredResearchUnlocks()
        {

        }
                    
        public bool CanUpgradeLevel()
        {
            if (this.CurrentLevel >= this.GetMaxLevel())
            {
                return false;
            }

            if (Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterials < this.NanoMaterialNeededForUpgrade())
            {
                return false;
            }

            return true;
        }

        //Persisted
        public int CurrentLevel = 0;

        public void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.CurrentLevel, "ShipSystem_" + this.Name() + "_CurrentLevel");
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
            Widgets.TextArea(_RectQuarter2, "System Status Level: " + this.CurrentLevel.ToString() + " / " + this.GetMaxLevel().ToString(), true);

            Rect _RectQuarter3 = _RectBottomHalf.TopHalf();
            Widgets.TextArea(_RectQuarter3, "Nano Materials Needed for Next Level:" + this.NanoMaterialNeededForUpgrade().ToString(), true);

            Rect _RectQuarter4 = _RectBottomHalf.BottomHalf();
            //Widgets.TextArea(_RectQuarter4.LeftHalf(), ":" + "TEST", true);

            if (this.CanUpgradeLevel())
            {

                if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "Upgrade Level"))
                {
                    this.TryUpgradeLevel();
                };
            }
            else
            {

                if (Widgets.ButtonText(_RectQuarter4.RightHalf().LeftHalf(), "Upgrade DISABLED"))
                {
                    //this.m_PriorityRepair = true;
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

        public void TryUpgradeLevel()
        {
            Core.GameComponent_Excalibur.Instance.Comp_Quest.NanoMaterials -= this.NanoMaterialNeededForUpgrade();
            this.CurrentLevel += 1;

        }

        //public bool CanPriorityRepair()
        //{
        //    if (this.m_SystemStatus >= this.GetMaxSystemStatus()) { return false; }
        //    if (!this.m_PriorityRepair) { return false; }
        //    if (this.PowerForRepair() > Core.GameComponent_Excalibur.Instance.Comp_Quest.GetReservePowerAsInt()) { return false; }
        //    if (this.ResourceUnitsForRepair() > Core.GameComponent_Excalibur.Instance.Comp_Quest.GetReserveMaterials()) { return false; }

        //    return true;
        //}

        //public void ProgressRepair()
        //{
        //    Core.GameComponent_Excalibur.Instance.Comp_Quest.RequestReservePower(this.PowerForRepair());
        //    Core.GameComponent_Excalibur.Instance.Comp_Quest.RequestReserveMaterials(this.ResourceUnitsForRepair());
        //    Core.GameComponent_Excalibur.Instance.Comp_Quest.UpdateAllResearch();
        //    this.m_SystemStatus += 1;
        //}

    }
}
