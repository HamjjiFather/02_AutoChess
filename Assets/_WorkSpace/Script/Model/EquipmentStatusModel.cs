using System.Collections.Generic;
using System.Linq;
using AutoChess.Helper;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class EquipmentStatusModel : ModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        public List<EquipmentModel> EquipmentModels;

        public IEnumerable<int> EquipmentUId => EquipmentModels.Any ()
            ? EquipmentModels.Select (model => model.UniqueEquipmentId)
            : Enumerable.Repeat (0, 3);

        private List<BaseStatusModel> _baseStatusModels;

        public bool ExistEquipment => EquipmentModels.Any (x => x.UniqueEquipmentId.Equals (Constant.InvalidIndex));

        #endregion


        public EquipmentStatusModel ()
        {
            EquipmentModels = Enumerable.Repeat (EquipmentViewmodel.EmptyEquipmentModel, 3).ToList ();
        }


        #region Methods

        public EquipmentModel GetEquipmentModel (int index)
        {
            return EquipmentModels[index];
        }


        public void SetEquipmentModel (IEnumerable<EquipmentModel> equipmentModels)
        {
            EquipmentModels = equipmentModels.ToList ();
        }


        public void ChangeEquipmentModel (int index, EquipmentModel equipmentModel)
        {
            EquipmentModels[index] = equipmentModel;
        }


        public string DisplayValue (StatusType statusType)
        {
            var statusValueFormat = TableDataHelper.Instance.GetStatus (statusType).Format;
            return GetStatusValue (statusType).FloatToInt ().ToString (statusValueFormat);
        }


        public float GetStatusValue (StatusType statusType)
        {
            var value = EquipmentModels.Sum (x => x.GetBaseStatusModel (statusType).StatusValue);
            return value;
        }

        #endregion
    }
}