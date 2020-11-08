using BaseFrame;
using KKSFramework.DataBind;
using UnityEngine.Events;
using UnityEngine.UI;


namespace AutoChess
{
    public class NextFloorConfirmPopup : PopupController, IResolveTarget
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

        private Model _popupModel;

        #endregion


        #region Unity Methods

        protected override void Awake ()
        {
            _confirmButton.onClick.AddListener (OnClickConfirm);
            base.Awake ();
        }

        #endregion


        #region PopupController Implements

        protected override void OnPush (Parameters parameters)
        {
            _popupModel = (Model) parameters["PopupModel"];
            base.OnPush (parameters);

            // 초기화 영역
        }


        protected override void OnPop ()
        {
            base.OnPop ();

            // 사용한 객체 해제 영역
        }

        #endregion


        #region Event callbacks

        private void OnClickConfirm ()
        {
            _popupModel.confirmAction.Invoke ();
        }

        #endregion
    }
}