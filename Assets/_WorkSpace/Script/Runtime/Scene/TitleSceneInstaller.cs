using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;

namespace KKSFramework.InGame
{
    public class TitleSceneInstaller : SceneInstaller
    {
        public override InitNavigationData InitPageInitNavigationData => new()
        {
            viewString = NavigationViewType.TitlePage.ToString ()
        };


        public override void Start()
        {
            base.Start();
            PushRootView().Forget();
        }
    }
}