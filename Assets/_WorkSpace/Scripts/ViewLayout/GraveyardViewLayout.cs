using KKSFramework;
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
        private CharacterManager _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void Initialize (ViewLayoutLoaderBase loader)
        {
            base.Initialize (loader);
        }


        protected override UniTask OnActiveAsync (object parameters)
        {
            _characterListArea.SetArea (ClickCharacterElement, _characterViewmodel.AllDeathCharacterModels, true,
                ref _characterViewmodel.IsDeathCharacterDataChanged);
            return base.OnActiveAsync (parameters);
        }

        #endregion


        #region EventMethods

        private void ClickCharacterElement (CharacterData characterData)
        {
            _characterInfoArea.SetArea (characterData);
        }

        #endregion
    }
}