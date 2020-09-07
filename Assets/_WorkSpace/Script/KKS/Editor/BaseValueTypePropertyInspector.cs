using System.Linq;
using KKSFramework.DataBind;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor (typeof (FloatPropertyBind))]
    public class FloatPropertyInspector : BaseValueTypePropertyInspector<FloatPropertyBind, float>
    {
    }
    
    [CanEditMultipleObjects]
    [CustomEditor (typeof (IntPropertyBind))]
    public class IntPropertyInspector : BaseValueTypePropertyInspector<IntPropertyBind, int>
    {
    }
    
    [CanEditMultipleObjects]
    [CustomEditor (typeof (BooleanPropertyBind))]
    public class BooleanPropertyInspector : BaseValueTypePropertyInspector<BooleanPropertyBind, bool>
    {
    }
    
    [CanEditMultipleObjects]
    [CustomEditor (typeof (StringPropertyBind))]
    public class StringPropertyInspector : BaseValueTypePropertyInspector<StringPropertyBind, string>
    {
    }
    
    
    public class BaseValueTypePropertyInspector<T, TV> : UnityEditor.Editor where T : BaseValueBindableProperty<Object, TV>
    {
        #region Fields & Property

        private T _target;

        private int _arrayIndex;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void OnEnable ()
        {
            _target = target as T;
        }


        private void OnDisable ()
        {
            _target = null;
        }


        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();

            if (_target.targetComponent == null)
                return;
            
            var properties = _target.targetComponent.GetType ().GetProperties ()
                .Where (x => x.PropertyType == typeof (TV) && x.SetMethod != null).ToList ();
            var propertyNames = properties.Select (x => x.Name).ToList ();

            if (!propertyNames.Any ())
            {
                Debug.Log ($"There is no properties that are {typeof(TV)} type");
                return;
            }

            _arrayIndex = propertyNames.Contains (_target.propertyName)
                ? propertyNames.IndexOf (_target.propertyName)
                : 0;
            _arrayIndex = EditorGUILayout.Popup ("Properties", _arrayIndex, propertyNames.ToArray ());
            _target.propertyName = propertyNames[_arrayIndex];
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}