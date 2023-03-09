using System.Collections.Generic;
using AutoChess.Domain;
using AutoChess.Presenter;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public enum GameSceneType
    {
        None,
        Base,
        Adventure,
        Battle
    }

    /// <summary>
    /// 게임 씬에서 큰 범주의 페이지 전환을 관리 한다.
    /// </summary>
    public class GameSceneManager : ManagerBase
    {
        public GameSceneManager(BasementManager basementManager, AdventureManager adventureManager,
            BattleManager battleManager)
        {
            SceneManagerMap.Add(GameSceneType.Base, basementManager);
            SceneManagerMap.Add(GameSceneType.Adventure, adventureManager);
            SceneManagerMap.Add(GameSceneType.Battle, battleManager);

            ViewMap.Add(GameSceneType.Base, NavigationViewType.BasePage);
            ViewMap.Add(GameSceneType.Adventure, NavigationViewType.AdventurePage);
            ViewMap.Add(GameSceneType.Battle, NavigationViewType.BattlePage);
        }

        #region Fields & Property

        [Inject]
        private LazyInject<EnvironmentConverter> _environmentConverter;

        private GameSceneType _currentGameScene;

        public readonly Dictionary<GameSceneType, IGameSceneManager<GameSceneParameterBase>> SceneManagerMap = new();

        public readonly Dictionary<GameSceneType, NavigationViewType> ViewMap = new();

        #endregion


        #region Methods

        #region Override

        public override void Initialize()
        {
        }

        #endregion


        #region This

        public void ChangeScene(GameSceneType gameSceneType, [CanBeNull] GameSceneParameterBase gameSceneParam = null,
            [CanBeNull] EnvironmentParameterBase environmentParam = null)
        {
            if (SceneManagerMap.ContainsKey(_currentGameScene))
            {
                SceneManagerMap[_currentGameScene].Dispose();
            }

            _currentGameScene = gameSceneType;

            if (SceneManagerMap.ContainsKey(_currentGameScene))
            {
                SceneManagerMap[_currentGameScene].OnStart(gameSceneParam);
            }

            // 네비게이션 변경.
            NavigationHelper.OpenPageAsync(ViewMap[_currentGameScene], NavigationTriggerState.First).Forget();

            // 환경 변경.
            _environmentConverter.Value.ChangeEnvironment(gameSceneType, environmentParam);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}