using KKSFramework.Navigation;
using UniRx.Async;
using UnityEngine.UI;

namespace HexaPuzzle
{
    public class HomePageView : PageViewBase
    {
        #region Fields & Property

        public InputField stageInputField;

        public Button gameStartButton;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            gameStartButton.onClick.AddListener (ClickStartButton);
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        private void ClickStartButton ()
        {
            NavigationHelper.OpenPage (NavigationViewState.GamePage, pushValue:stageInputField.text).Forget();
        }

        #endregion
    }
}