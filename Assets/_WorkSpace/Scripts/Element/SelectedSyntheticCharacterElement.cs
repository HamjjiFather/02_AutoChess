using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.UI;
using UnityEngine;

namespace AutoChess
{
    public class SelectedSyntheticCharacterElement : ElementBase<SelectedSyntheticCharacterModel>, IResolveTarget
    {
        #region Fields & Property
        
#pragma warning disable CS0649

        [Resolver]
        private StarGradeArea _starGradeArea;

        [Resolver]
        private Property<Sprite> _characterImage;

        [Resolver]
        private ButtonExtension _characterButton;

        [Resolver]
        private GameObject _characterArea;

        [Resolver]
        private GameObject _slotImage;

#pragma warning restore CS0649

        public override SelectedSyntheticCharacterModel ElementData { get; set; } = new SelectedSyntheticCharacterModel ();

        public bool HasElementData => ElementData.CharacterModel != null;

        private int _index;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _characterButton.AddListener (ClickCharacterButton);
        }

        #endregion


        #region Methods

        public void SetBaseIndex (int index)
        {
            _index = index;
        }
        

        public override void SetElement (SelectedSyntheticCharacterModel elementData)
        {
            ElementData = elementData;
            
            _characterArea.SetActive (true);
            _slotImage.SetActive (false);
            
            _starGradeArea.SetArea (elementData.CharacterModel.StarGrade);
            _characterImage.Value = elementData.CharacterModel.IconImageResources;
        }


        public void SetEmptyElement ()
        {
            _characterArea.SetActive (false);
            _slotImage.SetActive (true);

            ElementData.Empty ();
        }
        

        #endregion


        #region EventMethods

        private void ClickCharacterButton ()
        {
            ElementData.DeselectCharacter.Invoke (_index);
            SetEmptyElement ();
        }

        #endregion
    }
}