using System.Linq;
using KKSFramework;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using MasterData;
using UnityEngine.UI;

namespace AutoChess
{
    public class StatusElement : ElementBase<BaseStatusModel>, IResolveTarget
    {
        #region Fields & Property

        public Image statusIconImage;


#pragma warning disable CS0649

        [Resolver]
        private Property<string> _statusNameText;

        [Resolver]
        private Property<string> _statusValueText;

        [Resolver]
        private Property<string> _statusGradeText;


#pragma warning restore CS0649

        public override BaseStatusModel ElementData { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// 기본 능력치 표시.
        /// </summary>
        public override void SetElement (BaseStatusModel baseStatusModel)
        {
            ElementData = baseStatusModel;

            var grade = TableDataManager.Instance.StatusGradeRangeDict.Values.Last (statusGrade =>
                statusGrade.Min <= baseStatusModel.GradeValue && statusGrade.Max > baseStatusModel.GradeValue);

            _statusGradeText.Value = grade.GradeString;
            _statusNameText.Value = LocalizeHelper.FromName (ElementData.StatusData.NameKey);
            _statusValueText.Value = ElementData.DisplayValue;
        }


        /// <summary>
        /// 캐릭터 능력치 표시.
        /// </summary>
        public void SetCharacterElement (StatusType statusType, CharacterModel characterModel)
        {
            ElementData = characterModel.GetBaseStatusModel (statusType);

            var equipmentStatusValue = characterModel.EquipmentStatusModel.GetStatusValue (statusType);
            var grade = TableDataManager.Instance.StatusGradeRangeDict.Values.Last (statusGrade =>
                statusGrade.Min <= ElementData.GradeValue && statusGrade.Max > ElementData.GradeValue);
            var totalValue = ElementData.CombinedDisplayValue (equipmentStatusValue);
            var displayValueString = equipmentStatusValue.IsZero ()
                ? ElementData.DisplayValue
                : $"{totalValue} + ({characterModel.EquipmentStatusModel.DisplayValue (statusType)})";

            _statusGradeText.Value = grade.GradeString;
            _statusNameText.Value = LocalizeHelper.FromName (ElementData.StatusData.NameKey);
            _statusValueText.Value = displayValueString;
        }


        public void SetSubValueText (string subValueText)
        {
            _statusValueText.Value += subValueText;
        }

        #endregion
    }
}