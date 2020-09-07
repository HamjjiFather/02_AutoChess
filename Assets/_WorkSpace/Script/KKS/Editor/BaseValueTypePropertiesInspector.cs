using System.Collections;
using System.Linq;
using KKSFramework.DataBind;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor (typeof (BooleanPropertiesBind))]
    public class BooleanPropertiesInspector : BaseValueTypePropertiesInspector<BooleanPropertiesBind, bool[]>
    {
    }

    [CanEditMultipleObjects]
    [CustomEditor (typeof (IntPropertiesBind))]
    public class IntPropertiesInspector : BaseValueTypePropertiesInspector<IntPropertiesBind, int[]>
    {
    }

    [CanEditMultipleObjects]
    [CustomEditor (typeof (FloatPropertiesBind))]
    public class FloatPropertiesInspector : BaseValueTypePropertiesInspector<FloatPropertiesBind, float[]>
    {
    }

    [CanEditMultipleObjects]
    [CustomEditor (typeof (StringPropertiesBind))]
    public class StringPropertiesInspector : BaseValueTypePropertiesInspector<StringPropertiesBind, string[]>
    {
    }


    public class BaseValueTypePropertiesInspector<T, TV> : UnityEditor.Editor
        where T : BaseValueBindableProperties<Component, TV> where TV : IEnumerable
    {
        #region Fields & Property

        private T _target;

        private Component _targetComp;

        private int _typeArrayIndex;

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

            if (_target.targetComponents == null || !_target.targetComponents.Any ())
                return;

            var comps = _target.targetComponents.First ().GetComponents<Component> ().ToList ();
            _typeArrayIndex = _target.targetComponent == null || !comps.Contains (_target.targetComponent)
                ? 0
                : comps.IndexOf (_target.targetComponent);
            _typeArrayIndex = EditorGUILayout.Popup ("Components", _typeArrayIndex,
                comps.Select (x => x.GetType ().Name).ToArray ());

            if (_targetComp != comps[_typeArrayIndex])
            {
                _targetComp = comps[_typeArrayIndex];
                _target.targetComponent = comps[_typeArrayIndex];
                _target.targetComponents = _target.targetComponents
                    .Select (x => x.GetComponent (_targetComp.GetType ())).ToArray ();
            }

            _target.targetComponent = comps[_typeArrayIndex];

            var elementType = typeof (TV).GetElementType ();
            var properties = _target.targetComponent.GetType ().GetProperties ()
                .Where (x => x.PropertyType == elementType && x.SetMethod != null).ToList ();
            var propertyNames = properties.Select (x => x.Name).ToList ();

            if (!propertyNames.Any ())
            {
                Debug.Log ($"There is no properties that are {elementType} type");
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