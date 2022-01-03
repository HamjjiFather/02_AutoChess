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
        public CharacterData CharacterData;

        public UnityAction<CharacterData> ElementClick;
    }

    public class CharacterInfoListElement : MonoBehaviour, IElementBase<CharacterInfoListElementModel>, IResolveTarget
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
            _starGradeArea.SetArea (characterInfoListElementModel.CharacterData.StarGrade);
            _characterImage.sprite = characterInfoListElementModel.CharacterData.IconImageResources;
            _characterNameText.text = LocalizeHelper.FromName (characterInfoListElementModel.CharacterData.CharacterTable.Name);
            _elementButton.onClick.RemoveAllListeners ();
            _elementButton.onClick.AddListener (() =>
            {
                characterInfoListElementModel.ElementClick.Invoke (characterInfoListElementModel.CharacterData);
            });
        }

        #endregion
    }
}