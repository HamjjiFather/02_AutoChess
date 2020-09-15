using System.Linq;
using BaseFrame;
using Helper;
using KKSFramework.Navigation;
using MasterData;
using UnityEngine.UI;

namespace AutoChess
{
    public class StatusElement : ElementBase<BaseStatusModel>
    {
        #region Fields & Property

        public Image statusIconImage;

        public Text statusNameText;

        public Text statusValueText;

        public Text statusGradeText;

#pragma warning disable CS0649

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

            var grade = StatusGradeRange.Manager.Values.Last (statusGrade =>
                statusGrade.Min <= baseStatusModel.GradeValue && statusGrade.Max > baseStatusModel.GradeValue);

            statusGradeText.text = grade.GradeString;
            statusNameText.text = LocalizeHelper.FromName (ElementData.StatusData.NameKey);
            statusValueText.text = ElementData.DisplayValue;
        }
        
        
        /// <summary>
        /// 캐릭터 능력치 표시.
        /// </summary>
        public void SetCharacterElement (StatusType statusType, CharacterModel characterModel)
        {
            ElementData = characterModel.GetBaseStatusModel (statusType);
            
            var equipmentStatusValue  = characterModel.EquipmentStatusModel.GetStatusValue (statusType);
            var grade = StatusGradeRange.Manager.Values.Last (statusGrade =>
                statusGrade.Min <= ElementData.GradeValue && statusGrade.Max > ElementData.GradeValue);
            var totalValue = ElementData.CombinedDisplayValue (equipmentStatusValue);
            var displayValueString = equipmentStatusValue.IsZero ()
                ? ElementData.DisplayValue : $"{totalValue} + ({characterModel.EquipmentStatusModel.DisplayValue (statusType)})";

            statusGradeText.text = grade.GradeString;
            statusNameText.text = LocalizeHelper.FromName (ElementData.StatusData.NameKey);
            statusValueText.text = displayValueString;
        }
        
        
        
        public void SetSubValueText (string subValueText)
        {
            statusValueText.text += subValueText;
        }

        #endregion
    }
}