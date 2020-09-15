using System.Linq;
using System.Reflection;
using KKSFramework.DataBind;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor (typeof (MethodsBind), true)]
    public class BindableMethodsInspector : UnityEditor.Editor
    {
        #region Fields & Property

        private MethodsBind _target;
        
        private Component _targetComp;

        private int _typeArrayIndex;

        private int _arrayIndex;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void OnEnable ()
        {
            _target = target as MethodsBind;
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
            
            var methods = _target.targetComponent.GetType ().GetMethods (BindingFlags.Instance | BindingFlags.Public)
                .Where (x => x.GetCustomAttribute<BindAttribute>() != null).ToList ();

            if (!methods.Any ())
            {
                #if BF_DEBUG
                Debug.Log ("Component has not contain BindAttribute methods");
                return;
                #endif
            }

            var methodNames = methods.Select (x => x.Name).ToList ();

            _arrayIndex = methodNames.Contains (_target.methodName) ? methodNames.IndexOf (_target.methodName) : 0;
            _arrayIndex = EditorGUILayout.Popup ("Methods", _arrayIndex, methodNames.ToArray ());
            _target.methodName = methodNames[_arrayIndex];
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}