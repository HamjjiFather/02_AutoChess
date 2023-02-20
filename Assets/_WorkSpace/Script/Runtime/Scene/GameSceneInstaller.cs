using AutoChess;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;
using Zenject;


namespace KKSFramework.InGame
{
    public class GameSceneInstaller : SceneInstaller
    {
        private static GameSceneInstaller _instance;

        public static GameSceneInstaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType (typeof (GameSceneInstaller)) as GameSceneInstaller;
                    if (_instance == null)
                    {
                        _instance = new GameObject ().AddComponent<GameSceneInstaller> ();
                        _instance.gameObject.name = _instance.GetType ().Name;
                    }
                }

                return _instance;
            }
        }


        /// <summary>
        /// 컨테이너에 해당 타입 리졸브.
        /// </summary>
        public T Resolve<T>() => Container.Resolve<T>();
        
        
        public override InitNavigationData InitPageInitNavigationData => new()
        {
            viewString = nameof(NavigationViewType.BasePage),
            actionOnFirst = OpenQuitPopup
        };


        public void OnPostInstall()
        {
            CreateCommonView();

            void CreateCommonView()
            {
                var statusView = NavigationHelper.OpenCommonViewAsync<StatusView>(nameof(StatusView));
            }
        }


        public void OnPostResolve()
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