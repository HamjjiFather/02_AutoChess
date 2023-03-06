using KKSFramework;
using UnityEngine.Serialization;
using Zenject;

namespace AutoChess.Presenter
{
    public class AdventureEnvironmentController : GameEnvironmentBase
    {
        #region Fields & Property
        
        [Inject]
        private BuildingManager _buildingManager;

        /// <summary>
        /// 탐험 월드.
        /// </summary>
        public AdventureField field;

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        public PlayableCharacterAgentController player;

        /// <summary>
        /// 플레이어 캐릭터에 포함된 감시 컴포넌트.
        /// </summary>
        public AdventureDetectableObjectDetector detector;

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

        public override void OnEnvironmentEnabled(EnvironmentParameterBase environmentParameter)
        {
            var outpostMap = _buildingManager.OutpostBuildingEntities;
            player.transform.position = field.baseBuildingTile.spawnPoint.position;
            
            field.outpostBuildingTiles.Foreach(obt =>
            {
                var entity = outpostMap[obt.outpostIndex];
                obt.Initialize(entity);
            });
            
            detector.Activate();
        }

        #endregion

        #endregion
    }
}