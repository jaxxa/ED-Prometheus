using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection;

namespace EnhancedDevelopment.Excalibur.Shields
{

    [StaticConstructorOnStartup]
    public class Comp_ShieldGenerator : ThingComp
    {

        Material currentMatrialColour;

        public CompProperties_ShieldGenerator Properties;
        
        #region Variables

        //UI elements - Unsaved
        private static Texture2D UI_DIRECT_ON;
        private static Texture2D UI_DIRECT_OFF;

        private static Texture2D UI_INDIRECT_ON;
        private static Texture2D UI_INDIRECT_OFF;

        private static Texture2D UI_INTERCEPT_DROPPOD_ON;
        private static Texture2D UI_INTERCEPT_DROPPOD_OFF;

        private static Texture2D UI_SHOW_ON;
        private static Texture2D UI_SHOW_OFF;

        //Visual Settings
        private bool m_ShowVisually_Active = true;
        private float m_ColourRed;
        private float m_ColourGreen;
        private float m_ColourBlue;

        //Mode Settings - Active
        private bool m_BlockIndirect_Active;
        private bool m_BlockDirect_Active;
        public bool m_InterceptDropPod_Active;
        public bool m_IdentifyFriendFoe_Active;

        //Mode Settings - Avalable
        private bool m_BlockIndirect_Avalable;
        private bool m_BlockDirect_Avalable;
        private bool m_InterceptDropPod_Avalable;
        private bool m_IdentifyFriendFoe_Avalable;

        //Field Settings
        public int m_FieldIntegrity_Max;
        private int m_FieldIntegrity_Initial;
        public int m_Field_Radius_Max;
        public int m_Field_Radius_Selected = 999;

        //Power Settings
        private int m_PowerRequired_Charging;
        private int m_PowerRequired_Standby;

        //Recovery Settings
        private int m_RechargeTickDelayInterval;
        private int m_RecoverWarmupDelayTicks;
        private int m_WarmupTicksRemaining;
        

        //private long m_TickCount = 0;

        //Comp, found each time.
        CompPowerTrader m_Power;

        #endregion Variables

        #region Initilisation

        //Static Construtor
        static Comp_ShieldGenerator()
        {
            //Setup UI
            UI_DIRECT_OFF = ContentFinder<Texture2D>.Get("UI/DirectOff", true);
            UI_DIRECT_ON = ContentFinder<Texture2D>.Get("UI/DirectOn", true);
            UI_INDIRECT_OFF = ContentFinder<Texture2D>.Get("UI/IndirectOff", true);
            UI_INDIRECT_ON = ContentFinder<Texture2D>.Get("UI/IndirectOn", true);
            UI_INTERCEPT_DROPPOD_OFF = ContentFinder<Texture2D>.Get("UI/FireOff", true);
            UI_INTERCEPT_DROPPOD_ON = ContentFinder<Texture2D>.Get("UI/FireOn", true);

            UI_SHOW_ON = ContentFinder<Texture2D>.Get("UI/ShieldShowOn", true);
            UI_SHOW_OFF = ContentFinder<Texture2D>.Get("UI/ShieldShowOff", true);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            this.Properties = ((CompProperties_ShieldGenerator)this.props);
            this.m_Power = this.parent.GetComp<CompPowerTrader>();
            
            this.RecalculateStatistics();
        }

