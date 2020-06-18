using System.Linq;
using KKSFramework.GameSystem.GlobalText;
using KKSFramework.Navigation;
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

        public override void SetElement (BaseStatusModel baseStatusModel)
        {
            ElementData = baseStatusModel;

            var grade = TableDataManager.Instance.StatusGradeRangeDict.Values.Last (statusGrade =>
                statusGrade.Min <= baseStatusModel.GradeValue && statusGrade.Max > baseStatusModel.GradeValue);

            statusGradeText.text = grade.GradeString;
            statusNameText.GetTranslatedString (ElementData.StatusData.NameKey);
            statusValueText.text = ElementData.DisplayValue;
        }

        #endregion
    }
}