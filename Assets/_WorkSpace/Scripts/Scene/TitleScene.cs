using Cysharp.Threading.Tasks;
using KKSFramework.LocalData;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;

namespace KKSFramework.InGame
{
    public class TitleScene : SceneController
    {
        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = NavigationViewType.TitlePage.ToString ()
        };

        
        
        protected override async UniTask InitializeAsync ()
        {
            LocalDataManager.Instance.SetSaveAction (LocalDataHelper.SaveAllGameData);
            await base.InitializeAsync ();
        }
    }
}