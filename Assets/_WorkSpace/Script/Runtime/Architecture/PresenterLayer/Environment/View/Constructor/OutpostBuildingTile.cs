using UniRx;
using UnityEngine;

namespace AutoChess.Presenter
{
    public class OutpostBuildingTile : BuildingTileBase, IDetectableObject, ISpawnPoint
    {
        #region Fields & Property

        public int outpostIndex;

        public Canvas canvas;
        
        public Transform spawnPoint;
        public Transform SpawnPoint
        {
            get => spawnPoint;
            set => spawnPoint = value;
        }

        public override BuildingEntityBase BuildingEntity { get; set; }

        public OutpostBuildingEntity ToOutpostBuildingEntity { get; private set; }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public bool Detect { get; set; }

        #endregion


        #region Methods

        #region Override

        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            ToOutpostBuildingEntity = buildingEntity as OutpostBuildingEntity;
            MessageBroker.Default.Publish(new RegisterDetectableMessage(this));
            
            Position = transform.position;
            
            OnFarAway();
        }


        #region IDetectableObject

        public void OnDetected()
        {
            Debug.Log($"{nameof(OnDetected)}: {name}");
            
            canvas.enabled = true;
            Detect = true;
        }


        public void OnFarAway()
        {
            Debug.Log($"{nameof(OnFarAway)}: {name}");
            
            canvas.enabled = false;
            Detect = false;
        }

        #endregion

        #endregion


        #region This

        #endregion


        #region Event

        public void OnTileButton_Click()
        {
            MessageBroker.Default.Publish(new OutpostMsg(outpostIndex));
        }

        #endregion

        #endregion
    }
}