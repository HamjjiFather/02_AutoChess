using System;
using System.Linq;
using KKSFramework;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using UniRx;
using Zenject;

namespace AutoChess
{
    public class AdventurePage : PageViewBase
    {
        #region Fields & Property

        public ViewLayoutLoaderBase viewLayoutLoader;

        public BattleCharacterListArea battleCharacterListArea;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private CharacterManager _characterViewmodel;

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

        protected void Awake ()
        {
            ProjectContext.Instance.Container.BindInstance (battleCharacterListArea);
            battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            viewLayoutLoader.Initialize ();
        }

        #endregion


        #region Methods

        protected override async UniTask OnPush (object pushValue = null)
        {
            var fieldViewLayout = viewLayoutLoader.ViewLayoutBases[0] as FieldViewLayout;
            var battleViewLayout = viewLayoutLoader.ViewLayoutBases[1] as BattleViewLayout;

            if (fieldViewLayout == null || battleViewLayout == null)
                return;

            viewLayoutLoader.SetSubView (0);
            await fieldViewLayout.StartAdventure ();
            SubscribeBattleCommand ();
            await base.OnPush (pushValue);

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

                    // 모든 유닛 사망 여부 체크.
                    if (_characterViewmodel.BattleCharacterModels.All (x => x.CharacterDeathInfo.Death))
                    {
                        
                    }
                });
            }
            
        }


        protected override UniTask OnHide ()
        {
            _startBattleDisposable.DisposeSafe ();
            _endBattleDisposable.DisposeSafe ();
            return base.OnHide ();
        }


        protected override UniTask Popped ()
        {
            ((FieldViewLayout) viewLayoutLoader.ViewLayoutBases[0]).DisposeViewLayout ().Forget();
            return base.Popped ();
        }


        #endregion


        #region EventMethods

        #endregion
    }
}