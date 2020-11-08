using Helper;
using KKSFramework.Navigation;
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
    
    public class EquipmentInfoListElement : MonoBehaviour, IElementBase<EquipmentInfoListElementModel>
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

        public virtual void SetElement (EquipmentInfoListElementModel elementData)
        {
            ElementData = elementData;
            SetBaseInfo ();
            
            elementButton.onClick.RemoveAllListeners ();
            elementButton.onClick.AddListener (() =>
            {
                elementData.ElementClick.Invoke (elementData.EquipmentModel);
            });
        }


        protected void SetBaseInfo ()
        {
            starGradeArea.SetArea (ElementData.EquipmentModel.StarGrade);
            equipmentImage.sprite = ElementData.EquipmentModel.IconImageResources;
            equipmentNameText.text = LocalizeHelper.FromName (ElementData.EquipmentModel.EquipmentData.Name);
        }
        
        #endregion
    }
}