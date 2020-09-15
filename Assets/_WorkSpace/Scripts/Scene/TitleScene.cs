using System.IO;
using BaseFrame;
using BaseFrame.Navigation;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.LocalData;
using UnityEngine;

namespace KKSFramework.InGame
{
    public class TitleScene : SceneController
    {
        public override void InstallBindings ()
        {
            base.InstallBindings ();

            LocalDataHelper.LoadAllGameData ();
            TreeNavigation.Instance.SpawnAsync = SpawnView;
            TreeNavigation.Instance.Despawn = DespawnView;
        }
        
        
        public override async UniTask InitializeAsync (Parameters parameters)
        {
            //LocalDataManager.Instance.SetSaveAction (LocalDataHelper.SaveAllGameData);
            //await NavigationHelper.OpenPage (NavigationViewType.TitlePage, NavigationTriggerState.First);
            await base.InitializeAsync (parameters);
        }
        
        
        public override Configuration GetRootViewConfiguration ()
        {
            var config = new Configuration.Builder ();
            return config.SetName (Page.TitlePage.ToString (), true)
                .SetLayer (ContentLayer.Page)
                .Build ();
        }
        
        private async UniTask<ViewController> SpawnView (string viewName, Transform parent)
        {
            await UniTask.Yield ();

            var path = Path.Combine ("Bundles", "View", viewName);
            
            var prefab = ResourcesLoadHelper.LoadPrefab (path);
            var inst = Container.InstantiatePrefab (prefab, parent);
            return inst.GetComponent<ViewController> ();
        }


        private void DespawnView (Transform inst)
        {
            AppHelper.Destroy (inst.gameObject);
        }
    }
}