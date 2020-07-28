using System;
using KKSFramework.Navigation;
using UniRx;
using UniRx.Async;
using Zenject;

namespace AutoChess
{
    public class AdventurePageView : PageViewBase
    {
        #region Fields & Property

        public ViewLayoutLoader viewLayoutLoader;

        public BattleCharacterListArea battleCharacterListArea;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;
        
        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 전투 시작 구독.
        /// </summary>
        private IDisposable _startBattleDisposable;
        
        /// <summary>
        /// 전투 종료 구독.
        /// </summary>
        private IDisposable _endBattleDisposable;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            ProjectContext.Instance.Container.BindInstance (battleCharacterListArea);
            battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            viewLayoutLoader.Initialize ();
        }

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            viewLayoutLoader.SetSubView (0);
            SubscribeBattleCommand ();
            return base.OnPush (pushValue);

            void SubscribeBattleCommand ()
            {
                var fieldViewLayout = viewLayoutLoader.GetViewLayout (0) as FieldViewLayout;
                var battleViewLayout = viewLayoutLoader.GetViewLayout (1) as BattleViewLayout;

                if (fieldViewLayout == null || battleViewLayout == null)
                    return;
                
                _startBattleDisposable = _battleViewmodel.StartBattleCommand.Subscribe (stageModel =>
                {
                    viewLayoutLoader.SetSubView (1);
                    battleViewLayout.StartBattle ().Forget ();
                });

                _endBattleDisposable = _battleViewmodel.EndBattleCommand.Subscribe (isWin =>
                {
                    viewLayoutLoader.SetSubView (0);
                    fieldViewLayout.EndBattle (isWin);
                });
            }
        }

        protected override void Hid ()
        {
            _startBattleDisposable.DisposeSafe ();
            _endBattleDisposable.DisposeSafe ();
            base.Hid ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}