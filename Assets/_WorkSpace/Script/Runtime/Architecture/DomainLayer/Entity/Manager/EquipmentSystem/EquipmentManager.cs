using System.Collections.Generic;
using JetBrains.Annotations;
using Zenject;

namespace AutoChess
{
    [UsedImplicitly]
    public class EquipmentManager : ManagerBase
    {
        public EquipmentManager()
        {
        }
        
        #region Fields & Property

        [Inject]
        private EquipmentRepository _equipmentRepository;

        /// <summary>
        /// 장비 아이템.
        /// </summary>
        private Dictionary<int, EquipmentEntity> _equipmentMap;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public EquipmentEntity GetEquipment(int uniqueIndex) => _equipmentMap[uniqueIndex];
        

        /// <summary>
        /// 장비를 획득함.
        /// </summary>
        public void ObtainEquipment(EquipmentEntity entity)
        {
            _equipmentMap.Add(entity.UniqueIndex, entity);
            var dto = new EquipmentDto(entity);
            _equipmentRepository.UpdateEquipment(dto);
        }


        /// <summary>
        /// 장비 데이터가 삭제됨.
        /// </summary>
        public void DeleteEquipment(int uniqueIndex)
        {
            _equipmentMap.Remove(uniqueIndex);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}