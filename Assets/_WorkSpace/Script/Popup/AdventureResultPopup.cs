using System;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;

namespace AutoChess
{
    public class AdventureResultPopup : PopupViewBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private Action _closeAction;

        #endregion


        #region UnityMethods


        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            _closeAction = (Action) pushValue;
            return base.OnPush (pushValue);
        }


        protected override UniTask Popped ()
        {
            _closeAction = null;
            return base.Popped ();
        }

        #endregion


        #region EventMethods
        
        protected override void ClickClose ()
        {
            _closeAction.CallSafe ();
            base.ClickClose ();
        }

        #endregion
    }
}