        public void RecalculateStatistics()
        {

            //Visual Settings
            this.m_ShowVisually_Active = true;
            this.m_ColourRed = 0.5f;
            this.m_ColourGreen = 0.0f;
            this.m_ColourBlue = 0.5f;

            //Mode Settings - Active
            this.m_BlockIndirect_Active = this.Properties.m_BlockIndirect_Avalable;
            this.m_BlockDirect_Active = this.Properties.m_BlockIndirect_Avalable;
            this.m_InterceptDropPod_Active = this.Properties.m_InterceptDropPod_Avalable;

            //Mode Settings - Avalable
            this.m_BlockIndirect_Avalable = this.Properties.m_BlockIndirect_Avalable;
            this.m_BlockDirect_Avalable = this.Properties.m_BlockDirect_Avalable;
            this.m_InterceptDropPod_Avalable = this.Properties.m_InterceptDropPod_Avalable;

            //Field Settings
            this.m_FieldIntegrity_Max = this.Properties.m_FieldIntegrity_Max_Base;
            this.m_FieldIntegrity_Initial = this.Properties.m_FieldIntegrity_Initial;
            this.m_Field_Radius_Max = this.Properties.m_Field_Radius_Base;

            //Power Settings
            this.m_PowerRequired_Charging = this.Properties.m_PowerRequired_Charging;
            this.m_PowerRequired_Standby = this.Properties.m_PowerRequired_Standby;

            //Recovery Settings
            this.m_RechargeTickDelayInterval = this.Properties.m_RechargeTickDelayInterval_Base;
            this.m_RecoverWarmupDelayTicks = this.Properties.m_RecoverWarmupDelayTicks_Base;
            //this.m_WarmupTicksRemaining = this.Properties.m_RecoverWarmupDelayTicks_Base; // Dont do this???


            //Store the List of Building in initilisation????

            CompFacility _Facility = this.parent.GetComp<CompFacility>();
            Patch.Patcher.LogNULL(_Facility, "_Facility");

            FieldInfo _LinkedBuildingsInfo = typeof(CompFacility).GetField("linkedBuildings", BindingFlags.NonPublic | BindingFlags.Instance);
            Patch.Patcher.LogNULL(_LinkedBuildingsInfo, "_LinkedBuildingsInfo");

            List<Thing> _LinkedBuildings = _LinkedBuildingsInfo.GetValue(_Facility) as List<Thing>;
            Patch.Patcher.LogNULL(_LinkedBuildings, "_LinkedBuildings");

            //Log.Message(_LinkedBuildings.Count.ToString());
            

            _LinkedBuildings.ForEach(b =>
            {
                Building _Building = b as Building;
                Comp_ShieldUpgrade _Comp = _Building.GetComp<Comp_ShieldUpgrade>();

                Patch.Patcher.LogNULL(_Comp, "_Comp");
                Patch.Patcher.LogNULL(_Comp.Properties, "_Comp.Properties");

                this.AddStatsFromUpgrade(_Comp);

                //Patch.Patcher.LogNULL(_Comp, "_Comp");
                //Log.Message(_Comp.SecretTestoValue);

            });

            if (this.m_Field_Radius_Selected > this.m_Field_Radius_Max)
            {
                this.m_Field_Radius_Selected = this.m_Field_Radius_Max;
            }

            //Update power Usage
            if (this.m_CurrentStatus == EnumShieldStatus.ActiveSustaining)
            {
                this.m_Power.powerOutputInt = -this.m_PowerRequired_Standby;
            }
            else
            {
                this.m_Power.powerOutputInt = -this.m_PowerRequired_Charging;
            }

        }

        private void AddStatsFromUpgrade(Comp_ShieldUpgrade comp)
        {

            this.m_FieldIntegrity_Max += comp.Properties.FieldIntegrity_Increase;
            this.m_Field_Radius_Max += comp.Properties.Range_Increase;
            
            //Power
            this.m_PowerRequired_Charging += comp.Properties.PowerUsage_Increase;
            this.m_PowerRequired_Standby += comp.Properties.PowerUsage_Increase;


            if (comp.Properties.DropPodIntercept)
            {
                this.m_InterceptDropPod_Avalable = true;
                this.m_InterceptDropPod_Active = true;
            }

            if (comp.Properties.IdentifyFriendFoe)
            {
                //Log.Message("Setting IFF");
                this.m_IdentifyFriendFoe_Avalable = true;
                this.m_IdentifyFriendFoe_Active = true;
            }

            if (comp.Properties.SIFMode)
            {
                //this.sif = true;
            }

            if (comp.Properties.SlowDischarge)
            {
                //this.m_InterceptDropPod_Avalable = true;
            }

        }

        #endregion Initilisation

        #region Methods

        public override void CompTick()
        {
            base.CompTick();

            this.UpdateShieldStatus();

            this.TickRecharge();

            this.RecalculateStatistics();
        }

