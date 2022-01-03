using KKSFramework;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.Navigation;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class EquipmentViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private EquipmentInfoArea _equipmentInfoArea;

        [Resolver]
        private EquipmentListArea _equipmentListArea;

        [Resolver]
        private BattleCharacterListArea _battleCharacterListArea;
        
        [Resolver]
        private Button _equipButton;
        
        [Resolver]
        private Button _combineButton;
        
        [Resolver]
        private Button _reinforceButton;
        
        [Resolver]
        private Button _enchantButton;

        [Inject]
        private CharacterManager _characterViewmodel;
        
#pragma warning restore CS0649
        
        private GamePage _gamePage;

        #endregion
        
        
        private void Awake ()
        {
            _equipmentInfoArea.SetBattleCharacterListComponent (_battleCharacterListArea);
            _equipButton.onClick.AddListener (ClickEquipButton);
            _combineButton.onClick.AddListener (ClickCombineEquipmentButton);
        }


        #region Methods
        
        
        
        protected override void OnInitialized ()
        {
            _gamePage = ProjectContext.Instance.Container.Resolve<GamePage> ();
        }


        protected override UniTask OnActiveAsync (object parameters)
        {
            _equipmentListArea.SetArea (ClickEquipmentElement);
            _battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            return base.OnActiveAsync (parameters);
        }
        
        
        private void SetEquipState (bool active)
        {
            _equipButton.gameObject.SetActive (active);
        }

        #endregion


        #region EventMethods

        
        private void ClickEquipmentElement (EquipmentModel equipmentModel)
        {
            _equipmentInfoArea.SetArea (equipmentModel);
        }
        
        
        private void ClickEquipButton ()
        {
            _battleCharacterListArea.SetElementClickActions (ClickCharacter);

            void ClickCharacter (CharacterData characterModel)
            {
                characterModel.ChangeEquipmentModel (0, _equipmentInfoArea.AreaData);
                _characterViewmodel.SaveCharacterData ();
                SetEquipState (false);
            }
        }
        
        private void ClickCombineEquipmentButton ()
        {
            // var param = new Parameters
            // {
            //     [CombineViewLayout.MaterialParamKey] = _equipmentInfoArea.AreaData,
            //     [CombineViewLayout.IsCharacterParamKey] = false
            // };
            // ViewLayoutLoader.SetSubView (4, param);
        }
        

        #endregion
    }
}