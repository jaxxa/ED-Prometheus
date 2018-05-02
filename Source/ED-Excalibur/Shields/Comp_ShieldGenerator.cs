using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace EnhancedDevelopment.Excalibur.Shields
{

    [StaticConstructorOnStartup]
    public class Comp_ShieldGenerator : ThingComp
    {
        
        Material currentMatrialColour;

        #region Variables

        //UI elements
        private static Texture2D UI_DIRECT_ON;
        private static Texture2D UI_DIRECT_OFF;

        private static Texture2D UI_INDIRECT_ON;
        private static Texture2D UI_INDIRECT_OFF;

        private static Texture2D UI_INTERCEPT_DROPPOD_ON;
        private static Texture2D UI_INTERCEPT_DROPPOD_OFF;

        private static Texture2D UI_SHOW_ON;
        private static Texture2D UI_SHOW_OFF;

        //Default all flasgs to be shown initially
        protected bool m_BlockIndirect_Active = true;
        protected bool m_BlockDirect_Active = true;
        protected bool m_ShowVisually_Active = true;
        protected bool m_InterceptDropPod_Active = true;

        //Variables to store what modes the shield have avalable.
        protected bool m_BlockIndirect_Avalable;
        protected bool m_BlockDirect_Avalable;
        protected bool m_InterceptDropPod_Avalable;

        //variables that are read in from XML
        private int m_FieldIntegrity_Max;
        private int m_FieldIntegrity_Initial;
        public int m_Field_Radius;

        private int m_PowerRequired_Charging;
        private int m_PowerRequired_Sustaining;

        private int m_RechargeTickDelayInterval;
        private int m_RecoverWarmupDelayTicks;

        private float m_ColourRed;
        private float m_ColourGreen;
        private float m_ColourBlue;

        private int m_WarmupTicksRemaining = 0;
        private long m_TickCount = 0;

        CompPowerTrader m_Power;

        #endregion
        
        #region Initilisation
        
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
            
            this.m_Power = this.parent.GetComp<CompPowerTrader>();
            
            //Read in variables from the custom MyThingDef
            m_FieldIntegrity_Max = ((CompProperties_ShieldGenerator)this.props).m_FieldIntegrity_Max;
            m_FieldIntegrity_Initial = ((CompProperties_ShieldGenerator)this.props).m_FieldIntegrity_Initial;
            m_Field_Radius = ((CompProperties_ShieldGenerator)this.props).m_Field_Radius;

            m_PowerRequired_Charging = ((CompProperties_ShieldGenerator)this.props).m_PowerRequiredCharging;
            m_PowerRequired_Sustaining = ((CompProperties_ShieldGenerator)this.props).m_PowerRequiredSustaining;

            m_RechargeTickDelayInterval = ((CompProperties_ShieldGenerator)this.props).m_RechargeTickDelayInterval;
            m_RecoverWarmupDelayTicks = ((CompProperties_ShieldGenerator)this.props).m_RecoverWarmupDelayTicks;

            m_BlockIndirect_Avalable = ((CompProperties_ShieldGenerator)this.props).m_BlockIndirect_Avalable;
            m_BlockDirect_Avalable = ((CompProperties_ShieldGenerator)this.props).m_BlockDirect_Avalable;
            m_InterceptDropPod_Avalable = ((CompProperties_ShieldGenerator)this.props).m_InterceptDropPod_Avalable;
            
            m_ColourRed = ((CompProperties_ShieldGenerator)this.props).m_ColourRed;
            m_ColourGreen = ((CompProperties_ShieldGenerator)this.props).m_ColourGreen;
            m_ColourBlue = ((CompProperties_ShieldGenerator)this.props).m_ColourBlue;
        }

        #endregion Initilisation

        #region Methods

        public override void CompTick()
        {
            base.CompTick();
            
            this.UpdateShieldStatus();

            this.TickRecharge();
            
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
                    this.m_Power.powerOutputInt = this.m_PowerRequired_Sustaining;
                }
                else
                {
                    this.m_Power.powerOutputInt = this.m_PowerRequired_Charging;
                }
            }
        }
        private EnumShieldStatus m_CurrentStatus = EnumShieldStatus.Offline;

        private int FieldIntegrity_Current
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
            //if (!this.IsActive() || !this.m_ShowVisually_Active)
            //{
            //    return;
            //}
            
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
            DrawSubField(center, 10);
            //DrawSubField(center, m_Field_Radius);
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
               // currentMatrialColour = SolidColorMaterials.NewSolidColorMaterial(new Color(m_ColourRed, m_ColourGreen, m_ColourBlue, 0.15f), ShaderDatabase.MetaOverlay);
                currentMatrialColour = SolidColorMaterials.NewSolidColorMaterial(new Color(0.5f, 0.0f, 0.0f, 0.15f), ShaderDatabase.MetaOverlay);
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
            
            //Find Upgrades
            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.FindUpgrades();
                //act.icon = UI_SHOW_ON;
                act.defaultLabel = "Find Upgrades";
                act.defaultDesc = "Find Upgrades";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
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

        private void FindUpgrades()
        {
            this.parent.Map.listerBuildings.allBuildingsColonist.Where(x => x.AllComps.Any(c => c as Comp_ShieldUpgrade != null)).ToList().ForEach(b =>
            {
                b.AllComps.ForEach(c => {
                    CompProperties_ShieldUpgrade _Props = c.props as CompProperties_ShieldUpgrade;
                    if (_Props != null)
                    {
                        Log.Message("Found Comp, mp= " + _Props.m_FieldIntegrity_Multiplier);

                    }

                });

            }
            );
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
            Scribe_Values.Look(ref m_Field_Radius, "m_Field_Radius");

            Scribe_Values.Look(ref m_PowerRequired_Charging, "m_PowerRequired_Charging");
            Scribe_Values.Look(ref m_PowerRequired_Sustaining, "m_PowerRequired_Sustaining");

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
