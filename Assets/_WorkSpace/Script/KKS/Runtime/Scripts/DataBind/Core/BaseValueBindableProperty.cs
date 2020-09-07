using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public class BaseValueBindableProperty<T, TV> : BindableProperty<T, TV> where T : Object
    {
        #region Fields & Property
        
        [HideInInspector]
        public string propertyName;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods
        
        private PropertyInfo _propertyInfo;

        private PropertyInfo PropertyInfo
        {
            get
            {
                if (_propertyInfo == null)
                {
                    _propertyInfo = targetComponent.GetType ().GetProperties ()
                        .First (x => x.Name.Equals (propertyName));
                }

                return _propertyInfo;
            }
        }

        protected override TV GetDelegate () => (TV) PropertyInfo.GetValue (targetComponent);

        protected override void SetDelegate (TV value) => PropertyInfo.SetValue (targetComponent, value);

        #endregion
    }
}