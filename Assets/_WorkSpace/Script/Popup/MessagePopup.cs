using System;
using Cysharp.Threading.Tasks;
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
    
    public class MessagePopup : PopupViewBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private ButtonExtension _confirmButton;

        [Resolver]
        private Text _popupMessageText;

#pragma warning restore CS0649

        private Action _confirmAction;

        #endregion


        #region UnityMethods

        protected void Awake ()
        {
            _confirmButton.onClick.AddListener (ClickConfirmButton);
        }

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            if (pushValue == null)
                return UniTask.CompletedTask;
            
            var msgStruct = (MessagePopupStruct)pushValue;
            _confirmAction = msgStruct.ConfirmAction;
            _popupMessageText.text = msgStruct.Message;
            return base.OnPush (pushValue);
        }


        protected override UniTask Popped ()
        {
            _confirmAction = null;
            return base.Popped ();
        }

        #endregion


        #region EventMethods

        private void ClickConfirmButton ()
        {
            NavigationHelper.GoBackPage ();
            _confirmAction.CallSafe ();
        }

        #endregion
    }
}