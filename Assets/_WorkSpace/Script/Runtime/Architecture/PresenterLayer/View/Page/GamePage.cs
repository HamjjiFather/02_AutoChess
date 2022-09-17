﻿using AutoChess;
using AutoChess.Presenter;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess.Presenter
{
    public class GamePage : PageViewBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject] private CharacterViewModel _characterViewModel;

        [Resolver] private ViewLayoutLoaderWithButton _viewLayoutLoader;


#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        protected void Awake()
        {
            _viewLayoutLoader.Initialize();
        }

        #endregion


        #region Methods

        protected override UniTask OnPush(object pushValue = null)
        {
            // _statusView.InitializeStatusView(_lobbyViewmodel, _itemViewmodel, BackToMain);
            // _viewLayoutLoader.SetChangeAction(ChangeViewLayoutLoader);
            BackToMain();

            // void ChangeViewLayoutLoader(int nowLayout)
            // {
            //     _statusView.ConvertButton(nowLayout < 0);
            // }

            return base.OnPush(pushValue);
        }


        public void BackToMain()
        {
            _viewLayoutLoader.CloseViewLayout();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}