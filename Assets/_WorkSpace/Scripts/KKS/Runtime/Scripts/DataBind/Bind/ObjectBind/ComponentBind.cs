using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace KKSFramework.DataBind
{
    public class ComponentBind : Bindable, IDisposable
    {
        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        [SerializeField]
        protected Object targetComponent;

        public override object BindTarget => targetComponent;

        
        public override void Dispose ()
        {
            base.Dispose ();
            targetComponent = null;
        }
    }
}