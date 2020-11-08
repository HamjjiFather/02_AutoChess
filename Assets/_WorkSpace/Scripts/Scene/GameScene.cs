using BaseFrame;
using BaseFrame.Navigation;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.InGame;
using KKSFramework.LocalData;
using MasterData;
using UnityEngine;

namespace AutoChess
{
    public class GameScene : SceneController
    {
        private const string BasePath = "MasterData";

        public override async UniTask InitializeAsync (Parameters parameters)
        {
            ProjectInstall.InitViewmodel ();
            await TsvTableData.LoadAsync (BasePath);
            ProjectInstall.InitTableDataViewmodel ();
            LocalDataHelper.LoadAllGameData ();
            ProjectInstall.InitLocalDataViewmodel ();

            CreateCommonView ();
            base.InitializeAsync (parameters).Forget ();

            void CreateCommonView ()
            {
            }

            async void OpenQuitPopup ()
            {
                var param = TreeNavigationHelper.SpawnParam ();
                param["msg"] = "?게임을 나가시겠습니까?";
                var popupCode = await TreeNavigationHelper.WaitForPopPushPopup (nameof(MessagePopup), param);
                if (popupCode == PopupEndCode.Ok)
                    Application.Quit ();
            }
        }

        public override Configuration GetRootViewConfiguration ()
        {
            var config = new Configuration.Builder ();
            return config.SetName (nameof(GamePage), true)
                .SetLayer (ContentLayer.Page)
                .Build ();
        }
    }
}