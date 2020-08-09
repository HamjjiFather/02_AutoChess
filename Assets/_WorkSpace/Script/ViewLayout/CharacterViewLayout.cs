using KKSFramework;
using KKSFramework.Navigation;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class CharacterViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public CharacterInfoArea characterInfoArea;

        public CharacterListArea characterListArea;

        public BattleCharacterListArea battleCharacterListArea;

        public Button backButton;

#pragma warning disable CS0649


        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private GamePageView _gamePageView;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            backButton.onClick.AddListener (ClickBackButton);
        }

        #endregion


        #region Methods

        public override void Initialize ()
        {
            _gamePageView = ProjectContext.Instance.Container.Resolve<GamePageView> ();
            base.Initialize ();
        }

        public override UniTask ActiveLayout ()
        {
            characterListArea.SetArea (ClickCharacterElement);
            battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            return base.ActiveLayout ();
        }

        #endregion


        #region EventMethods

        private void ClickBackButton ()
        {
            _gamePageView.viewLayoutLoader.SetSubView (0);
        }


        private void ClickCharacterElement (CharacterModel characterModel)
        {
            characterInfoArea.SetArea (characterModel);
        }

        #endregion
    }
}