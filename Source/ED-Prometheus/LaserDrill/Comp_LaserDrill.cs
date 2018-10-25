using EnhancedDevelopment.Prometheus.Settings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.LaserDrill
{
    [StaticConstructorOnStartup]
    class Comp_LaserDrill : ThingComp
    {


        public enum EnumLaserDrillState
        {
            Scanning,
            LowPower,
            ReadyToActivate,
            NoGuyser
        }


        #region Variables

        //Saved
        private int DrillWork;

        //Unsaved
        //private CompPowerTrader _PowerComp;
        //private CompFlickable _FlickComp;
        private CompProperties_LaserDrill Properties;

        private static Texture2D UI_LASER_ACTIVATE;

        private EnumLaserDrillState m_CurrentStaus = EnumLaserDrillState.LowPower;

        #endregion Variables

        #region Initilisation

        static Comp_LaserDrill()
        {

            UI_LASER_ACTIVATE = ContentFinder<Texture2D>.Get("UI/DirectOn", true);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            //this._PowerComp = this.parent.GetComp<CompPowerTrader>();
            //this._FlickComp = this.parent.GetComp<CompFlickable>();
            this.Properties = this.props as CompProperties_LaserDrill;

            if (!respawningAfterLoad)
            {
                this.CalculateWorkStart();
            }

        }

        #endregion Initilisation

        #region Overrides
        
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.DrillWork, "DrillWork", 0);
        } //PostExposeData()

        public override void CompTickRare()
        {
        
            if (this.Properties.FillMode && this.FindClosestGuyser() == null)
            {
                this.m_CurrentStaus = EnumLaserDrillState.NoGuyser;
            }
            else if (this.DrillWork <= 0)
            {
                if (Core.GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(Core.GameComponent_Prometheus_Quest.EnumResourceType.Power) > 1000)
                {
                    this.m_CurrentStaus = EnumLaserDrillState.LowPower;
                }
                else
                {
                    this.m_CurrentStaus = EnumLaserDrillState.ReadyToActivate;
                }
                
            }
            else
            {
                this.m_CurrentStaus = EnumLaserDrillState.Scanning;

                this.DrillWork = this.DrillWork - 1;
            }
            
            
            base.CompTickRare();

        } //CompTickRare()

        public override string CompInspectStringExtra()
        {
            // return base.CompInspectStringExtra();

            StringBuilder _StringBuilder = new StringBuilder();

            //if (this.parent.Map != null && this.parent.Map.GetComponent<MapComp_LaserDrill>() != null)

            {
                //if (!this.parent.Map.GetComponent<MapComp_LaserDrill>().IsActive(this.parent))
                //{
                //    _StringBuilder.Append("Drill Status: Offline, Waiting for another drill to finish.");
                //}
                //else

                if (this.m_CurrentStaus == EnumLaserDrillState.LowPower)
                {
                    _StringBuilder.AppendLine("Scan complete");
                    _StringBuilder.Append("Low Power on Ship");
                }
                else if(this.m_CurrentStaus == EnumLaserDrillState.NoGuyser)
                {
                    _StringBuilder.Append("Error: No Guyser found");
                }
                else if (this.m_CurrentStaus == EnumLaserDrillState.ReadyToActivate)
                {
                    _StringBuilder.AppendLine("Scan complete");
                    _StringBuilder.Append("Ready for Laser Activation");
                }
                else if(this.m_CurrentStaus == EnumLaserDrillState.Scanning)
                {
                    _StringBuilder.AppendLine("Scanning in Progress");
                    _StringBuilder.Append("Work Remaining: " + this.DrillWork);
                }
            }

            return _StringBuilder.ToString();
        } //CompInspectStringExtra()

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            //return base.CompGetGizmosExtra();

            //Add the stock Gizmoes
            foreach (var g in base.CompGetGizmosExtra())
            {
                yield return g;
            }

            if (this.m_CurrentStaus == EnumLaserDrillState.ReadyToActivate)
            //if (true)
            {
                Command_Action act = new Command_Action();
                act.action = () => this.TriggerLaser();
                act.icon = UI_LASER_ACTIVATE;
                act.defaultLabel = "Activate Laser";
                act.defaultDesc = "Activate Laser";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }

        } //CompGetGizmosExtra()
        
        public override void PostDeSpawn(Map map)
        {
            this.CalculateWorkStart();
            base.PostDeSpawn(map);
        }

        #endregion Overrides

        #region Methods

        private void CalculateWorkStart()
        {
            if (this.Properties.FillMode)
            {
                this.DrillWork = Mod_EDPrometheus.Settings.LaserDrill.RequiredFillWork;
            }
            else
            {
                this.DrillWork = Mod_EDPrometheus.Settings.LaserDrill.RequiredDrillWork;
            }
        }

        public Thing FindClosestGuyser()
        {
            List<Thing> steamGeysers = this.parent.Map.listerThings.ThingsOfDef(ThingDefOf.SteamGeyser);
            Thing currentLowestGuyser = null;

            double lowestDistance = double.MaxValue;

            foreach (Thing currentGuyser in steamGeysers)
            {
                //if (currentGuyser.SpawnedInWorld)
                if (currentGuyser.Spawned)
                {
                    if (this.parent.Position.InHorDistOf(currentGuyser.Position, 5))
                    {
                        double distance = Math.Sqrt(Math.Pow((this.parent.Position.x - currentGuyser.Position.x), 2) + Math.Pow((this.parent.Position.y - currentGuyser.Position.y), 2));

                        if (distance < lowestDistance)
                        {

                            lowestDistance = distance;
                            currentLowestGuyser = currentGuyser;
                        }
                    }
                }
            }
            return currentLowestGuyser;
        }

        public void TriggerLaser()
        {


            if (this.Properties.FillMode)
            {
                if (this.FindClosestGuyser() != null)
                {
                    Messages.Message("SteamGeyser Removed.", MessageTypeDefOf.TaskCompletion);
                    this.FindClosestGuyser().DeSpawn();
                    this.ShowLaserVisually();
                    this.parent.Destroy(DestroyMode.Vanish);
                }
                else
                {
                    Messages.Message("SteamGeyser not found to Remove.", MessageTypeDefOf.TaskCompletion);
                }
            }
            else
            {
                Messages.Message("SteamGeyser Created.", MessageTypeDefOf.TaskCompletion);
                this.ShowLaserVisually();
                GenSpawn.Spawn(ThingDef.Named("SteamGeyser"), this.parent.Position, this.parent.Map);

                //Destroy

                this.parent.Destroy(DestroyMode.Vanish);

            }


        }

        private void ShowLaserVisually()
        {
            IntVec3 _Position = IntVec3.FromVector3(new UnityEngine.Vector3(parent.Position.x, parent.Position.y, parent.Position.z - 2));
            LaserDrillVisual _LaserDrillVisual = (LaserDrillVisual)GenSpawn.Spawn(ThingDef.Named("LaserDrillVisual"), _Position, parent.Map, WipeMode.Vanish);
        }

        #endregion


    } //Comp_LaserDrill

}

