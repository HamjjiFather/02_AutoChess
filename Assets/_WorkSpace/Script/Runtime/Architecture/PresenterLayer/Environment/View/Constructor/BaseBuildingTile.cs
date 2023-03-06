using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChess.Presenter
{
    public class BaseBuildingTile : BuildingTileBase, ISpawnPoint
    {
        #region Fields & Property
        
        public override BuildingEntityBase BuildingEntity { get; set; }

        public Transform spawnPoint;
        
        public Transform SpawnPoint
        {
            get => spawnPoint;
            set => spawnPoint = value;
        }

        // public OutpostBuildingEntity ToOutpostBuildingEntity { get; private set; }

        #endregion


        #region Methods

        #region Override
        
        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            // ToOutpostBuildingEntity = buildingEntity as OutpostBuildingEntity;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}