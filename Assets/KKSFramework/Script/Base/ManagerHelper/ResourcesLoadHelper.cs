using UniRx.Async;

namespace KKSFramework.ResourcesLoad
{
    public enum ResourceRoleType
    {
        _Data,
        _Prefab,
        _Sound,
        _Image,
        _Animation
    }

    public enum ResourcesType
    {
        TSV,
        Json,
        
        Page,
        Popup,
        CommonView,
        Element,

        BGM = 50,
        Button,
        SFX,
        
        Food,
        Monster,
        Item
    }

    public static class ResourcesLoadHelper
    {
        #region GetResources

        public static T GetResources<T>(ResourceRoleType roleType, ResourcesType type, string resourceName)
            where T : UnityEngine.Object
        {
            return ResourcesLoadManager.Instance.GetResources<T>(roleType.ToString(), type.ToString(), resourceName);
        }

        
        public static T GetResources<T>(ResourceRoleType roleType, string resourceName) where T : UnityEngine.Object
        {
            return ResourcesLoadManager.Instance.GetResources<T>(roleType.ToString(), resourceName);
        }


        public static T GetResources<T>(string resourceName) where T : UnityEngine.Object
        {
            return ResourcesLoadManager.Instance.GetResources<T>(resourceName);
        }
        
        
        public static async UniTask<T> GetResourcesAsync<T>(ResourceRoleType roleType, ResourcesType type,
            string resourceName)
            where T : UnityEngine.Object
        {
            return await ResourcesLoadManager.Instance.GetResourcesAsync<T>(roleType.ToString(), type.ToString(),
                resourceName);
        }

        
        public static async UniTask<T> GetResourcesAsync<T>(ResourceRoleType roleType, string resourceName)
            where T : UnityEngine.Object
        {
            return await ResourcesLoadManager.Instance.GetResourcesAsync<T>(roleType.ToString(), resourceName);
        }
        
        #endregion


        #region Instantiate Prefab

        

        #endregion

    }
}