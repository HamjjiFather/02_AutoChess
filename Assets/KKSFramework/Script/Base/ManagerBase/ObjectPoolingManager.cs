using System;
using System.Collections.Generic;
using KKSFramework.Management;
using KKSFramework.ResourcesLoad;
using UnityEngine;

namespace KKSFramework.Object
{
    /// <summary>
    /// 풀링할 오브젝트 타입.
    /// 게임에 맞게 선언해주어야 함.
    /// </summary>
    public enum PoolingObjectType
    {
        View,
        Prefab
    }

    /// <summary>
    /// 오브젝트 풀링 관리 클래스.
    /// 선입선출 타입의 페이지뷰 풀링.
    /// 2018.06.03. 작성.
    /// </summary>
    public class ObjectPoolingManager : ManagerBase<ObjectPoolingManager>
    {
        #region Constructor

        #endregion


        #region Fields & Property

#pragma warning disable CS0649

        /// <summary>
        /// 오브젝트 풀링 컴포넌트.
        /// </summary>
        private ObjectPoolingComponent _objectPoolingComponent => ComponentBase as ObjectPoolingComponent;

#pragma warning restore CS0649

        /// <summary>
        /// 풀링된 오브젝트 보관 트랜스폼.
        /// </summary>
        private readonly Dictionary<PoolingObjectType, Transform> _pooledObjTransformDict =
            new Dictionary<PoolingObjectType, Transform> ();

        /// <summary>
        /// 풀링된 페이지 뷰 오브젝트 리스트.
        /// enum 타입별로 관리하고 싶으면 변수 타입에 해당하는 딕셔너리를 늘려서 관리하도록.
        /// </summary>
        private readonly Dictionary<string, PooledObjectComponent> _pooledPageViewDict =
            new Dictionary<string, PooledObjectComponent> ();

        /// <summary>
        /// 풀링된 일반 프리팹 오브젝트 리스트.
        /// enum 타입별로 관리하고 싶으면 변수 타입에 해당하는 딕셔너리를 늘려서 관리하도록.
        /// </summary>
        private readonly Dictionary<string, Queue<PooledObjectComponent>> _pooledPrefabDict =
            new Dictionary<string, Queue<PooledObjectComponent>> ();

        #endregion


        #region Methods

        /// <summary>
        /// 해당 타입의 풀링된 오브젝트를 리턴.
        /// </summary>
        private PooledObjectComponent ReturnPooledObjectBase (PoolingObjectType pEPoolingObjectType,
            string objectName)
        {
            switch (pEPoolingObjectType)
            {
                case PoolingObjectType.View:
                {
                    if (IsExistPooledObject (pEPoolingObjectType, objectName))
                    {
                        var obj = _pooledPageViewDict[objectName];
                        _pooledPageViewDict.Remove (objectName);
                        return obj;
                    }

                    return null;
                }

                case PoolingObjectType.Prefab:
                {
                    if (IsExistPooledObject (pEPoolingObjectType, objectName))
                        return _pooledPrefabDict[objectName].Dequeue ();

                    return null;
                }

                default:
                    return null;
            }
        }


        /// <summary>
        /// 해당 타입의 해당 문자열을 가진 풀링된 오브젝트가 있는지 여부.
        /// </summary>
        /// <returns></returns>
        public bool IsExistPooledObject (PoolingObjectType poolingObjectType, string objectName)
        {
            switch (poolingObjectType)
            {
                case PoolingObjectType.View:
                {
                    return _pooledPageViewDict.ContainsKey (objectName) &&
                           _pooledPageViewDict[objectName] != null;
                }

                case PoolingObjectType.Prefab:
                {
                    return _pooledPrefabDict.ContainsKey (objectName) &&
                           _pooledPrefabDict[objectName].Count != 0;
                }

                default:
                    Debug.Log ($"Wrong Status_Pooling Value : {default (PoolingObjectType)}");
                    return false;
            }
        }

        /// <summary>
        /// 풀링된 오브젝트를 꺼냄.
        /// 리소스 관리를 껐다 켰다 할 수 있도록 설계해야 함.
        /// </summary>
        public T ReturnLoadResources<T> (PoolingObjectType poolingObjectType, string objectName,
            Transform parents = null) where T : PooledObjectComponent
        {
            if (!IsExistPooledObject (poolingObjectType, objectName))
            {
                Debug.LogException (new NullReferenceException ("null ref exception pooled object"));
                return null;
            }

            var pooledObject = ReturnPooledObjectBase (poolingObjectType, objectName);

            // 리소스 오브젝트가 없음.
            if (pooledObject == null)
            {
                Debug.LogException (new NullReferenceException ("null ref exception pooled object"));
                return null;
            }

            // 리소스 오브젝트가 있음.
            if (parents)
            {
                pooledObject.transform.SetParent (parents);
            }

            pooledObject.Unpooled ();

            return pooledObject.GetComponent<T> ();
        }


        /// <summary>
        /// 오브젝트를 파괴하지 않고 풀링함.
        /// 풀링된 오브젝트는 하위에 위치 시킴.
        /// </summary>
        public void RegistPooledObject (PooledObjectComponent pooledObjectComponent)
        {
            var poolingInfo = pooledObjectComponent.PoolingInfo;
            var prefixName = poolingInfo.PrefixName;

            switch (pooledObjectComponent.PoolingInfo.PoolingObjectType)
            {
                case PoolingObjectType.View:
                {
                    if (_pooledPageViewDict.ContainsKey (prefixName) == false)
                        _pooledPageViewDict.Add (prefixName, pooledObjectComponent);

                    break;
                }

                case PoolingObjectType.Prefab:
                {
                    if (_pooledPrefabDict.ContainsKey (prefixName) == false)
                        _pooledPrefabDict.Add (prefixName, new Queue<PooledObjectComponent> ());

                    _pooledPrefabDict[prefixName].Enqueue (pooledObjectComponent);
                    break;
                }

                default:
                    break;
            }

            if (_pooledObjTransformDict.ContainsKey (poolingInfo.PoolingObjectType) == false)
            {
                var gameObj = new GameObject ();
                var objTransform = gameObj.transform;
                objTransform.name = poolingInfo.PoolingObjectType.ToString ();
                objTransform.SetParent (_objectPoolingComponent.transform);
                objTransform.SetInstantiateTransform ();

                _pooledObjTransformDict.Add (poolingInfo.PoolingObjectType, objTransform);
            }

            pooledObjectComponent.transform.SetParent (_pooledObjTransformDict[poolingInfo.PoolingObjectType]);
            pooledObjectComponent.gameObject.SetActive (false);
        }

        #endregion
    }
}