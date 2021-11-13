using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine.Events;
using UnityEngine.UI;


namespace AutoChess
{
    public class ConfirmPopup : PopupViewBase, IResolveTarget
    {
        public struct Model
        {
            public UnityAction confirmAction;
        }
        
        #region Binding

#pragma warning disable 0649

        [Resolver]
        private Button _confirmButton;

#pragma warning restore 0649

        private Model _model;

        #endregion

        #region Unity Methods

        protected override void Awake ()
        {
            _confirmButton.onClick.AddListener (OnClickConfirm);
            base.Awake ();
        }

        #endregion


        #region PopupViewBase Implements

        protected override void Pushed (object pushValue = null)
        {
            if (pushValue == null) return;
            
            _model = (Model)pushValue;
            base.Pushed (pushValue);
        }

        #endregion


        #region Event callbacks

        private void OnClickConfirm ()
        {
            _model.confirmAction.Invoke ();
            ClickClose ();
        }

        #endregion
    }
}