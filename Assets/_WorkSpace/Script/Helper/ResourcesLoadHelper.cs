using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using KKSFramework.ResourcesLoad;
using UnityEngine;
#if BF_ADDRESSABLE
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
#endif


namespace Helper
{
    public static class ResourcesLoadHelper
    {
        #region Resources
        
        public static T LoadResource<T> (ResourceRoleType roleType, ResourcesType type, string resourceName)
            where T : Object
        {
            return LoadResource<T> (roleType.ToString (), type.ToString (), resourceName);
        }

        public static T LoadResource<T> (ResourceRoleType roleType, string resourceName) where T : Object
        {
            return LoadResource<T> (roleType.ToString(), resourceName);
        }

        
        /// <summary>
        /// 제네릭 타입에 해당하는 리소스 로드.
        /// </summary>
        private static T LoadResource<T> (string typeString, string prefabName) where T : Object
        {
            return LoadResource<T> ($"{typeString}/{prefabName}");
        }
        
        
        /// <summary>
        /// 제네릭 타입에 해당하는 리소스 로드.
        /// </summary>
        private static T LoadResource<T> (string roleString, string typeString, string prefabName) where T : Object
        {
            return LoadResource<T> ($"{roleString}/{typeString}/{prefabName}");
        }
        

        public static T LoadResource<T> (string path) where T : Object
        {
            return Resources.Load<T> (path);
        }
        
        
        private static T[] LoadResourcesAll<T> (string path) where T : Object
        {
            return Resources.LoadAll<T> (path);
        }
        

        public static GameObject InstantiatePrefab (string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject> (path);
            if (prefab != null)
            {
                var inst = Object.Instantiate (prefab, parent);
                return inst;
            }

            return null;
        }
        
        
        public static GameObject LoadPrefab (string path)
        {
            return Resources.Load<GameObject> (path);
        }


        
        
        public static Task<T> LoadResourcesAsync<T> (ResourceRoleType roleType, ResourcesType type, string resourceName)
            where T : Object
        {
            return LoadResourcesAsync<T> (roleType.ToString (), type.ToString (), resourceName);
        }

        public static Task<T> LoadResourcesAsync<T> (ResourceRoleType roleType, string resourceName) where T : Object
        {
            return LoadResourcesAsync<T> (string.Empty, roleType.ToString (), resourceName);
        }
        
        /// <summary>
        /// 제네릭 타입에 해당하는 리소스 비동기 로드.
        /// </summary>
        private static Task<T> LoadResourcesAsync<T> (string roleString, string typeString, string prefabName) where T : Object
        {
            return LoadResourcesAsync<T> ($"{roleString}/{typeString}/{prefabName}");
        }
        

        public static async Task<T> LoadResourcesAsync<T> (string resourcesPath) where T : Object
        {
            var request = Resources.LoadAsync<T> (resourcesPath.Replace ("\r", string.Empty));

            while (!request.isDone)
            {
                await request;
            }

            return request.asset as T;
        }


        #region Unload
        
        public static void UnloadAsset<T> (T resources) where T : Object
        {
            if (resources != null)
            {
                Resources.UnloadAsset (resources);
            }
        }
        
        #endregion


        #endregion


#if BF_ADDRESSABLE
        #region AssetBundle

        public const string RootPath = "Assets/Bundles";


        public static AsyncOperationHandle<IResourceLocator> InitializeAddressablesAsync ()
        {
            return Addressables.InitializeAsync ();
        }


        public static AsyncOperationHandle<long> GetDownloadSizeAsync (AssetBundleLabel label)
        {
            return Addressables.GetDownloadSizeAsync (label.ToString ());
        }


        public static AsyncOperationHandle DownloadDependenciesAsync (AssetBundleLabel label)
        {
            return Addressables.DownloadDependenciesAsync (label.ToString ());
        }


        public static string GetImageAssetPath (ResourcesRole resourcesRole, string spriteName)
        {
            var key = $"{resourcesRole.ToString ()}/{spriteName}.png";
            return key;
        }


        public static void LoadSpriteAsync (ResourcesRole resourcesRole, string spriteName, Action<Sprite> complete)
        {
            LoadAssetAsync (GetImageAssetPath (resourcesRole, spriteName), complete);
        }


        public static void LoadSpriteAsync (string spritePath, Action<Sprite> complete)
        {
            LoadAssetAsync (spritePath, complete);
        }


        public static void LoadSpriteAsync (string spritePath, Image destImage)
        {
            ReleaseSprite (destImage);

            destImage.enabled = false;
            LoadSpriteAsync (spritePath, res =>
            {
                if (destImage.Obj () == null)
                {
                    return;
                }

                destImage.enabled = true;
                destImage.sprite = res;
            });
        }


        private static void LoadSpriteSimplifyNameAsync (ResourcesRole resourcesRole, string spriteName,
            Image destImage, bool isNativeSize = false)
        {
            if (destImage != null)
            {
                ReleaseSprite (destImage);
                destImage.enabled = false;
            }

            LoadAssetAsync<Sprite> ($"{resourcesRole}/{spriteName}.png", res =>
            {
                if (destImage == null) return;
                destImage.enabled = true;
                destImage.sprite = res;
                if (isNativeSize) destImage.SetNativeSize ();
            });
        }


