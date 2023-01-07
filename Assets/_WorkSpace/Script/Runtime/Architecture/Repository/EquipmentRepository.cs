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
    public class EquipmentRepository : IRepository<EquipmentDao>, IUniqueIndexIssuancer
    {
        #region Fields & Property

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


        public int UniqueIndex { get; set; }
        
        public int GetUniqueIndex() => _equipmentBundle.uniqueIndex++;

        #endregion


        #region This

        private void Construct(EquipmentBundle equipmentBundle)
        {
            _equipmentBundle = equipmentBundle;
            UniqueIndex = _equipmentBundle.uniqueIndex;
            EquipmentGenerator.UniqueIndexIssuancer = this;
        }

        #endregion


        #region Event

        #endregion

        #endregion


    }
}