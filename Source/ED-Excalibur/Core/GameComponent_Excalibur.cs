using EnhancedDevelopment.Prometheus.Quest.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Prometheus.Core
{
    class GameComponent_Prometheus : Verse.GameComponent
    {

        public static GameComponent_Prometheus Instance;

        public GameComponent_Prometheus_NanoShields Comp_Shields;
        public GameComponent_Prometheus_Quest Comp_Quest;
        public GameComponent_Prometheus_Fabrication Comp_Fabrication;
        public GameComponent_Prometheus_Transporter Comp_Transporter;

        private List<GameComponent_BaseClass> m_SubComponents = new List<GameComponent_BaseClass>();

        #region Variables


        #endregion

        public GameComponent_Prometheus(Game game)
        {
            //Record the instance for easy acess.
            GameComponent_Prometheus.Instance = this;

            this.Comp_Shields = new GameComponent_Prometheus_NanoShields();
            this.m_SubComponents.Add(this.Comp_Shields);

            this.Comp_Quest = new GameComponent_Prometheus_Quest();
            this.m_SubComponents.Add(this.Comp_Quest);

            this.Comp_Fabrication = new GameComponent_Prometheus_Fabrication();
            this.m_SubComponents.Add(this.Comp_Fabrication);

            this.Comp_Transporter = new GameComponent_Prometheus_Transporter();
            this.m_SubComponents.Add(Comp_Transporter);

        }

        //public override void GameComponentUpdate()
        //{
        //    Log.Message("GC.Update");
        //    base.GameComponentUpdate();
        //}

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            int _CurrentTick = Find.TickManager.TicksGame;

            this.m_SubComponents.ForEach(x => x.TickIfRequired(_CurrentTick));

            //this.Shields.Tick();
            //this.Quest.Tick();
            
        }

        public override void ExposeData()
        {
            base.ExposeData();
            this.m_SubComponents.ForEach(x => x.ExposeData());

            //this.Quest.ExposeData();
            //this.Shields.ExposeData();
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            this.m_SubComponents.ForEach(x => x.FinalizeInit());

        }

    }
}

