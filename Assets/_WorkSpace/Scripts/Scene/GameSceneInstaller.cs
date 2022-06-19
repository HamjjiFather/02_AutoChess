using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<Type> _viewModelTypes = new List<Type>();


        public override InitNavigationData InitPageInitNavigationData => new InitNavigationData
        {
            viewString = nameof(NavigationViewType.GamePage),
            actionOnFirst = OpenQuitPopup
        };


        public override void InstallBindings()
        {
            BindViewmodel();
            base.InstallBindings();
        }



        protected override async void Awake()
        {
            await TableDataManager.Instance.LoadTableDatas();
            InitViewmodel();
            await base.AwakeAsync();
        }


        private void BindViewmodel()
        {
            _viewModelTypes.Add(typeof(CharacterViewmodel));
            _viewModelTypes.Add(typeof(ItemViewmodel));
            _viewModelTypes.Add(typeof(SkillViewmodel));
            _viewModelTypes.Add(typeof(StatusViewmodel));
            _viewModelTypes.Add(typeof(AdventureViewmodel));
            _viewModelTypes.Add(typeof(LobbyViewmodel));
            _viewModelTypes.Foreach(type => { Container.Bind(type).AsSingle(); });
        }


        public void InitViewmodel()
        {
            var vmBase = _viewModelTypes.Select(x => (ViewModelBase) Container.Resolve(x)).ToList();
            
            vmBase.Foreach(vm =>
            {
                vm.Initialize();
            });
            
            vmBase.Foreach(vm =>
            {
                vm.InitFinally();
            });
        }
        

        private void OpenQuitPopup()
        {
            var popupStruct = new MessagePopup.Model
            {
                confirmAction = Application.Quit,
                msg = "?게임을 나가시겠습니까?"
            };

            // NavigationManager.Instance.
            NavigationHelper.OpenPopup(NavigationViewType.MessagePopup, popupStruct).Forget();
        }
    }
}