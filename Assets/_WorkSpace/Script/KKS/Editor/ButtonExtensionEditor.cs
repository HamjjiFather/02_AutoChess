﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KKSFramework.UI;
using UnityEditor;
using UnityEditor.UI;

namespace KKSFramework.Editor
{
    /// <summary>
    /// ButtonExtension 클래스 커스텀 에디터.
    /// </summary>
    [CustomEditor (typeof (ButtonExtension), true)]
    [CanEditMultipleObjects]
    public class ButtonExtensionEditor : ButtonEditor
    {
        //[Header("[Editor_UIToggleBase]"), Space(5)]


        #region Fields & Property

        /// <summary>
        /// 필드 인포.
        /// </summary>
        private List<FieldInfo> _listPropertyInfos;

        /// <summary>
        /// 상속 여부.
        /// </summary>
        private bool _isInheritance;

        #endregion


        #region UnityMethods

        protected override void OnEnable ()
        {
            base.OnEnable ();
            _isInheritance = !target.GetType ().Name.Equals (nameof (ButtonExtension));
            if (_isInheritance)
                _listPropertyInfos = target.GetType ().GetFields ().ToList ().FindAll (x =>
                    !x.DeclaringType.Name.Equals (nameof (ButtonExtension)) &&
                    x.DeclaringType.Name.Equals (target.GetType ().Name));
        }

        public override void OnInspectorGUI ()
        {
            if (_isInheritance)
                if (_listPropertyInfos.Count != 0)
                    foreach (var t in _listPropertyInfos)
                    {
                        EditorGUILayout.PropertyField (serializedObject.FindProperty (t.Name));
                        serializedObject.ApplyModifiedProperties ();
                    }

            serializedObject.ApplyModifiedProperties ();

            EditorGUILayout.PropertyField (serializedObject.FindProperty ("buttonText"));
            serializedObject.ApplyModifiedProperties ();

            EditorGUILayout.Space ();
            EditorGUILayout.LabelField ("[Button]", EditorStyles.boldLabel);

            base.OnInspectorGUI ();
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}