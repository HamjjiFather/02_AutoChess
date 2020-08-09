using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Cysharp.Threading.Tasks;
using KKSFramework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class FieldLandElement : LandElement
    {
        #region Fields & Property

        public GameObject fieldObj;

        public GameObject[] fieldTypeObjs;

        public GameObject[] fieldGroundTypeObjs;

        public GameObject sealedObj;

        public GameObject revealedObj;

        public Button landButton;

#pragma warning disable CS0649

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

#pragma warning restore CS0649

        private FieldModel _fieldModel;
        public FieldModel FieldModel => _fieldModel;

        private FieldViewLayout _fieldViewLayout;

        private List<IDisposable> _fieldTypeDisposables;

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
            _fieldTypeDisposables = new List<IDisposable> ();

            var stateDisposable = _fieldModel.FieldRevealState.Subscribe (state =>
            {
                sealedObj.SetActive (state == FieldRevealState.Sealed);
                revealedObj.SetActive (state == FieldRevealState.Revealed);

                if (state != FieldRevealState.Sealed)
                {
                    SetFieldSpecialType ();
                    SetFieldGroundType ();
                }
                // fieldObj.SetActive (state != FieldRevealState.Sealed);
            });

            fieldTypeObjs.Foreach (x => x.SetActive (false));
            fieldTypeObjs[(int) fieldModel.FieldSpecialType.Value].SetActive (true);
            var specialTypeDisposable = _fieldModel.FieldSpecialType.Subscribe (type =>
            {
                fieldTypeObjs.Where (x => x.activeSelf).Foreach (x => x.SetActive (false));
                fieldTypeObjs[(int) type].SetActive (_fieldModel.FieldRevealState.Value != FieldRevealState.Sealed);
            });

            fieldGroundTypeObjs.Foreach (x => x.SetActive (false));
            var groundTypeDisposable = _fieldModel.FieldGroundType.Subscribe (type =>
            {
                fieldGroundTypeObjs.Where (x => x.activeSelf).Foreach (x => x.SetActive (false));
                fieldGroundTypeObjs[(int) type].SetActive (_fieldModel.FieldRevealState.Value != FieldRevealState.Sealed);
            });

            _fieldTypeDisposables.AddRange (new[]
            {
                stateDisposable, specialTypeDisposable, groundTypeDisposable
            });
            _fieldViewLayout = ProjectContext.Instance.Container.Resolve<FieldViewLayout> ();
            PositionModel = fieldModel.LandPosition;

            void SetFieldSpecialType ()
            {
                fieldTypeObjs[(int) _fieldModel.FieldSpecialType.Value].SetActive (true);
            }

            void SetFieldGroundType ()
            {
                fieldGroundTypeObjs[(int)_fieldModel.FieldGroundType.Value].SetActive (true);
            }
        }

        #endregion


        #region EventMethods

        private void ClickElement ()
        {
            if (_fieldModel.FieldRevealState.Value != FieldRevealState.Sealed)
                _fieldViewLayout.ChangePosition (this).Forget ();
        }

        #endregion
    }
}