using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;


namespace KKSFramework.InGame
{
    public class GameSceneInstaller : SceneInstaller
    {
        public override InitNavigationData InitPageInitNavigationData => new()
        {
            viewString = nameof(NavigationViewType.GamePage),
            actionOnFirst = OpenQuitPopup
        };


        [UsedImplicitly]
        public void PushRootView()
        {
            base.PushRootView().Forget();
        }


        private void OpenQuitPopup()
        {
            // NavigationManager.Instance.
            // NavigationHelper.OpenPopup(NavigationViewType.MessagePopup, popupStruct).Forget();
        }
    }
}