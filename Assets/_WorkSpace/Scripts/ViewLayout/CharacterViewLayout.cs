using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.UI;
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
        private ButtonExtension _syntheticButton;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _syntheticButton.AddListener (ClickSyntheticCharacterButton);
        }

        #endregion


        #region Methods

        protected override UniTask OnActiveAsync (Parameters parameters)
        {
            _characterListArea.SetArea (ClickCharacterElement, true, ref _characterViewmodel.IsDataChanged);
            _battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            return base.OnActiveAsync (parameters);
        }

        #endregion


        #region EventMethods

        private void ClickCharacterElement (CharacterModel characterModel)
        {
            _characterInfoArea.SetArea (characterModel);
        }


        private void ClickSyntheticCharacterButton ()
        {
            var param = new Parameters
            {
                [CharacterSyntheticViewLayout.SyntheticCharacterViewLayoutParamKey] = _characterInfoArea.AreaData
            };
            ViewLayoutLoader.SetSubView (4, param);
        }

        #endregion
    }
}