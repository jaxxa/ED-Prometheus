using EnhancedDevelopment.Excalibur.Quest.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Excalibur.Core
{
    class GameComponent_Excalibur : Verse.GameComponent
    {

        public static GameComponent_Excalibur Instance;

        public GameComponent_Excalibur_NanoShields Comp_Shields;
        public GameComponent_Excalibur_Quest Comp_Quest;
        public GameComponent_Excalibur_Fabrication Comp_Fabrication;
        public GameComponent_Excalibur_Transporter Comp_Transporter;

        private List<GameComponent_BaseClass> m_SubComponents = new List<GameComponent_BaseClass>();

        #region Variables


        #endregion

        public GameComponent_Excalibur(Game game)
        {
            //Record the instance for easy acess.
            GameComponent_Excalibur.Instance = this;

            this.Comp_Shields = new GameComponent_Excalibur_NanoShields();
            this.m_SubComponents.Add(this.Comp_Shields);

            this.Comp_Quest = new GameComponent_Excalibur_Quest();
            this.m_SubComponents.Add(this.Comp_Quest);

            this.Comp_Fabrication = new GameComponent_Excalibur_Fabrication();
            this.m_SubComponents.Add(this.Comp_Fabrication);

            this.Comp_Transporter = new GameComponent_Excalibur_Transporter();
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

    }
}

