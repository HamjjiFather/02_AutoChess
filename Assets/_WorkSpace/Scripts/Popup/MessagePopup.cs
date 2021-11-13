using System;
using Cysharp.Threading.Tasks;
using KKSFramework;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class MessagePopup : PopupViewBase
    {
        public struct Model
        {
            public string title;

            public string msg;

            public UnityAction confirmAction;
        }
        
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Button _confirmButton;

        [Resolver]
        private Property<string> _popupTitleText;

        [Resolver]
        private Property<string> _popupMessageText;

#pragma warning restore CS0649

        private UnityAction _confirmAction;

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            _confirmButton.onClick.AddListener (ClickConfirmButton);
        }

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue)
        {
            if (pushValue == null)
                return UniTask.CompletedTask;
            
            var msgModel = (Model)pushValue;
            _popupTitleText.Value = msgModel.title;
            _popupMessageText.Value = msgModel.msg;
            _confirmAction = msgModel.confirmAction;
            return base.OnPush (pushValue);
        }

        protected override UniTask Popped ()
        {
            _confirmAction = null;
            return base.Popped ();
        }



        public static void PushNotEnoughPrice ()
        {
            // var param = TreeNavigationHelper.SpawnParam ();
            // param[MessagePopupStringKey] = LocalizeHelper.FromDescription ("DESC_0001");
            // TreeNavigationHelper.PushPopup (nameof (MessagePopup), param);
        }

        #endregion


        #region EventMethods

        private void ClickConfirmButton ()
        {
            _confirmAction.Invoke ();
        }

        #endregion
    }
}