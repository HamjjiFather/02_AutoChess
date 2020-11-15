using BaseFrame;
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
        
        
        
        protected override void OnInitialized ()
        {
            _gamePage = ProjectContext.Instance.Container.Resolve<GamePage> ();
        }


        protected override UniTask OnActiveAsync (Parameters parameters)
        {
            _equipmentListArea.SetArea (ClickEquipmentElement);
            _battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            return base.OnActiveAsync (parameters);
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