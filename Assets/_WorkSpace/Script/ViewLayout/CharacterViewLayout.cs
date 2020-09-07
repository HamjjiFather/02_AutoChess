using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class CharacterViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private CharacterInfoArea _characterInfoArea;

        [Resolver]
        private CharacterListArea _characterListArea;

        [Resolver]
        private BattleCharacterListArea _battleCharacterListArea;

        [Resolver]
        private Button _backButton;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private GamePageView _gamePageView;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _backButton.onClick.AddListener (ClickBackButton);
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
            _characterListArea.SetArea (ClickCharacterElement);
            _battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            // EscapeEventManager.Instance.SetHookingEscapeEvent (ClickBackButton);
            return base.ActiveLayout ();
        }

        #endregion


        #region EventMethods

        private void ClickBackButton ()
        {
            _gamePageView.BackToMain ();
        }


        private void ClickCharacterElement (CharacterModel characterModel)
        {
            _characterInfoArea.SetArea (characterModel);
        }

        #endregion
    }
}