using System;
using System.Collections.Generic;
using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework.DesignPattern;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;


namespace KKSFramework.InGame
{
    public class GameSceneInstaller : SceneInstaller
    {
        private readonly List<Type> ViewModelTypes = new List<Type> ();
        

        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = nameof (NavigationViewType.GamePage),
            actionOnFirst = OpenQuitPopup
        };

        
        protected override async UniTask InitializeAsync()
        {
            BindViewmodel ();
            ProjectInstall.Initialize ();
            await TableDataManager.Instance.LoadTableDatas ();
            ProjectInstall.InitAfterLoadLocalData ();
            ProjectInstall.InitAfterLoadTableData ();
            InitViewmodel ();

            CreateCommonView ();
            base.InitializeAsync ().Forget();

            void CreateCommonView ()
            {
                
            }
        }
        
        
        private void BindViewmodel ()
        {
            ViewModelTypes.Add (typeof(LobbyViewmodel));
            ViewModelTypes.Add (typeof(BattleViewmodel));
            ViewModelTypes.Add (typeof(ItemViewmodel));
            ViewModelTypes.Add (typeof(SkillViewmodel));
            ViewModelTypes.Add (typeof(StatusViewmodel));
            ViewModelTypes.Add (typeof(AdventureViewmodel));
            ViewModelTypes.Foreach (type => { Container.Bind (type).AsSingle (); });
        }
        
        
        
        public void InitViewmodel ()
        {
            ViewModelTypes.Foreach (type =>
            {
                var viewmodel = (ViewModelBase) Container.Resolve (type);
                viewmodel.Initialize ();
            });
        }

        
        private void OpenQuitPopup ()
        {
            var popupStruct = new MessagePopup.Model
            {
                confirmAction = Application.Quit,
                msg = "?게임을 나가시겠습니까?"
            };
                
            // NavigationManager.Instance.
            NavigationHelper.OpenPopup (NavigationViewType.MessagePopup, popupStruct).Forget();
        }
    }
}