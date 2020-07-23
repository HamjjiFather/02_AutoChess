using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class EquipmentInfoElement : ElementBase<EquipmentModel>
    {
        #region Fields & Property

        public GameObject equipmentObj;

        public GameObject emptyObj;
        
        public Image equipmentImage;

        public StarGradeArea equipmentStarGradeArea;

        public override EquipmentModel ElementData { get; set; }
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        public override void SetElement (EquipmentModel elementData)
        {
            var isEmpty = elementData.UniqueEquipmentId.Equals (Constant.InvalidIndex);
            emptyObj.SetActive (isEmpty);
            equipmentObj.SetActive (!isEmpty);

            if (isEmpty) return;
            equipmentStarGradeArea.SetArea (elementData.StarGrade);
            equipmentImage.sprite = elementData.IconImageResources;
        }
        
        
        #endregion
    }
}
