using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;


namespace KKSFramework.InGame
{
    public class GameScene : SceneController
    {
        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = nameof (NavigationViewType.GamePage),
            actionOnFirst = OpenQuitPopup
        };

        protected override async UniTask InitializeAsync()
        {
            ProjectInstall.InitViewmodel ();
            await TableDataManager.Instance.LoadTableDatas ();
            ProjectInstall.InitLocalDataViewmodel ();
            ProjectInstall.InitTableDataViewmodel ();
            
            CreateCommonView ();
            base.InitializeAsync ().Forget();

            void CreateCommonView ()
            {
                
            }
        }
        
        private void OpenQuitPopup ()
        {
            var popupStruct = new MessagePopup.Model
            {
                confirmAction = Application.Quit,
                msg = "?게임을 나가시겠습니까?"
            };
                
            // NavigationManager.Instance.
            NavigationHelper.OpenPopup (NavigationViewType.MessagePopup, popupStruct).Forget();
        }
    }
}