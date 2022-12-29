using AutoChess.Bundle;
using JetBrains.Annotations;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess
{
    public struct EquipmentDao : IDAO
    {
        
    }
    
    [UsedImplicitly]
    public class EquipmentRepository : IRepository<EquipmentDao>
    {
        #region Fields & Property

        [Inject]
        private EquipmentBundle _equipmentBundle;
        
        #endregion


        #region Methods

        #region Override
        
        public EquipmentDao Read(int index)
        {
            return default;
        }

        public void Update(EquipmentDao entity)
        {
        }

        public void Delete(EquipmentDao entity)
        {
        }


        public int UpdateUniqueIndex => _equipmentBundle.uniqueIndex++;

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion


    }
}