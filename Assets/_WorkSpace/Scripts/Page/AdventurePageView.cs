using System;
using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using UniRx;
using Zenject;

namespace AutoChess
{
    public class AdventurePageView : PageController
    {
        #region Fields & Property

        public ViewLayoutLoaderBase viewLayoutLoader;

        public BattleCharacterListArea battleCharacterListArea;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

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

        protected override async UniTask OnPrepareAsync (Parameters parameters)
        {
            var fieldViewLayout = viewLayoutLoader.ViewLayoutBases[0] as FieldViewLayout;
            var battleViewLayout = viewLayoutLoader.ViewLayoutBases[1] as BattleViewLayout;

            if (fieldViewLayout == null || battleViewLayout == null)
                return;

            viewLayoutLoader.SetSubView (0);
            await fieldViewLayout.StartAdventure ();
            SubscribeBattleCommand ();
            await base.OnPrepareAsync (parameters);

            void SubscribeBattleCommand ()
            {
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

        protected override UniTask OnHideAsync ()
        {
            _startBattleDisposable.DisposeSafe ();
            _endBattleDisposable.DisposeSafe ();
            return base.OnHideAsync ();
        }


        protected override void OnPopComplete ()
        {
            ((FieldViewLayout) viewLayoutLoader.ViewLayoutBases[0]).DisposeViewLayout ().Forget();
            base.OnPopComplete ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}