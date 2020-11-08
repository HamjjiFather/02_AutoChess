using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.Navigation;
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

        [Inject]
        private CharacterViewmodel _characterViewmodel;
        
#pragma warning restore CS0649
        
        private GamePage _gamePage;

        #endregion
        
        
        private void Awake ()
        {
            _equipmentInfoArea.SetBattleCharacterListComponent (_battleCharacterListArea);
        }


        #region Methods
        
        public override void Initialize ()
        {
            _gamePage = ProjectContext.Instance.Container.Resolve<GamePage> ();
            base.Initialize ();
        }
        
        public override UniTask ActiveLayout ()
        {
            _equipmentListArea.SetArea (ClickEquipmentElement);
            _battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            // EscapeEventManager.Instance.SetHookingEscapeEvent (ClickBackButton);
            return base.ActiveLayout ();
        }

        #endregion


        #region EventMethods

        
        private void ClickEquipmentElement (EquipmentModel equipmentModel)
        {
            _equipmentInfoArea.SetArea (equipmentModel);
        }

        #endregion
    }
}