        private static void LoadSpriteAsync (ResourcesRole resourcesRole, string spriteName, Image destImage,
            bool isNativeSize = false)
        {
            if (destImage != null)
            {
                ReleaseSprite (destImage);
                destImage.enabled = false;
            }

            LoadSpriteAsync (resourcesRole, spriteName, res =>
            {
                if (destImage == null) return;
                destImage.enabled = true;
                destImage.sprite = res;
                if (isNativeSize) destImage.SetNativeSize ();
            });
        }


        public static AsyncOperationHandle<T> LoadAssetAsync<T> (string key) where T : Object
        {
            return Addressables.LoadAssetAsync<T> ($"{RootPath}/{key}");
        }


        public static AsyncOperationHandle<T> LoadAssetAsyncBySimplifyName<T> (string entryName) where T : Object
        {
            return Addressables.LoadAssetAsync<T> (entryName);
        }


        private static void LoadAssetAsync<T> (string key, Action<T> complete) where T : Object
        {
            var opHandle = Addressables.LoadAssetAsync<T> (key);
            opHandle.Completed += LoadComplete;

            void LoadComplete (AsyncOperationHandle<T> result)
            {
                opHandle.Completed -= LoadComplete;
                complete.Invoke (result.Result);
            }
        }


        public static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T> (IList<object> keys) where T : Object
        {
            return Addressables.LoadAssetsAsync<T> (keys, null, Addressables.MergeMode.Union);
        }


        public static AsyncOperationHandle<T> LoadAssetAsync<T> (object keys) where T : Object
        {
            return Addressables.LoadAssetAsync<T> (keys);
        }


        private static void LoadAssetsAsync<T> (IList<string> keys, Action<IList<T>> complete) where T : Object
        {
            var opHandle = Addressables.LoadAssetAsync<IList<T>> (keys);
            opHandle.Completed += handle => complete.Invoke (handle.Result);
        }


        public static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T> (AssetBundleLabel label) where T : Object
        {
            return Addressables.LoadAssetsAsync<T> (label.ToString (), null);
        }


        public static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T> (string key, Action<T> callback = null)
            where T : Object
        {
            return Addressables.LoadAssetsAsync<T> (key, callback);
        }


        public static void Release (string key)
        {
            Addressables.Release (key);
        }


        public static void Release<T> (AsyncOperationHandle<T> handle)
        {
            Addressables.Release (handle);
        }


        public static AsyncOperationHandle<GameObject> InstantiateAsync (string path, Transform parent)
        {
            return Addressables.InstantiateAsync ($"{RootPath}/{path}", parent);
        }


        public static AsyncOperationHandle<GameObject> InstantiateAsyncSimplifyName (string entryName, Transform parent)
        {
            return Addressables.InstantiateAsync (entryName, parent);
        }


        private static void InstantiateAsync<T> (string path, Transform parent, Action<T> complete,
            bool defaultActive = true) where T : Object
        {
            var opHandle = Addressables.InstantiateAsync (path, parent);
            opHandle.Completed += LoadComplete;

            void LoadComplete (AsyncOperationHandle<GameObject> result)
            {
                opHandle.Completed -= LoadComplete;
                result.Result.SetActiveSafe (defaultActive);
                complete.Invoke (result.Result.GetComponent<T> ());
            }
        }


        public static void ReleaseInstance (GameObject asset)
        {
            Addressables.ReleaseInstance (asset);
        }

        #endregion


        #region Async

        public static void LoadFishIconAsync (string spriteName, Image dest)
        {
            LoadSpriteAsync (ResourcesRole.IconFish, spriteName, dest);
        }


        public static void LoadGearIconAsync (string spriteName, Image dest)
        {
            LoadSpriteSimplifyNameAsync (ResourcesRole.IconGear, spriteName, dest);
        }


        public static void LoadShopIconAsync (string spriteName, Image dest)
        {
            LoadSpriteAsync (ResourcesRole.IconShop, spriteName, dest);
        }


        public static void LoadCurrencyIconAsync (string spriteName, Image dest)
        {
            LoadSpriteAsync (ResourcesRole.IconCurrency, spriteName, dest);
        }


        public static void LoadCellBackgroundAsync (string spriteName, Image dest)
        {
            LoadSpriteAsync (ResourcesRole.CellBackground, spriteName, dest);
        }


        public static void ReleaseSprite (Image image)
        {
            if (image.sprite == null) return;

            Addressables.Release (image.sprite);
            image.sprite = null;
        }

        #endregion


        public static AsyncOperationHandle<GameObject> InstantiatePrefabBySimplifyNameAsync (ResourcesRole role,
            string name, Transform parent)
        {
            var key = $"{role}/{name}.prefab";
            return Addressables.InstantiateAsync (key, parent);
        }


        #region Scene

        public static AsyncOperationHandle<SceneInstance> LoadSceneAsync (string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            var key = $"Map/{sceneName}.unity";
            return Addressables.LoadSceneAsync (key, mode);
        }


        public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync (SceneInstance scene)
        {
            return Addressables.UnloadSceneAsync (scene);
        }
        
        #endregion
#endif
    }
}