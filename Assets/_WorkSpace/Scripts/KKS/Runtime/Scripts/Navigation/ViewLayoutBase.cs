using BaseFrame;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public class ViewLayoutBase : MonoBehaviour
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        /// <summary>
        /// 초기화 여부.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// 이 ViewLayout을 관리하는 Loader.
        /// </summary>
        public ViewLayoutLoaderBase ViewLayoutLoader { get; private set; }

        #endregion


        #region Methods

        public virtual void Initialize (ViewLayoutLoaderBase loader)
        {
            Initialized = true;
            gameObject.SetActive (false);
            ViewLayoutLoader = loader;
            OnInitialized ();
        }


        public async UniTask ActiveLayout (Parameters parameters = null)
        {
            if (!Initialized)
                return;
            
            gameObject.SetActive (true);
            await OnActiveAsync (parameters);
        }


        public async UniTask DisableLayout ()
        {
            if (!Initialized)
                return;
            
            gameObject.SetActive (false);
            await OnDisableAsync ();
        }


        protected virtual void OnInitialized ()
        {
        }


        protected virtual UniTask OnActiveAsync (Parameters parameters)
        {
            return UniTask.CompletedTask;
        }


        protected virtual UniTask OnDisableAsync ()
        {
            return UniTask.CompletedTask;
        }


        #endregion


        #region EventMethods

        #endregion
    }
}