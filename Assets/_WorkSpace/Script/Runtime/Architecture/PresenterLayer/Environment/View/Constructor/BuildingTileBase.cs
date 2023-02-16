using KKSFramework.Navigation;
using UniRx;
using UnityEngine;

namespace AutoChess.Presenter
{
    public abstract class BuildingTileBase : MonoBehaviour
    {
        #region Fields & Property

        public abstract BuildingEntityBase BuildingEntity { get; set; }

        /// <summary>
        /// 레벨에 따른 건축물 스프라이트.
        /// </summary>
        public Sprite[] buildingTileByLevel;

        /// <summary>
        /// 건축물 스프라이트 렌더러.
        /// </summary>
        public SpriteRenderer buildingSrenderer;

        /// <summary>
        /// 이벤트 감시자.
        /// </summary>
        public EnvironmentMouseDownDetector tileTriggerCollider;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            
        }

        private void OnMouseDown()
        {
            // _blackSmithBuildingTile.OnMouseDown();
        }

        #endregion


        #region This

        public virtual void Initialize(BuildingEntityBase buildingEntity)
        {
            BuildingEntity = buildingEntity;

            tileTriggerCollider.OnMouseDownEvent += OnSelected;
            BuildingEntity.OnLevelUpEvent += BuildingSpriteChange;
            BuildingSpriteChange(BuildingEntity.Level);
        }


        protected void BuildingSpriteChange(int level)
        {
            var index = level - 1;
            buildingSrenderer.sprite = buildingTileByLevel[index];
        }


        public virtual void OnSelected()
        {
            MessageBroker.Default.Publish(new OpenBuildingMenuMsg(BuildingEntity.BuildingTableData.BuildingType));
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}