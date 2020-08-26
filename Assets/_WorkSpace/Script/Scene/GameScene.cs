using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;


namespace KKSFramework.InGame
{
    public class GameScene : SceneController
    {
        protected override async UniTask InitializeAsync()
        {
            ProjectInstall.InitViewmodel ();
            await TableDataManager.Instance.LoadTableDatas ();
            ProjectInstall.InitLocalDataViewmodel ();
            ProjectInstall.InitTableDataViewmodel ();
            
            CreateCommonView ();
            await NavigationHelper.OpenPage (NavigationViewType.HomePage, NavigationTriggerState.First, actionOnFirst:OpenQuitPopup);
            base.InitializeAsync ().Forget();

            void CreateCommonView ()
            {
                
            }
            
            void OpenQuitPopup ()
            {
                var popupStruct = new MessagePopupStruct
                {
                    ConfirmAction = Application.Quit,
                    Message = "?게임을 나가시겠습니까?"
                };
                NavigationHelper.OpenPopup (NavigationViewType.MessagePopup, popupStruct).Forget();
            }
        }
    }
}