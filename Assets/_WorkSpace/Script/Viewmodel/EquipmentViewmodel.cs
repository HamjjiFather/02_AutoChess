using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class EquipmentViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly Dictionary<int, EquipmentModel> _equipmentModels = new Dictionary<int, EquipmentModel> ();
        
        private static EquipmentModel _emptyEquipmentModel = new EquipmentModel ();
        public static EquipmentModel EmptyEquipmentModel => _emptyEquipmentModel;

        #endregion
        
        
        public override void Initialize ()
        {
        }


        public override void InitTableData ()
        {
            base.InitTableData ();
        }


        #region Methods

        public EquipmentModel GetEquipmentModel (int uniqueIndex)
        {
            return _equipmentModels.ContainsKey (uniqueIndex) ? _equipmentModels[uniqueIndex] : _emptyEquipmentModel;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}