using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class GraveyardViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private GraveyardCharacterInfoArea _characterInfoArea;

        [Resolver]
        private CharacterListArea _characterListArea;

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
            _characterListArea.SetArea (ClickCharacterElement, _characterViewmodel.AllDeathCharacterModels);
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