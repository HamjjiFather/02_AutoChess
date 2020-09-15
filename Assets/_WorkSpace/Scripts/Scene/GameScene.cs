using BaseFrame;
using BaseFrame.Navigation;
using Cysharp.Threading.Tasks;
using Helper;
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
            ProjectInstall.InitLocalDataViewmodel ();
            ProjectInstall.InitTableDataViewmodel ();

            CreateCommonView ();
            base.InitializeAsync (parameters).Forget ();

            void CreateCommonView ()
            {
            }

            void OpenQuitPopup ()
            {
                var param = new Parameters
                {
                    {
                        "struct", new MessagePopupStruct
                        {
                            ConfirmAction = Application.Quit,
                            Message = "?게임을 나가시겠습니까?"
                        }
                    }
                };
                TreeNavigationHelper.PushPopup (Popup.Message, param);
            }
        }

        
        public override Configuration GetRootViewConfiguration ()
        {
            var config = new Configuration.Builder ();
            return config.SetName (Page.GamePage.ToString (), true)
                .SetLayer (ContentLayer.Page)
                .Build ();
        }
    }
}