﻿using UniRx.Async;

namespace KKSFramework.Navigation
{
    public class TitleScene : SceneController
    {
        public TitlePageView titlePageView;

        protected override UniTask InitializeAsync ()
        {
            titlePageView.Push ().Forget();
            return base.InitializeAsync ();
        }
    }
}