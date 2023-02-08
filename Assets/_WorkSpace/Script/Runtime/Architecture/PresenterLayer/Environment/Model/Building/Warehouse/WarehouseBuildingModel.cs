using Zenject;

namespace AutoChess
{
    public class WarehouseBuildingModel : BuildingModelBase, IInitializable
    {
        #region Fields & Property

        /// <summary>
        /// 인벤토리 공간.
        /// </summary>
        public int InventorySpace
        {
            get
            {
                return ItemDefine.BaseInventorySpace + AdditionalValue(); 

                int AdditionalValue()
                {
                    return Level switch
                    {
                        1 => 5,
                        2 => 10,
                        3 => 15,
                        4 => 20,
                        _ => 0,    
                    };
                }
            }
        }

        #endregion


        #region Methods

        #region Override

        public override void Build()
        {
            // throw new System.NotImplementedException();
        }

        public override void SpendTime()
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnLevelUp(int level)
        {
            base.OnLevelUp(level);
            // throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}