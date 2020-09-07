using UnityEngine;

namespace KKSFramework.DataBind
{
    public abstract class BindableProperty<T, TV> : Bindable where T : Object
    {
        #region Fields & Property
        
        /// <summary>
        /// BindTarget component.
        /// </summary>
        public override object BindTarget => BindTargetProperty;
        
        /// <summary>
        /// target comp.
        /// </summary>
        public T targetComponent;

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

        /// <summary>
        /// getter delegates.
        /// </summary>
        protected abstract TV GetDelegate ();

        /// <summary>
        /// setter delegates.
        /// </summary>
        protected abstract void SetDelegate (TV value);


#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void Dispose ()
        {
            _targetValue = null;
            targetComponent = null;
            base.Dispose ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}