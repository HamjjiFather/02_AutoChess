using KKSFramework;
using KKSFramework.Localization;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class EquipmentInfoArea : AreaBase<EquipmentModel>
    {
        #region Fields & Property
        
        public Text equipmentNameText;
                                                  
        public StarGradeArea starGradeArea;
                                                  
        public Image equipmentImage;

        public Button equipButton;
        
        public StatusElement[] baseStatusElements;

        public GameObject[] baseStatusElementLineObjs;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;
        
#pragma warning restore CS0649

        private EquipmentModel _equipmentModel;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            equipButton.onClick.AddListener (ClickEquipButton);
        }

        #endregion


        #region Methods
        
        public override void SetArea (EquipmentModel areaData)
        {
            _equipmentModel = areaData;
            equipmentNameText.GetTranslatedString (areaData.EquipmentData.Name);
            starGradeArea.SetArea (areaData.StarGrade);
            equipmentImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Equipment, areaData.EquipmentData.SpriteResName);
            
            baseStatusElements.Foreach (element => element.gameObject.SetActive (false));
            baseStatusElementLineObjs.Foreach (obj => obj.SetActive (false));
            areaData.Status.Foreach ((status, index) =>
            {
                baseStatusElements[index].gameObject.SetActive (true);
                baseStatusElements[index].SetElement (areaData.GetBaseStatusModel(status.Key));

                var objIndex = index - 1;
                
                if(objIndex >= 0 && objIndex <= baseStatusElementLineObjs.Length - 1)
                    baseStatusElementLineObjs[objIndex].SetActive (true);
            });
            
            SetEquipState (true);
        }


        private void SetEquipState (bool active)
        {
            equipButton.gameObject.SetActive (active);
        }

        #endregion


        #region EventMethods

        private void ClickEquipButton ()
        {
            // battleCharacterListArea.SetElementClickActions (ClickCharacter);
            
            void ClickCharacter (CharacterModel characterModel)
            {
                characterModel.ChangeEquipmentModel (0, _equipmentModel);
                _characterViewmodel.SaveCharacterData ();
                SetEquipState (false);
            }
        }

        #endregion
    }
}