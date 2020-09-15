using System;
using BaseFrame;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.UI;
using UnityEngine.UI;

namespace AutoChess
{
    public struct MessagePopupStruct
    {
        public Action ConfirmAction;

        public string Message;

        public MessagePopupStruct (Action confirmAction, string message)
        {
            ConfirmAction = confirmAction;
            Message = message;
        }
    }

    public class MessagePopup : PopupController, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Button _confirmButton;

        [Resolver]
        private Text _popupMessageText;

#pragma warning restore CS0649

        private Action _confirmAction;

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            _confirmButton.onClick.AddListener (ClickConfirmButton);
        }

        #endregion


        #region Methods

        
        
        
        protected override void OnPush (Parameters pushValue = null)
        {
            if (pushValue == null)
                return;

            var msgStruct = (MessagePopupStruct) pushValue.GetValue<MessagePopupStruct> ("struct");
            _confirmAction = msgStruct.ConfirmAction;
            _popupMessageText.text = msgStruct.Message;
            base.OnPush (pushValue);
        }

        
        protected override void OnPopComplete ()
        {
            _confirmAction = null;
            base.OnPopComplete ();
        }


        #endregion


        #region EventMethods

        private void ClickConfirmButton ()
        {
            TreeNavigationHelper.BackKey ();
            _confirmAction.CallSafe ();
        }

        #endregion
    }
}