using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoListElementModel
    {
        public CharacterModel CharacterModel;

        public UnityAction<CharacterModel> ElementClick;
    }

    public class CharacterInfoListElement : PooingComponent, IElementBase<CharacterInfoListElementModel>, IResolveTarget
    {
        #region Fields & Property

        private Context _context => GetComponent<Context> ();

#pragma warning disable CS0649

        [Resolver]
        private StarGradeArea _starGradeArea;

        [Resolver]
        private Image _characterImage;

        [Resolver]
        private Text _characterNameText;

        [Resolver]
        private Button _elementButton;

        [Resolver]
        private GameObject _inBattleObj;

#pragma warning restore CS0649

        public CharacterInfoListElementModel ElementData { get; set; }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetElement (CharacterInfoListElementModel characterInfoListElementModel)
        {
            _context.Resolve (true);
            ElementData = characterInfoListElementModel;
            _starGradeArea.SetArea (characterInfoListElementModel.CharacterModel.StarGrade);
            _characterImage.sprite = characterInfoListElementModel.CharacterModel.IconImageResources;
            _characterNameText.text = LocalizeHelper.FromName (characterInfoListElementModel.CharacterModel.CharacterData.Name);
            _elementButton.onClick.RemoveAllListeners ();
            _elementButton.onClick.AddListener (() =>
            {
                characterInfoListElementModel.ElementClick.Invoke (characterInfoListElementModel.CharacterModel);
            });
        }

        #endregion
    }
}