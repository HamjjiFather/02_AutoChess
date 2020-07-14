using KKSFramework.GameSystem.GlobalText;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class EquipmentInfoListElementModel
    {
        public EquipmentModel EquipmentModel;
        
        public UnityAction<EquipmentModel> ElementClick;
    }
    
    public class EquipmentInfoListElement : PooledObjectComponent, IElementBase<EquipmentInfoListElementModel>
    {
        #region Fields & Property
        
        public StarGradeArea starGradeArea;

        public Image equipmentImage;

        public Text equipmentNameText;

        public Button elementButton;

        public GameObject equippedObj;

#pragma warning disable CS0649

#pragma warning restore CS0649
        
        public EquipmentInfoListElementModel ElementData { get; set; }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        public void SetElement (EquipmentInfoListElementModel elementData)
        {
            starGradeArea.SetArea (elementData.EquipmentModel.StarGrade);
            equipmentImage.sprite = elementData.EquipmentModel.IconImageResources;
            equipmentNameText.GetTranslatedString (elementData.EquipmentModel.EquipmentData.Name);
            
            elementButton.onClick.RemoveAllListeners ();
            elementButton.onClick.AddListener (() =>
            {
                elementData.ElementClick.Invoke (elementData.EquipmentModel);
            });
        }
        
        #endregion
    }
}