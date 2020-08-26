using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class FieldLandElement : LandElement
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private GameObject _fieldObj;

        [Resolver]
        private GameObject[] _fieldTypeObjs;

        [Resolver]
        private GameObject[] _fieldGroundTypeObjs;

        [Resolver]
        private GameObject _sealedObj;

        [Resolver]
        private GameObject _revealedObj;

        [Resolver]
        private Button _landButton;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

#pragma warning restore CS0649

        public FieldModel FieldModel { get; private set; }

        private FieldViewLayout _fieldViewLayout;

        private List<IDisposable> _fieldTypeDisposables;

        #endregion


        #region UnityMethods

        private void Start ()
        {
            _landButton.onClick.AddListener (ClickElement);
        }

        #endregion


        #region Methods

        public void Element (FieldModel fieldModel)
        {
            FieldModel = fieldModel;
            _fieldTypeDisposables = new List<IDisposable> ();

            var stateDisposable = FieldModel.FieldRevealState.Subscribe (state =>
            {
                _sealedObj.SetActive (state == FieldRevealState.Sealed);
                _revealedObj.SetActive (state == FieldRevealState.Revealed);

                if (state == FieldRevealState.Sealed) return;
                SetFieldSpecialType ();
                SetFieldGroundType ();

                // _fieldObj.SetActive (state != FieldRevealState.Sealed);
            });

            _fieldTypeObjs.Foreach (x => x.SetActive (false));
            _fieldTypeObjs[(int) fieldModel.FieldSpecialType.Value].SetActive (true);
            var specialTypeDisposable = FieldModel.FieldSpecialType.Subscribe (type =>
            {
                _fieldTypeObjs.Where (x => x.activeSelf).Foreach (x => x.SetActive (false));
                _fieldTypeObjs[(int) type].SetActive (FieldModel.FieldRevealState.Value != FieldRevealState.Sealed);
            });

            _fieldGroundTypeObjs.Foreach (x => x.SetActive (false));
            var groundTypeDisposable = FieldModel.FieldGroundType.Subscribe (type =>
            {
                _fieldGroundTypeObjs.Where (x => x.activeSelf).Foreach (x => x.SetActive (false));
                _fieldGroundTypeObjs[(int) type]
                    .SetActive (FieldModel.FieldRevealState.Value != FieldRevealState.Sealed);
            });

            _fieldTypeDisposables.AddRange (new[]
            {
                stateDisposable, specialTypeDisposable, groundTypeDisposable
            });
            _fieldViewLayout = ProjectContext.Instance.Container.Resolve<FieldViewLayout> ();
            PositionModel = fieldModel.LandPosition;

            void SetFieldSpecialType ()
            {
                _fieldTypeObjs[(int) FieldModel.FieldSpecialType.Value].SetActive (true);
            }

            void SetFieldGroundType ()
            {
                _fieldGroundTypeObjs[(int) FieldModel.FieldGroundType.Value].SetActive (true);
            }
        }

        #endregion


        #region EventMethods

        private void ClickElement ()
        {
            if (FieldModel.FieldRevealState.Value != FieldRevealState.Sealed)
                _fieldViewLayout.ChangePosition (this).Forget ();
        }

        #endregion
    }
}