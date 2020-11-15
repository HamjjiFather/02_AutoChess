using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using KKSFramework.DataBind.Extension;
using ModestTree;

namespace KKSFramework.DataBind
{
    public class Context : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// 컨테이너에 포함된 컴포넌트 딕셔너리.
        /// </summary>
        public Dictionary<string, object> Container { get; } = new Dictionary<string, object> ();

        /// <summary>
        /// 자동 Resolve 실행 여부.
        /// </summary>
        public bool autoRun = true;
        
        /// <summary>
        /// Resolve가 되었는지 여부.
        /// </summary>
        private bool _isResolved;


        #region UnityMethods

        private void Awake ()
        {
            if (autoRun && CheckRunnableState ())
            {
                Resolve ();
            }
        }


        private void OnDestroy ()
        {
            Dispose ();
        }

        #endregion


        private bool CheckRunnableState ()
        {
            var classes = GetComponents<IResolveTarget> ();
            var result = classes.Any ();
            if (!result)
            {
                Debug.LogWarning ($"[{nameof (Context)}] There is no 'IResolveTarget' component in this game object.");
            }

            return classes.Any ();
        }


        /// <summary>
        /// 바인딩된 컴포넌트를 할당한다. 
        /// </summary>
        public void Resolve (bool isForce = false)
        {
            if (CheckRunnableState () && !_isResolved || isForce)
            {
                var bindingComps = GetComponentsInChildren<Bindable> (true)
                    .Where (x => x.TargetContext.Equals (this));

                var binderClasses = GetComponents<IResolveTarget> ();
                binderClasses.ForEach (binder =>
                {
                    var binderType = binder.GetType ();
                    var fields = new List<FieldInfo> ();
                    
                    while (!binderType.Name.Equals (nameof(MonoBehaviour)))
                    {
                        var result = binderType
                            .GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where (x => x.HasAttribute<ResolverAttribute> ());
                        fields.AddRange (result);
                        binderType = binderType.BaseType;
                    }
                    
#if BF_DEBUG
                    var thisObj = gameObject;
                    Debug.Log (
                        $"The result quantity of binding requests in the {thisObj.name} GameObject is {fields.Count ()}",
                        thisObj);
#endif

                    fields.ForEach (field =>
                    {
                        var attribute = field.GetCustomAttribute<ResolverAttribute> (true);
                        var attributeKey = string.IsNullOrEmpty (attribute.Key) ? field.Name : attribute.Key;
#if BF_DEBUG
                        Debug.Log ($"resolve request {attributeKey} in {thisObj.name} GameObject", thisObj);
#endif

                        var targetComp = Container.ContainsKey (attributeKey)
                            ? Container[attributeKey]
                            : bindingComps.Single (x => x.ContainerPath.Equals (attributeKey)).BindTarget;

                        // target component is GameObject Array type.
                        if (targetComp is GameObject[] gameObjects)
                        {
                            var fieldType = field.FieldType;
                            if (!fieldType.HasElementType)
                            {
#if BF_DEBUG
                                Debug.LogError (
                                    $"resolve target field type has not array type {fieldType} in {thisObj.name} GameObject",
                                    thisObj);
#endif
                                return;
                            }

                            var elementType = fieldType.GetElementType ();
                            if (elementType == typeof (GameObject))
                            {
                                AddComponent (attributeKey, gameObjects);
                                field.SetValue (binder, gameObjects);
                                return;
                            }

                            var components = gameObjects.Select (x => x.GetComponent (elementType))
                                .ToArray ();
                            var elementArray = Array.CreateInstance (elementType, gameObjects.Length);
                            components.ForEach ((c, i) => { elementArray.SetValue (c, i); });
                            AddComponent (attributeKey, gameObjects);
                            field.SetValue (binder, elementArray);
                            return;
                        }

                        AddComponent (attributeKey, targetComp);
                        field.SetValue (binder, targetComp);
                    });
                });
            }

            _isResolved = true;
        }


        /// <summary>
        /// Manually Resolve.
        /// </summary>
        public object Resolve (string key)
        {
            if (Vaildate (key))
            {
                return Container[key];
            }

            Debug.Log ($"There is no bindable source: {key}.");
            return default;
        }


        /// <summary>
        /// Manually Resolve with Convert to Generic type.
        /// </summary>
        public T Resolve<T> (string key)
        {
            if (Vaildate (key))
            {
                return (T) Container[key];
            }

            Debug.Log ($"There is no bindable source: {key}.");
            return default;
        }


        /// <summary>
        /// Add Component.
        /// </summary>
        public void AddComponent (string key, object target)
        {
            if (Container.ContainsKey (key))
                return;

            Container.Add (key, target);
        }


        /// <summary>
        /// Has contained key.
        /// </summary>
        public bool Vaildate (string key)
        {
            return Container.ContainsKey (key);
        }


        public void Dispose ()
        {
            Container.Clear ();
            GC.Collect ();
        }

        public void ChangeAutoRunState (bool value)
        {
            autoRun = value;
        }
    }
}