using Cysharp.Threading.Tasks;
using KKSFramework.LocalData;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;

namespace KKSFramework.InGame
{
    public class TitleSceneInstaller : SceneInstaller
    {
        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = NavigationViewType.TitlePage.ToString ()
        };


        protected override void Awake()
        {
            LocalDataProjectManager.Instance.SetSaveAction (LocalDataHelper.SaveAllGameData);
            base.Awake();
        }
    }
}