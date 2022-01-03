using KKSFramework;
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
        private ButtonExtension _combineButton;

        [Inject]
        private CharacterManager _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _combineButton.AddListener (ClickCombineCharacterButton);
        }

        #endregion


        #region Methods

        protected override UniTask OnActiveAsync (object parameters)
        {
            _characterListArea.SetArea (ClickCharacterElement, true, ref _characterViewmodel.IsDataChanged);
            _battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            return base.OnActiveAsync (parameters);
        }

        #endregion


        #region EventMethods

        private void ClickCharacterElement (CharacterData characterData)
        {
            _characterInfoArea.SetArea (characterData);
        }


        private void ClickCombineCharacterButton ()
        {
            // var param = new Parameters
            // {
            //     [CombineViewLayout.MaterialParamKey] = _characterInfoArea.AreaData,
            //     [CombineViewLayout.IsCharacterParamKey] = true
            // };
            // ViewLayoutLoader.SetSubView (4, param);
        }

        #endregion
    }
}