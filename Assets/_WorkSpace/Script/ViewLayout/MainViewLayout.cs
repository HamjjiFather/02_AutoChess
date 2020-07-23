using KKSFramework;
using KKSFramework.Navigation;
using UniRx.Async;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class MainViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public Button[] layoutViewButton;

        public Button fieldViewButton;
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        private GamePageView _gamePageView;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            layoutViewButton.Foreach ((button, index) =>
            {
                button.onClick.AddListener (() => ClickLayoutViewButton(index));
            });
            
            fieldViewButton.onClick.AddListener (ClickFieldButton);
        }

        #endregion


        #region Methods

        public override void Initialize ()
        {
            _gamePageView = ProjectContext.Instance.Container.Resolve<GamePageView> ();
        }

        #endregion


        #region EventMethods

        private void ClickLayoutViewButton (int index)
        {
            _gamePageView.viewLayoutLoader.SetSubView (index);
        }
        
        
        private void ClickFieldButton ()
        {
            NavigationHelper.OpenPage (NavigationViewState.FieldPage).Forget();
        }

        #endregion
    }
}