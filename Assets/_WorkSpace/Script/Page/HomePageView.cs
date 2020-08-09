using KKSFramework.Navigation;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace AutoChess
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
            NavigationHelper.OpenPage (NavigationViewType.GamePage, pushValue:stageInputField.text).Forget();
        }

        #endregion
    }
}