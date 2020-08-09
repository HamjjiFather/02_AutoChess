using KKSFramework;
using KKSFramework.Navigation;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class EquipmentViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public EquipmentInfoArea equipmentInfoArea;

        public EquipmentListArea equipmentListArea;

        public Button backButton;

#pragma warning disable CS0649
        
#pragma warning restore CS0649
        
        private GamePageView _gamePageView;

        #endregion
        
        
        private void Awake ()
        {
            backButton.onClick.AddListener (ClickBackButton);
        }


        #region Methods
        
        public override void Initialize ()
        {
            _gamePageView = ProjectContext.Instance.Container.Resolve<GamePageView> ();
            base.Initialize ();
        }
        
        public override UniTask ActiveLayout ()
        {
            equipmentListArea.SetArea (ClickEquipmentElement);
            return base.ActiveLayout ();
        }

        #endregion


        #region EventMethods
        
        private void ClickBackButton ()
        {
            _gamePageView.BackToMain ();
        }
        
        private void ClickEquipmentElement (EquipmentModel equipmentModel)
        {
            equipmentInfoArea.SetArea (equipmentModel);
        }

        #endregion
    }
}