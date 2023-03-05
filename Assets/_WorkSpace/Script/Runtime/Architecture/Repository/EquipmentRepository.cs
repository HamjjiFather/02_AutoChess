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
    public class EquipmentRepository : IRepository, IUniqueIndexIssuancer
    {
        #region Fields & Property

        public int UniqueIndex { get; set; }
        
        private EquipmentBundle _equipmentBundle;
        
        #endregion


        #region Methods

        #region Override
        
        public int GetUniqueIndex() => _equipmentBundle.uniqueIndex++;

        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        private void Construct(EquipmentBundle equipmentBundle)
        {
            _equipmentBundle = equipmentBundle;
            UniqueIndex = _equipmentBundle.uniqueIndex;
            EquipmentGenerator.UniqueIndexIssuancer = this;
        }

        public void UpdateEquipment(EquipmentDto dto)
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion


    }
}