namespace AutoChess.Presenter
{
    public class AdventureEnvironmentController : GameEnvironmentBase
    {
        #region Fields & Property

        /// <summary>
        /// 탐험 월드.
        /// </summary>
        public AdventureWorld world;

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        public PlayableCharacterAgentController player;

        #endregion


        #region Methods

        #region Override

        // public override void InstallBindings()
        // {
        //     Container.BindInstance(world).AsSingle();
        //     Container.BindInstance(player).AsSingle();
        //
        //     Container.Inject(player);
        // }

        #endregion


        #region This

        #endregion


        #region Event

        public override void OnEnvironmentEnabled(EnvironmentParameterBase environmentParameterBase)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion
    }
}