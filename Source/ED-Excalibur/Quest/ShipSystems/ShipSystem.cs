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

        public static float m_Height = 100f;

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
            if (this.IsAtMaxLevel())
            {
                return false;
            }

            if (!this.IsEnoughNanoMaterialsToUpgrade())
            {
                return false;
            }

            return true;
        }

        public bool IsAtMaxLevel()
        {
            return this.CurrentLevel >= this.GetMaxLevel();
        }

        private bool IsEnoughNanoMaterialsToUpgrade()
        {
            return Core.GameComponent_Excalibur.Instance.Comp_Quest.ResourceGetReserveStatus(Core.GameComponent_Excalibur_Quest.EnumResourceType.NanoMaterials) >= this.NanoMaterialNeededForUpgrade();
        }

        public string DescriptionText = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";

        //Persisted
        public int CurrentLevel = 0;

        public void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.CurrentLevel, "ShipSystem_" + this.Name() + "_CurrentLevel");
        }


        public Rect DoInterface(float x, float y, float width, int index)
        {
            //Log.Message("Interface");

            Rect _RectTotal = new Rect(x, y, width, ShipSystem.m_Height);
            Rect _Total_LeftHalf_Control = _RectTotal.LeftHalf();

            Rect _left_Row1 = _Total_LeftHalf_Control.TopHalf().TopHalf();
            Widgets.TextArea(_left_Row1, this.Name() + " Status", true);
                       
            Rect _left_Row2 = _Total_LeftHalf_Control.TopHalf().BottomHalf();
            Widgets.TextArea(_left_Row2, "System Status Level: " + this.CurrentLevel.ToString() + " / " + this.GetMaxLevel().ToString(), true);

            Rect _left_Row3 = _Total_LeftHalf_Control.BottomHalf().TopHalf();
            Widgets.TextArea(_left_Row3, "Nano Materials Needed for Next Level:" + this.NanoMaterialNeededForUpgrade().ToString(), true);

            Rect _left_Row4 = _Total_LeftHalf_Control.BottomHalf().BottomHalf();
            //Widgets.TextArea(_RectQuarter4.LeftHalf(), ":" + "TEST", true);

            Rect _left_Row4_left = _left_Row4.LeftHalf();
            if (this.CanUpgradeLevel())
            {

                if (Widgets.ButtonText(_left_Row4_left, "Upgrade Level"))
                {
                    this.TryUpgradeLevel();
                };
            }
            else
            {
                if (this.IsAtMaxLevel())
                {
                    Widgets.ButtonText(_left_Row4_left, "MAX Level");
                }
                else if(!this.IsEnoughNanoMaterialsToUpgrade())
                {
                    Widgets.ButtonText(_left_Row4_left, "Low Nano Materials");
                }
                else if (Widgets.ButtonText(_left_Row4_left, "Upgrade DISABLED"))
                {
                };
            }
            
            Rect _Total_RightHalf_Description = _RectTotal.RightHalf();



            //Text.Font = GameFont.Small;

           // Rect _TextArea = canvas.TopPartPixels(canvas.height - this.CloseButSize.y);

            Widgets.TextAreaScrollable(_Total_RightHalf_Description, this.DescriptionText, ref this.m_Position, true);


            //Text.Font = GameFont.Medium;


            return _RectTotal;

        }

        Vector2 m_Position = Vector2.zero;

        public void TryUpgradeLevel()
        {
            Core.GameComponent_Excalibur.Instance.Comp_Quest.ResourceRequestReserve(Core.GameComponent_Excalibur_Quest.EnumResourceType.NanoMaterials,  this.NanoMaterialNeededForUpgrade());
            this.CurrentLevel += 1;

            Core.GameComponent_Excalibur.Instance.Comp_Quest.UpdateAllResearch();

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
