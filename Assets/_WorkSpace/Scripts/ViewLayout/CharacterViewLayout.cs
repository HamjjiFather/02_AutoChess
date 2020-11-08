using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.InGame;
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

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void Initialize ()
        {
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


        private void ClickCharacterElement (CharacterModel characterModel)
        {
            _characterInfoArea.SetArea (characterModel);
        }

        #endregion
    }
}