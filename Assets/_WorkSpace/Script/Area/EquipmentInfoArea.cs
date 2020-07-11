using KKSFramework.GameSystem.GlobalText;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class EquipmentInfoArea : AreaBase<EquipmentModel>
    {
        #region Fields & Property
        
        public Text equipmentNameText;
                                                  
        public StarGradeArea starGradeArea;
                                                  
        public Image equipmentImage;
                                                  
        public StatusElement[] baseStatusElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void SetArea (EquipmentModel areaData)
        {
            equipmentNameText.GetTranslatedString (areaData.EquipmentData.Name);
            starGradeArea.SetArea (areaData.StarGrade);
            equipmentImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Equipment, areaData.EquipmentData.SpriteResName);
            
            baseStatusElements.Foreach (element => element.gameObject.SetActive (false));
            areaData.Status.Foreach ((status, index) =>
            {
                baseStatusElements[index].gameObject.SetActive (true);
                baseStatusElements[index].SetElement (areaData.GetBaseStatusModel(status.Key));
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}