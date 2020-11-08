using System;
using AutoChess;
using BaseFrame;
using BaseFrame.Navigation;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Helper
{
    public enum Scene
    {
        None,
        BootStrap,
        Entry,
        Title,
        Game
    }

    
    public enum SystemPopup
    {
        Alert,
        CheatDetectPopup
    }

    public static class TreeNavigationHelper
    {
        #region Scene

        public static UniTask ChangeSceneAsync(Scene scene, Parameters parameters = null,
            TransitionType transitionType = TransitionType.Fade)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(scene.ToString(), true).SetTransitionType(transitionType)
                .SetParameters(parameters).Build();

            return TreeNavigation.Instance.ChangeSceneAsync(build);
        }

        public static void ChangeScene(Scene scene, Parameters parameters = null,
            TransitionType transitionType = TransitionType.Fade)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(scene.ToString(), true).SetTransitionType(transitionType)
                .SetParameters(parameters).Build();

            TreeNavigation.Instance.ChangeSceneAsync(build).Forget();
        }

        #endregion


        #region Page

        /// <summary>
        /// 루트 페이지 설정
        /// </summary>
        public static UniTask<PageController> PushRootPageAsync(string pageName, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(pageName).SetPrefabName(pageName)
                .SetLayer(ContentLayer.Page).SetTransitionType(transitionType).SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushRootAsync<PageController>(build);
        }


        public static void PushRootPage(string pageName, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            PushRootPageAsync(pageName, parameters, transitionType).Forget();
        }


        /// <summary>
        /// 홈 페이지로 되돌아 가기
        /// </summary>
        public static UniTask<PageController> GoBackHomeAsync(TransitionType transitionType = TransitionType.None)
        {
            return TreeNavigation.Instance.GoBackRoot<PageController>(transitionType);
        }


        public static void GoBackHome(TransitionType transitionType = TransitionType.None)
        {
            GoBackHomeAsync().Forget();
        }


        /// <summary>
        /// 페이지 전환.
        /// 이전 히스토리 유지
        /// </summary>
        public static UniTask<PageController> PushPageAsync(string pageName, Parameters parameters = null, bool pop = false,
            TransitionType transitionType = TransitionType.None)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(pageName).SetPrefabName(pageName)
                .SetLayer(ContentLayer.Page).SetTransitionType(transitionType).SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushBranchAsync<PageController>(build, pop);
        }


        public static void PushPage(string pageName, Parameters parameters = null, bool pop = false,
            TransitionType transitionType = TransitionType.None)
        {
            PushPageAsync(pageName, parameters, pop, transitionType).Forget();
        }


        /// <summary>
        /// 페이지 전환.
        /// 기존 페이지 히스토리는 제거된다.
        /// </summary>
        public static UniTask<PageController> ChangePageAsync(string pageName, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(pageName).SetPrefabName(pageName)
                .SetLayer(ContentLayer.Page).SetTransitionType(transitionType).SetParameters(parameters).Build();

            return TreeNavigation.Instance.ChangeBranch<PageController>(build);
        }


        public static void ChangePage(string pageName, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            ChangePageAsync(pageName, parameters, transitionType).Forget();
        }

        #endregion


        #region Popup
        
        
        public static void PushPopup(string popupName, Parameters parameters = null)
        {
            PushPopupAsync(popupName, parameters).Forget();
        }
        
        
        public static UniTask<PopupController> PushPopupAsync(string popupName, Parameters parameters = null)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(popupName)
                .SetPrefabName(popupName).SetLayer(ContentLayer.Popup)
                .SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushLeafAsync<PopupController>(build);
        }
        
        
        public static async UniTask<PopupEndCode> WaitForPopPushPopup(string popupName, Parameters parameters = null)
        {
            var popupTask = await PushPopupAsync(popupName, parameters);

            return await popupTask.WaitForPopAsync();
        }
        

        #endregion


        public static void PushSystem(SystemPopup popup, Parameters parameters = null)
        {
            PushSystemAsync(popup, parameters).Forget();
        }

        public static UniTask<PopupController> PushSystemAsync(SystemPopup popup, Parameters parameters = null)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(popup.ToString())
                .SetPrefabName(popup.ToString())
                .SetLayer(ContentLayer.System)
                .SetAutoSorting(false)
                .SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushLeafAsync<PopupController>(build);
        }


        public static async UniTask<PopupEndCode> WaitForHidePushSystemAsync(SystemPopup popup,
            Parameters parameters = null)
        {
            var popupTask = await PushSystemAsync(popup, parameters);
            return await popupTask.WaitForPopAsync();
        }


        public static UniTask Pop(bool restoreLast = true, TransitionType transitionType = TransitionType.None)
        {
            return TreeNavigation.Instance.PopAsync(restoreLast, transitionType);
        }


        /// <summary>
        /// 최상의 뷰에서 인자로 넘어온 view 까지 전부 Pop
        /// </summary>
        public static UniTask PopTo(string pageName, bool restoreLast = true,
            TransitionType transitionType = TransitionType.None)
        {
            return TreeNavigation.Instance.PopToAsync(pageName, restoreLast, transitionType);
        }


        /// <summary>
        /// 최상의 뷰에서 인자로 넘어온 view 까지 전부 Pop
        /// </summary>
        public static UniTask PopToPopup(string popupName, bool restoreLast = true,
            TransitionType transitionType = TransitionType.None)
        {
            return TreeNavigation.Instance.PopToAsync(popupName, restoreLast, transitionType);
        }


        #region Helper

        public static async UniTask TransitionActionAsync(TransitionType transitionType, Action between,
            Action complete = null)
        {
            await TreeNavigation.Instance.BeginTransitionAsync(transitionType);
            between?.Invoke();
            await TreeNavigation.Instance.EndTransitionAsync(transitionType);
            complete?.Invoke();
        }


        public static void LockBackKeyProcess(bool active)
        {
            TreeNavigation.Instance.LockBackKey(active);
        }


        public static void BackKey()
        {
            TreeNavigation.Instance.ProcessBackKey();
        }


        public static bool IsTopMostViewPage(string pageName) => TreeNavigation.Instance.IsTopMostView(pageName);

        public static bool IsTopMostViewPopup(string popupName) => TreeNavigation.Instance.IsTopMostView(popupName);


        public static T GetViewControllerPage<T>(string pageName) where T : ViewController
        {
            return TreeNavigation.Instance.GetViewController<T>(pageName);
        }


        public static T GetViewControllerPopup<T>(string popupName) where T : ViewController
        {
            return TreeNavigation.Instance.GetViewController<T>(popupName);
        }


        public static T GetViewController<T>(SystemPopup popup) where T : ViewController
        {
            return TreeNavigation.Instance.GetViewController<T>(popup.ToString());
        }


        public static Parameters SpawnParam()
        {
            return ParametersPool.Get();
        }


        public static void DespawnParam(Parameters parameters)
        {
            ParametersPool.Release(parameters);
        }


        public static Camera GetContentCamera()
        {
            return TreeNavigation.Instance.ViewCamera;
        }


        public static void BeginNetworkProgress(bool isLazy = true)
        {
            TreeNavigation.Instance.networkProgressController.Play(isLazy);
        }


        public static void EndNetworkProgress()
        {
            TreeNavigation.Instance.networkProgressController.Stop();
        }


        public static void SetLockTouch(bool active)
        {
            TreeNavigation.Instance.LockTouch(active);
        }


        public static string ToPagePrefabName(string pageName)
        {
            return pageName.CamelToSnakeCase() + "_page";
        }

        #endregion


        #region Common

        #endregion
    }
}