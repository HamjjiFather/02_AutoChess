using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;


namespace KKSFramework.InGame
{
    public class EntrySceneInstaller : SceneInstaller
    {
        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = nameof (NavigationViewType.EntryPage),
            actionOnFirst = null
        };


        public override void Start ()
        {
            base.Start ();
        }
    }
}