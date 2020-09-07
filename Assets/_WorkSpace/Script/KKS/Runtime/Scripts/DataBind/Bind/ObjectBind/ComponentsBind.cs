﻿using UnityEngine;

namespace KKSFramework.DataBind
{
    public class ComponentsBind : Bindable
    {
        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        [SerializeField]
        protected GameObject[] targetComponents;

        public override object BindTarget => targetComponents;


        public override void Dispose ()
        {
            base.Dispose ();
            targetComponents = null;
        }
    }
}