        public void UpdateShieldStatus()
        {
            Boolean _PowerAvalable = this.CheckPowerOn();

            switch (this.CurrentStatus)
            {

                case (EnumShieldStatus.Offline):

                    //If it is offline bit has Power that initialising
                    if (_PowerAvalable)
                    {
                        this.CurrentStatus = EnumShieldStatus.Initilising;
                        //this.m_warmupTicksCurrent = GenDate.SecondsToTicks(this.m_shieldRecoverWarmup);
                        this.m_WarmupTicksRemaining = this.m_RecoverWarmupDelayTicks;
                        //this.CellsToProtect = null;
                    }
                    break;


                case (EnumShieldStatus.Initilising):
                    if (_PowerAvalable)
                    {
                        if (this.m_WarmupTicksRemaining > 0)
                        {
                            this.m_WarmupTicksRemaining--;
                            //  Log.Message(this.m_warmupTicksCurrent.ToString());
                        }
                        else
                        {
                            this.CurrentStatus = EnumShieldStatus.ActiveCharging;
                            this.FieldIntegrity_Current = this.m_FieldIntegrity_Initial;
                        }
                    }
                    else
                    {
                        this.CurrentStatus = EnumShieldStatus.Offline;
                    }
                    break;


                case (EnumShieldStatus.ActiveDischarging):
                    if (_PowerAvalable)
                    {
                        this.CurrentStatus = EnumShieldStatus.ActiveCharging;
                    }
                    else if (this.FieldIntegrity_Current <= 0)
                    {
                        this.CurrentStatus = EnumShieldStatus.Offline;
                    }
                    break;
                case (EnumShieldStatus.ActiveCharging):
                    if (this.FieldIntegrity_Current < 0)
                    {
                        this.CurrentStatus = EnumShieldStatus.Offline;
                    }
                    else
                    {
                        if (!_PowerAvalable)
                        {
                            this.CurrentStatus = EnumShieldStatus.ActiveDischarging;
                        }
                        else if (this.FieldIntegrity_Current >= this.m_FieldIntegrity_Max)
                        {
                            this.CurrentStatus = EnumShieldStatus.ActiveSustaining;
                        }
                    }
                    break;

                case (EnumShieldStatus.ActiveSustaining):
                    if (!_PowerAvalable)
                    {
                        this.CurrentStatus = EnumShieldStatus.ActiveDischarging;
                    }
                    else
                    {
                        if (this.FieldIntegrity_Current < this.m_FieldIntegrity_Max)
                        {
                            this.CurrentStatus = EnumShieldStatus.ActiveCharging;
                        }
                    }
                    break;
            }
        }

        public bool IsActive()
        {
            //return true;
            return (this.CurrentStatus == EnumShieldStatus.ActiveCharging ||
                 this.CurrentStatus == EnumShieldStatus.ActiveDischarging ||
                 this.CurrentStatus == EnumShieldStatus.ActiveSustaining);
        }

        public bool CheckPowerOn()
        {
            if (this.m_Power != null)
            {
                if (this.m_Power.PowerOn)
                {
                    return true;
                }
            }
            return false;
        }

        public void TickRecharge()
        {
            if (Find.TickManager.TicksGame % this.m_RechargeTickDelayInterval == 0)
            {
                if (this.CurrentStatus == EnumShieldStatus.ActiveCharging)
                {
                    this.FieldIntegrity_Current++;
                }
                else if (this.CurrentStatus == EnumShieldStatus.ActiveDischarging)
                {
                    this.FieldIntegrity_Current--;
                }
            }
        }

        #endregion Methods

        #region Properties

        public EnumShieldStatus CurrentStatus
        {
            get
            {
                return this.m_CurrentStatus;
            }
            set
            {
                this.m_CurrentStatus = value;

                if (this.m_CurrentStatus == EnumShieldStatus.ActiveSustaining)
                {
                    this.m_Power.powerOutputInt = -this.m_PowerRequired_Standby;
                }
                else
                {
                    this.m_Power.powerOutputInt = -this.m_PowerRequired_Charging;
                }
            }
        }
        private EnumShieldStatus m_CurrentStatus = EnumShieldStatus.Offline;

        public int FieldIntegrity_Current
        {
            get
            {
                return this.m_FieldIntegrity_Current;
            }
            set
            {
                if (value < 0)
                {
                    this.CurrentStatus = EnumShieldStatus.Offline;
                    this.m_FieldIntegrity_Current = 0;
                }
                else if (value > this.m_FieldIntegrity_Max)
                {
                    this.m_FieldIntegrity_Current = this.m_FieldIntegrity_Max;
                }
                else
                {
                    this.m_FieldIntegrity_Current = value;
                }
            }
        }
        private int m_FieldIntegrity_Current;

