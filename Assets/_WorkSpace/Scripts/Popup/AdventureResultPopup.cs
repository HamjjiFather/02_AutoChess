using System;
using BaseFrame;
using KKSFramework.DataBind;

namespace AutoChess
{
    public class AdventureResultPopup : PopupController, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private Action _closeAction;

        #endregion

        #region Methods

        protected override void OnPush (Parameters pushValue = null)
        {
            _closeAction = pushValue.GetValue<Action> ("action");
            base.OnPush (pushValue);
        }


        protected override void OnPopComplete ()
        {
            _closeAction = null;
            base.OnPopComplete ();
        }


        #endregion


        #region EventMethods

        protected override void OnClickClose ()
        {
            _closeAction.CallSafe ();
            base.OnClickClose ();
        }


        #endregion
    }
}