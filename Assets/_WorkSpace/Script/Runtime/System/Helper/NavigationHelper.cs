using Cysharp.Threading.Tasks;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;

namespace KKSFramework.Navigation
{
    /// <summary>
    /// Navigation은 게임에 따라 변형 될 수 있으므로 Helper를 두어 접근한다.
    /// </summary>
    public static class NavigationHelper
    {
        #region Methods

        /// <summary>
        /// 페이지 오픈.
        /// </summary>
        public static async UniTask OpenPageAsync (NavigationViewType navigationViewType,
            NavigationTriggerState triggerState = NavigationTriggerState.CloseAndOpen,
            object pushValue = null, UnityAction actionOnFirst = null)
        {
            await NavigationProjectManager.Instance.OpenPage (navigationViewType.ToString (), triggerState, pushValue,
                actionOnFirst);
        }

        /// <summary>
        /// 팝업 오픈.
        /// </summary>
        public static async UniTask OpenPopupAsync (NavigationViewType navigationViewType, object pushValue = null)
        {
            await NavigationProjectManager.Instance.OpenPopup (navigationViewType.ToString (), pushValue);
        }


        /// <summary>
        /// CommonView 오픈.
        /// </summary>
        public static async UniTask OpenCommonViewAsync<T> (string viewName) where T : Component
        {
            await NavigationProjectManager.Instance.OpenCommonView<T> (viewName);
        }


        /// <summary>
        /// 뒷 페이지로 이동.
        /// </summary>
        public static void GoBackPage ()
        {
            NavigationProjectManager.Instance.GoBackPage ().Forget ();
        }

        #endregion
    }
}