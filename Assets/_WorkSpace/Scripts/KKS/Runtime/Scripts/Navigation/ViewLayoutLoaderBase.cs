using System;
using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.Events;

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
        
        public int nowLayout;

#pragma warning disable CS0649

        [Resolver]
        private ViewLayoutBase[] _viewLayoutObjs;

        public ViewLayoutBase[] ViewLayoutBases => _viewLayoutObjs;

#pragma warning restore CS0649

        private Action<int> _onChangeSubviewAction;

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
            _viewLayoutObjs.ForEach (x => { x.Initialize (this); });
        }


        public virtual void SetSubView (int index, Parameters parameters = null)
        {
            if (nowLayout >= 0 && nowLayout < _viewLayoutObjs.Length)
                _viewLayoutObjs[nowLayout].DisableLayout ().Forget ();

            nowLayout = index;
            _viewLayoutObjs[nowLayout].ActiveLayout (parameters).Forget ();
            _onChangeSubviewAction.CallSafe (nowLayout);
        }


        public virtual void CloseViewLayout ()
        {
            _viewLayoutObjs.ForEach (vlo => vlo.DisableLayout ().Forget ());
            nowLayout = -1;
            _onChangeSubviewAction.CallSafe (nowLayout);
        }

        
        public void SetChangeAction (Action<int> changeAction)
        {
            _onChangeSubviewAction = changeAction;
        }

        #endregion
    }
}