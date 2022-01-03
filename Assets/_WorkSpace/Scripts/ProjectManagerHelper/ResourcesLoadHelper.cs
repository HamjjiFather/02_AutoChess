using Cysharp.Threading.Tasks;
using ResourcesLoad;

namespace KKSFramework.ResourcesLoad
{
    public static class ResourcesLoadHelper
    {
        public static T GetResources<T> (ResourceRoleType roleType, ResourcesType type, string resourceName)
            where T : UnityEngine.Object
        {
            return ResourcesLoadProjectManager.Instance.GetResources<T> (roleType.ToString (), type.ToString (), resourceName);
        }

        public static T GetResources<T> (ResourceRoleType roleType, string resourceName) where T : UnityEngine.Object
        {
            return ResourcesLoadProjectManager.Instance.GetResources<T> (roleType.ToString (), resourceName);
        }


        public static T GetResources<T> (string resourceName) where T : UnityEngine.Object
        {
            return ResourcesLoadProjectManager.Instance.GetResources<T> (resourceName);
        }


        public static async UniTask<T> GetResourcesAsync<T> (ResourceRoleType roleType, ResourcesType type,
            string resourceName)
            where T : UnityEngine.Object
        {
            return await ResourcesLoadProjectManager.Instance.GetResourcesAsync<T> (roleType.ToString (), type.ToString (),
                resourceName);
        }

        public static async UniTask<T> GetResourcesAsync<T> (ResourceRoleType roleType, string resourceName)
            where T : UnityEngine.Object
        {
            return await ResourcesLoadProjectManager.Instance.GetResourcesAsync<T> (roleType.ToString (), resourceName);
        }
    }
}