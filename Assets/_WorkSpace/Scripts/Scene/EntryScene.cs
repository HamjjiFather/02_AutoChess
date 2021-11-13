﻿using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;


namespace KKSFramework.InGame
{
    public class EntryScene : SceneController
    {
        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = nameof (NavigationViewType.EntryPage),
            actionOnFirst = null
        };


        protected override async UniTask InitializeAsync ()
        {
            SceneLoadManager.Instance.InitManager ();
            await base.InitializeAsync ();
            await UniTask.CompletedTask;
        }
        
        public override void Start ()
        {
            base.Start ();
        }
    }
}