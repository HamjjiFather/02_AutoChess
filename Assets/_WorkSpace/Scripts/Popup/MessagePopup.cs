using System;
using BaseFrame;
using Helper;
using KKSFramework.DataBind;
using UnityEngine.UI;

namespace AutoChess
{
    public class MessagePopup : PopupController, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Button _confirmButton;

        [Resolver]
        private Property<string> _popupTitleText;

        [Resolver]
        private Property<string> _popupMessageText;

#pragma warning restore CS0649

        private Action _confirmAction;

        public const string MessagePopupTitleKey = "title";
        
        public const string MessagePopupStringKey = "message";

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            _confirmButton.onClick.AddListener (ClickConfirmButton);
        }

        #endregion


        #region Methods
        
        
        protected override void OnPush (Parameters pushValue)
        {
            if (pushValue == null)
                return;
            var title = pushValue.GetValue<string> (MessagePopupTitleKey);
            _popupTitleText.Value = title;

            var msg = pushValue.GetValue<string> (MessagePopupStringKey);
            _popupMessageText.Value = msg;
            base.OnPush (pushValue);
        }

        
        protected override void OnPopComplete ()
        {
            _confirmAction = null;
            base.OnPopComplete ();
        }


        public static void PushNotEnoughPrice ()
        {
            var param = TreeNavigationHelper.SpawnParam ();
            param[MessagePopupStringKey] = LocalizeHelper.FromDescription ("DESC_0001");
            TreeNavigationHelper.PushPopup (nameof(MessagePopup), param);
        }


        #endregion


        #region EventMethods

        private void ClickConfirmButton ()
        {
            SetResult (PopupEndCode.Ok);
            TreeNavigationHelper.BackKey ();
            _confirmAction.CallSafe ();
        }

        #endregion
    }
}