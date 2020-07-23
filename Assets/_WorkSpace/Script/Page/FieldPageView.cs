using System.Linq;
using KKSFramework.Navigation;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class FieldPageView : PageViewBase
    {
        #region Fields & Property

        public ViewLayoutLoader viewLayoutLoader;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            viewLayoutLoader.SetSubView (0);
            return base.OnPush (pushValue);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}