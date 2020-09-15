using System;
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


    /// <summary>
    /// Page Prefab 이름과 동일해야 한다.
    /// </summary>
    public enum Page
    {
        None,
        EntyPage,
        TitlePage,
        GamePage,
        AdventurePage
    }

    public enum Popup
    {
        None,
        PausePopup,
        ResultPopup,
        SettingsPopup,
        Message,
        AdventureResultPopup,
        FormationPopup,
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
        public static UniTask<PageController> PushRootPageAsync(Page page, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(page.ToString()).SetPrefabName(page.ToString())
                .SetLayer(ContentLayer.Page).SetTransitionType(transitionType).SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushRootAsync<PageController>(build);
        }


        public static void PushRootPage(Page page, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            PushRootPageAsync(page, parameters, transitionType).Forget();
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
        public static UniTask<PageController> PushPageAsync(Page page, Parameters parameters = null, bool pop = false,
            TransitionType transitionType = TransitionType.None)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(page.ToString()).SetPrefabName(page.ToString())
                .SetLayer(ContentLayer.Page).SetTransitionType(transitionType).SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushBranchAsync<PageController>(build, pop);
        }


        public static void PushPage(Page page, Parameters parameters = null, bool pop = false,
            TransitionType transitionType = TransitionType.None)
        {
            PushPageAsync(page, parameters, pop, transitionType).Forget();
        }


        /// <summary>
        /// 페이지 전환.
        /// 기존 페이지 히스토리는 제거된다.
        /// </summary>
        public static UniTask<PageController> ChangePageAsync(Page page, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(page.ToString()).SetPrefabName(page.ToString())
                .SetLayer(ContentLayer.Page).SetTransitionType(transitionType).SetParameters(parameters).Build();

            return TreeNavigation.Instance.ChangeBranch<PageController>(build);
        }


        public static void ChangePage(Page page, Parameters parameters = null,
            TransitionType transitionType = TransitionType.None)
        {
            ChangePageAsync(page, parameters, transitionType).Forget();
        }

        #endregion


        #region Popup

        public static void PushPopup(Popup popup, Parameters parameters = null)
        {
            PushPopupAsync(popup, parameters).Forget();
        }


        public static UniTask<PopupController> PushPopupAsync(Popup popup, Parameters parameters = null)
        {
            var config = new Configuration.Builder();
            var build = config.SetName(popup.ToString())
                .SetPrefabName(popup.ToString()).SetLayer(ContentLayer.Popup)
                .SetParameters(parameters).Build();

            return TreeNavigation.Instance.PushLeafAsync<PopupController>(build);
        }


        public static async UniTask<PopupEndCode> WaitForPopPushPopup(Popup popup, Parameters parameters = null)
        {
            var popupTask = await PushPopupAsync(popup, parameters);

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
        public static UniTask PopTo(Page toPage, bool restoreLast = true,
            TransitionType transitionType = TransitionType.None)
        {
            return TreeNavigation.Instance.PopToAsync(toPage.ToString(), restoreLast, transitionType);
        }


        /// <summary>
        /// 최상의 뷰에서 인자로 넘어온 view 까지 전부 Pop
        /// </summary>
        public static UniTask PopTo(Popup toPopup, bool restoreLast = true,
            TransitionType transitionType = TransitionType.None)
        {
            return TreeNavigation.Instance.PopToAsync(toPopup.ToString(), restoreLast, transitionType);
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


        public static bool IsTopMostView(Page page) => TreeNavigation.Instance.IsTopMostView(page.ToString());

        public static bool IsTopMostView(Popup popup) => TreeNavigation.Instance.IsTopMostView(popup.ToString());


        public static T GetViewController<T>(Page page) where T : ViewController
        {
            return TreeNavigation.Instance.GetViewController<T>(page.ToString());
        }


        public static T GetViewController<T>(Popup popup) where T : ViewController
        {
            return TreeNavigation.Instance.GetViewController<T>(popup.ToString());
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


        public static string ToPagePrefabName(Page page)
        {
            return page.ToString().CamelToSnakeCase() + "_page";
        }

        #endregion


        #region Common

        public static async UniTask WaitForHidePushAskAgainAsync(string title, string explain,
            Action okEndCodeAction = null)
        {
            var param = SpawnParam();
            param["title"] = title;
            param["explain"] = explain;
            param["cancel"] = true;
            param["ok"] = true;
            param["active_last_view"] = false;
            var endCode = await WaitForPopPushPopup(Popup.Message, param);

            if (endCode != PopupEndCode.Ok) return;

            okEndCodeAction.CallSafe();
        }


        public static async UniTask WaitForPopSystemPopupAsync(string explain)
        {
            var param = SpawnParam();
            param["title"] = LocalizeHelper.FromUI("SUI000134"); // 알림
            param["explain"] = explain;
            param["cancel"] = false;
            param["ok"] = true;
            await WaitForHidePushSystemAsync(SystemPopup.Alert, param);
        }

        #endregion


        public static bool IsPage(string viewName)
        {
            return Enum.TryParse<Page>(viewName, out _);
        }


        public static bool IsPopup(string viewName)
        {
            return Enum.TryParse<Popup>(viewName, out _);
        }
    }
}