        #endregion Properties

        #region Drawing


        public override void PostDraw()
        {
            //Log.Message("DrawComp");
            base.PostDraw();

            this.DrawShields();


        }

        /// <summary>
        /// Draw the shield Field
        /// </summary>
        public void DrawShields()
        {
            if (!this.IsActive() || !this.m_ShowVisually_Active)
            {
                return;
            }

            //Draw field
            this.DrawField(EnhancedDevelopment.Excalibur.Shields.Utilities.VectorsUtils.IntVecToVec(this.parent.Position));

        }

        //public override void DrawExtraSelectionOverlays()
        //{
        //    //    GenDraw.DrawRadiusRing(base.Position, shieldField.shieldShieldRadius);
        //}

        public void DrawSubField(IntVec3 center, float radius)
        {
            this.DrawSubField(EnhancedDevelopment.Excalibur.Shields.Utilities.VectorsUtils.IntVecToVec(center), radius);
        }

        //Draw the field on map
        public void DrawField(Vector3 center)
        {
            DrawSubField(center, this.m_Field_Radius_Selected);
        }

        public void DrawSubField(Vector3 position, float shieldShieldRadius)
        {
            position = position + (new Vector3(0.5f, 0f, 0.5f));

            Vector3 s = new Vector3(shieldShieldRadius, 1f, shieldShieldRadius);
            Matrix4x4 matrix = default(Matrix4x4);
            matrix.SetTRS(position, Quaternion.identity, s);

            if (currentMatrialColour == null)
            {
                //Log.Message("Creating currentMatrialColour");
                currentMatrialColour = SolidColorMaterials.NewSolidColorMaterial(new Color(m_ColourRed, m_ColourGreen, m_ColourBlue, 0.15f), ShaderDatabase.MetaOverlay);
                //currentMatrialColour = SolidColorMaterials.NewSolidColorMaterial(new Color(0.5f, 0.0f, 0.0f, 0.15f), ShaderDatabase.MetaOverlay);
            }

            UnityEngine.Graphics.DrawMesh(EnhancedDevelopment.Excalibur.Shields.Utilities.Graphics.CircleMesh, matrix, currentMatrialColour, 0);

        }

        #endregion Drawing

        #region UI

        public override string CompInspectStringExtra()
        {
            StringBuilder _StringBuilder = new StringBuilder();
            //return base.CompInspectStringExtra();
            _StringBuilder.Append(base.CompInspectStringExtra());

            if (this.IsActive())
            {
                _StringBuilder.AppendLine("Shield: " + this.FieldIntegrity_Current + "/" + this.m_FieldIntegrity_Max);
            }
            else if (this.CurrentStatus == EnumShieldStatus.Initilising)
            {
                //stringBuilder.AppendLine("Initiating shield: " + ((warmupTicks * 100) / recoverWarmup) + "%");
                _StringBuilder.AppendLine("Ready in " + Math.Round(GenTicks.TicksToSeconds(m_WarmupTicksRemaining)) + " seconds.");
                //stringBuilder.AppendLine("Ready in " + m_warmupTicksCurrent + " seconds.");
            }
            else
            {
                _StringBuilder.AppendLine("Shield disabled!");
            }

            if (m_Power != null)
            {
                string text = m_Power.CompInspectStringExtra();
                if (!text.NullOrEmpty())
                {
                    _StringBuilder.Append(text);
                }
                else
                {
                    _StringBuilder.Append("Error, No Power Comp Text.");
                }
            }
            else
            {
                _StringBuilder.Append("Error, No Power Comp.");
            }

            return _StringBuilder.ToString();

        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            //return base.CompGetGizmosExtra();

            //Add the stock Gizmoes
            foreach (var g in base.CompGetGizmosExtra())
            {
                yield return g;
            }

            if (m_BlockDirect_Avalable)
            {
                if (m_BlockIndirect_Active)
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchDirect();
                    act.icon = UI_DIRECT_ON;
                    act.defaultLabel = "Block Direct";
                    act.defaultDesc = "On";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
                else
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchDirect();
                    act.icon = UI_DIRECT_OFF;
                    act.defaultLabel = "Block Direct";
                    act.defaultDesc = "Off";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
            }

            if (m_BlockIndirect_Avalable)
            {
                if (m_BlockDirect_Active)
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchIndirect();
                    act.icon = UI_INDIRECT_ON;
                    act.defaultLabel = "Block Indirect";
                    act.defaultDesc = "On";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
                else
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchIndirect();
                    act.icon = UI_INDIRECT_OFF;
                    act.defaultLabel = "Block Indirect";
                    act.defaultDesc = "Off";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
            }

            if (m_InterceptDropPod_Avalable)
            {
                if (m_InterceptDropPod_Active)
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchInterceptDropPod();
                    act.icon = UI_INTERCEPT_DROPPOD_ON;
                    act.defaultLabel = "Intercept DropPod";
                    act.defaultDesc = "On";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
                else
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchInterceptDropPod();
                    act.icon = UI_INTERCEPT_DROPPOD_OFF;
                    act.defaultLabel = "Intercept DropPod";
                    act.defaultDesc = "Off";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
            }


            if (true)
            {
                if (m_ShowVisually_Active)
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchVisual();
                    act.icon = UI_SHOW_ON;
                    act.defaultLabel = "Show Visually";
                    act.defaultDesc = "Show";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
                else
                {

                    Command_Action act = new Command_Action();
                    //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                    act.action = () => this.SwitchVisual();
                    act.icon = UI_SHOW_OFF;
                    act.defaultLabel = "Show Visually";
                    act.defaultDesc = "Hide";
                    act.activateSound = SoundDef.Named("Click");
                    //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                    //act.groupKey = 689736;
                    yield return act;
                }
            }








        } //CompGetGizmosExtra()

