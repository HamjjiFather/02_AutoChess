using BaseFrame;
using Helper;
using KKSFramework.ResourcesLoad;
using PathologicalGames;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public static class ObjectPoolingHelper
{
    #region Methods

    public static void Create (string poolName, GameObject poolObject)
    {
        PoolManager.Pools.Create (poolName, poolObject);
    }

    /// <summary>
    /// 풀링된 오브젝트가 있으면 풀링해제, 없으면 오브젝트를 새로 생성함. 
    /// </summary>
    public static T GetResources<T> (ResourceRoleType roleType, ResourcesType type, string resourceName,
        Transform parents) where T : PooingComponent
    {
        T obj;
        var poolingPath = $"{roleType}/{type}/{resourceName}";
        if (PoolManager.Pools.ContainsKey (poolingPath))
        {
            obj = PoolManager.Pools[poolingPath].Spawn (poolingPath).GetComponent<T> ();
            obj.transform.SetParentLocalReset (parents);
            return obj;
        }

        var res = ResourcesLoadHelper.LoadResource<T> (poolingPath);
        obj = res.InstantiateObject<T> ();
        obj.transform.SetParentLocalReset (parents);
        obj.Created<PooingComponent> (poolingPath);
        return obj;
    }

    #endregion
}