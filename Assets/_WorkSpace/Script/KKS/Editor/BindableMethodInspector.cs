using System.Linq;
using System.Reflection;
using KKSFramework.DataBind;
using UnityEditor;

namespace KKSFramework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor (typeof (MethodBind), true)]
    public class BindableMethodInspector : UnityEditor.Editor
    {
        #region Fields & Property

        private MethodBind _target;

        private int _arrayIndex;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void OnEnable ()
        {
            _target = target as MethodBind;
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

            var methods = _target.targetComponent.GetType ().GetMethods (BindingFlags.Instance | BindingFlags.Public)
                .Where (x => x.GetCustomAttribute<BindAttribute>() != null).ToList ();
            var methodNames = methods.Select (x => x.Name).ToList ();

            _arrayIndex = methodNames.Contains (_target.methodName) ? methodNames.IndexOf (_target.methodName) : 0;
            _arrayIndex = EditorGUILayout.Popup ("Methods", _arrayIndex, methodNames.ToArray ());
            _target.methodName = methodNames[_arrayIndex];
        }

        #endregion
    }
}