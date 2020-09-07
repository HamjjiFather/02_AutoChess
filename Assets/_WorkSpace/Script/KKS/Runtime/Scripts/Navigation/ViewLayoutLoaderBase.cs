using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    /// <summary>
    /// 페이지에서 규칙적으로 호출되는 ViewLayout들을 호출하기 위한 컴포넌트.
    /// </summary>
    [RequireComponent (typeof (Context))]
    public class ViewLayoutLoaderBase : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

        public bool initOnAwake = true;

#pragma warning disable CS0649

        [Resolver]
        private ViewLayoutBase[] _viewLayoutObjs;

        public ViewLayoutBase[] ViewLayoutBases => _viewLayoutObjs;

#pragma warning restore CS0649

        private int _nowLayout;

        #endregion


        #region UnityMethods

        protected virtual void Awake ()
        {
            if (!initOnAwake) return;
            Initialize ();
        }

        #endregion


        #region Methods

        public virtual void Initialize ()
        {
            _viewLayoutObjs.ForEach (x => { x.Initialize (); });
        }


        public virtual void SetSubView (int index)
        {
            if (_nowLayout >= 0 && _nowLayout < _viewLayoutObjs.Length)
                _viewLayoutObjs[_nowLayout].DisableLayout ().Forget ();

            _nowLayout = index;
            _viewLayoutObjs[_nowLayout].ActiveLayout ().Forget ();
        }


        public virtual void CloseViewLayout ()
        {
            _viewLayoutObjs.ForEach (vlo => vlo.DisableLayout ().Forget ());
            _nowLayout = -1;
        }

        #endregion
    }
}