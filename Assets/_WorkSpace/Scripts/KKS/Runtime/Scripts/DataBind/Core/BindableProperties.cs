using System.Collections;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public abstract class BindableProperties<T, TV> : Bindable where T : Object where TV : IEnumerable
    {
        #region Fields & Property

        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        public T[] targetComponents;

        public override object BindTarget => BindTargetProperty;

        /// <summary>
        /// my property.
        /// </summary>
        private Property<TV> _targetValue;

        public Property<TV> BindTargetProperty
        {
            get
            {
                if (_targetValue is null)
                    _targetValue = new Property<TV> (GetDelegate, SetDelegate);

                return _targetValue;
            }
        }

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected abstract TV GetDelegate ();

        protected abstract void SetDelegate (TV values);

        #endregion


        #region EventMethods

        #endregion
    }
}