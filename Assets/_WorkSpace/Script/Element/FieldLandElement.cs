using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class FieldLandElement : LandElement
    {
        #region Fields & Property

        public GameObject sealedObj;

        public GameObject revealedObj;

        public Button landButton;

#pragma warning disable CS0649

        [Inject]
        private FieldViewmodel _fieldViewmodel;

#pragma warning restore CS0649

        private FieldModel _fieldModel;
        
        private FieldViewLayout _fieldViewLayout;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            landButton.onClick.AddListener (ClickElement);
        }

        #endregion


        #region Methods

        public void SetElement (FieldModel fieldModel)
        {
            _fieldModel = fieldModel;

            _fieldModel.RevealState.Subscribe (state =>
            {
                sealedObj.SetActive (state == FieldRevealState.Sealed);
                revealedObj.SetActive (state == FieldRevealState.Revealed);
            });

            _fieldViewLayout = ProjectContext.Instance.Container.Resolve<FieldViewLayout> ();

            PositionModel = fieldModel.LandPosition;
        }

        #endregion


        #region EventMethods

        private void ClickElement ()
        {
            if(_fieldModel.RevealState.Value != FieldRevealState.Sealed)
                _fieldViewLayout.ChangePosition (_fieldModel.LandPosition);
        }

        #endregion
    }
}