using System;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Navigation
{
    public class ViewLayoutLoader : MonoBehaviour
    {
        #region Fields & Property
        
        public ViewLayoutBase[] viewLayoutObjs;
        
        public Button[] layoutViewButton;

        public bool initOnAwake = true;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private int _nowLayout;

        #endregion


        #region UnityMethods
        
        private void Awake ()
        {
            layoutViewButton.Foreach ((button, index) =>
            {
                button.onClick.AddListener (() => ClickLayoutViewButton(index));
            });

            if (!initOnAwake) return;
            Initialize ();
        }

        #endregion


        #region Methods

        public void Initialize ()
        {
            viewLayoutObjs.Foreach (x =>
            {
                x.Initialize ();
            });
        }
        
        
        public ViewLayoutBase GetViewLayout (int index)
        {
            if (viewLayoutObjs.Length <= index)
            {
                throw new IndexOutOfRangeException();
            }

            return viewLayoutObjs[index];
        }
        
        
        public void SetSubView (int index)
        {
            viewLayoutObjs[_nowLayout].DisableLayout ().Forget();
            _nowLayout = index;
            viewLayoutObjs[_nowLayout].ActiveLayout ().Forget();
        }

        #endregion


        #region EventMethods
        
        private void ClickLayoutViewButton (int index)
        {
            SetSubView (index);
        }

        #endregion
    }
}