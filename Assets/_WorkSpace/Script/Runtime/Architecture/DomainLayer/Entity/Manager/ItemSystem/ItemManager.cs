using JetBrains.Annotations;
using Zenject;

namespace AutoChess
{
    public class ItemManager : ManagerBase
    {
        [Inject]
        private CurrencyManager _currencyManager;

        [Inject]
        private EquipmentManager _equipmentManager;

        public IItemEntityBase IdentifyItemToUniqueIndex(int uniqueIndex)
        {
            if (uniqueIndex / UniqueIndexDefine.BaseEquipmentUniqueIndex == 1)
            {
                return _equipmentManager.GetEquipment(uniqueIndex);
            }

            return default;
        }


        #region Obtain

        public void GetNewEquipment(EquipmentEntity entity)
        {
            _equipmentManager.ObtainEquipment(entity);
        }

        #endregion
    }
}