        private void SwitchDirect()
        {
            m_BlockIndirect_Active = !m_BlockIndirect_Active;
        }

        private void SwitchIndirect()
        {
            m_BlockDirect_Active = !m_BlockDirect_Active;
        }

        private void SwitchInterceptDropPod()
        {
            m_InterceptDropPod_Active = !m_InterceptDropPod_Active;
        }

        private void SwitchVisual()
        {
            m_ShowVisually_Active = !m_ShowVisually_Active;
        }

        #endregion UI

        #region DataAcess

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref m_BlockIndirect_Active, "m_BlockIndirect_Active");
            Scribe_Values.Look(ref m_BlockDirect_Active, "m_BlockDirect_Active");
            Scribe_Values.Look(ref m_ShowVisually_Active, "m_ShowVisually_Active");
            Scribe_Values.Look(ref m_InterceptDropPod_Active, "m_InterceptDropPod_Active");

            Scribe_Values.Look(ref m_BlockIndirect_Avalable, "m_BlockIndirect_Avalable");
            Scribe_Values.Look(ref m_BlockDirect_Avalable, "m_BlockDirect_Avalable");
            Scribe_Values.Look(ref m_InterceptDropPod_Avalable, "m_InterceptDropPod_Avalable");

            Scribe_Values.Look(ref m_FieldIntegrity_Max, "m_FieldIntegrity_Max");
            Scribe_Values.Look(ref m_FieldIntegrity_Initial, "m_FieldIntegrity_Initial");
            Scribe_Values.Look(ref m_Field_Radius_Max, "m_Field_Radius");

            Scribe_Values.Look(ref m_PowerRequired_Charging, "m_PowerRequired_Charging");
            Scribe_Values.Look(ref m_PowerRequired_Standby, "m_PowerRequired_Standby");

            Scribe_Values.Look(ref m_RechargeTickDelayInterval, "m_shieldRechargeTickDelay");
            Scribe_Values.Look(ref m_RecoverWarmupDelayTicks, "m_shieldRecoverWarmup");

            Scribe_Values.Look(ref m_ColourRed, "m_colourRed");
            Scribe_Values.Look(ref m_ColourGreen, "m_colourGreen");
            Scribe_Values.Look(ref m_ColourBlue, "m_colourBlue");

            Scribe_Values.Look(ref m_WarmupTicksRemaining, "m_WarmupTicksRemaining");

            Scribe_Values.Look(ref m_CurrentStatus, "m_CurrentStatus");
            Scribe_Values.Look(ref m_FieldIntegrity_Current, "m_FieldIntegrity_Current");

        }

        #endregion DataAcess

    }
}
