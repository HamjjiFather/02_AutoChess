using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.UI;
using UnityEngine;

namespace AutoChess
{
    public class SelectedCombineMaterialElement : ElementBase<SelectedCombineModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Property<Sprite> _materialImage;

        [Resolver]
        private ButtonExtension _materialButton;

        [Resolver]
        private GameObject _materialArea;

        [Resolver]
        private GameObject _slotImage;

#pragma warning restore CS0649

        public override SelectedCombineModel ElementData { get; set; } = new SelectedCombineModel ();

        public bool HasElementData => ElementData.CombineMaterialModel != null;

        private int _index;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _materialButton.AddListener (ClickCharacterButton);
        }

        #endregion


        #region Methods

        public void SetBaseIndex (int index)
        {
            _index = index;
        }


        public override void SetElement (SelectedCombineModel elementData)
        {
            ElementData = elementData;

            _materialArea.SetActive (true);
            _slotImage.SetActive (false);

            // _starGradeArea.SetArea (((CharacterData) elementData.CombineMaterialModel).StarGrade);
            _materialImage.Value = ((CharacterData) elementData.CombineMaterialModel).IconImageResources;
        }


        public void SetEmptyElement ()
        {
            _materialArea.SetActive (false);
            _slotImage.SetActive (true);

            ElementData.Empty ();
        }

        #endregion


        #region EventMethods

        private void ClickCharacterButton ()
        {
            ElementData.DeselectMaterial.Invoke (_index);
            SetEmptyElement ();
        }

        #endregion
    }
}