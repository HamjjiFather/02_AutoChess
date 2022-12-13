using UnityEngine;

#if BF_ZENJECT
using Zenject;
#endif

namespace Helper
{
    /// <summary>
    /// 오브젝트 풀링 헬퍼.
    /// </summary>
    public static class ObjectPoolingHelper
    {
        private const string SpawnPath = "Bundles";


        #region Methods

        public static void Despawn (Transform poolObject)
        {
            Debug.Log ($"Despawn Request SpawnPath:{SpawnPath}, Transform:{poolObject.name}");
            // PoolManager.Pools[SpawnPath].Despawn (poolObject.transform);
        }

        /// <summary>
        /// 풀링된 오브젝트가 있으면 풀링해제, 없으면 오브젝트를 새로 생성함.
        /// </summary>
        public static T Spawn<T> (string roleType, string type, string resourceName,
            Transform parents, bool useInject = true) where T : MonoBehaviour
        {
            return default;
            // if (!PoolManager.Pools.ContainsKey (SpawnPath))
            // {
            //     PoolManager.Pools.Create (SpawnPath);
            // }
            //
            // if (!PoolManager.Pools[SpawnPath].prefabPools.ContainsKey (resourceName))
            // {
            //     PoolManager.Pools[SpawnPath].CreatePrefabPool (new PrefabPool (res.transform));
            // }
            //
            // var obj = PoolManager.Pools[SpawnPath].Spawn (resourceName).GetComponent<T> ();
//             obj.gameObject.name = resourceName;
//             var t = obj.transform;
//             t.SetParent (parents);
//             t.localPosition = Vector3.zero;
//             t.localRotation = Quaternion.identity;
//             t.localScale = Vector3.one;
//
// #if BF_ZENJECT
//             if (useInject)
//                 ProjectContext.Instance.Container.InjectGameObject (obj.gameObject);
// #endif
//
//             return obj;
        }

        #endregion
    }
}