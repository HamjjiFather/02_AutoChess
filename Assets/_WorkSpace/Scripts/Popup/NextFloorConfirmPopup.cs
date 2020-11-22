using BaseFrame;
using KKSFramework.DataBind;
using MasterData;
using UnityEngine.Events;
using UnityEngine.UI;


namespace AutoChess
{
    public class NextFloorConfirmPopup : PopupController, IResolveTarget
    {
        #region Binding

#pragma warning disable 0649

        [Resolver]
        private Button _confirmButton;

#pragma warning restore 0649

        #endregion

        #region Unity Methods

        protected override void Awake ()
        {
            _confirmButton.onClick.AddListener (OnClickConfirm);
            base.Awake ();
        }

        #endregion


        #region PopupController Implements

        #endregion


        #region Event callbacks

        private void OnClickConfirm ()
        {
            SetResult (PopupEndCode.Ok);
            OnClickClose ();
        }

        #endregion
    }
}