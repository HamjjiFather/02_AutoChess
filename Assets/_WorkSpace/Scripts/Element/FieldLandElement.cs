using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class FieldLandElement : LandElement
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Property<bool> _fieldSprite;
        
        [Resolver]
        private Property<bool> _fieldActive;

        [Resolver]
        private GameObject[] _fieldTypeObjs;

        [Resolver]
        private GameObject[] _fieldGroundTypeObjs;

        [Resolver]
        private GameObject _sealedObj;

        [Resolver]
        private GameObject _revealedObj;

        [Resolver]
        private ButtonExtension _landButton;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

#pragma warning restore CS0649

        public FieldModel FieldModel { get; private set; }

        private FieldViewLayout _fieldViewLayout;

        private List<IDisposable> _fieldTypeDisposables;

        private bool FieldExistState => FieldModel.FieldExistType.Value == FieldExistType.Exist;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        /// <summary>
        /// 필드 설정.
        /// 필드가 안보임 상태일 경우 세팅 안함.
        /// </summary>
        public void Element (FieldModel fieldModel)
        {
            FieldModel = fieldModel;
            if (!FieldExistState)
            {
                _fieldActive.Value = false;
                return;
            }
            
            _fieldTypeDisposables = new List<IDisposable> ();
            _fieldSprite.Value = true;
            _fieldActive.Value = true;
            _landButton.RemoveAllListeners ();
            _landButton.AddListener (ClickElement);
            
            var stateDisposable = FieldModel.FieldRevealState.Subscribe (state =>
            {
                _sealedObj.SetActive (state == FieldRevealState.Sealed);
                _revealedObj.SetActive (state == FieldRevealState.Revealed);

                if (state == FieldRevealState.Sealed) return;
                SetFieldSpecialType ();
                SetFieldGroundType ();

                // _fieldObj.SetActive (state != FieldRevealState.Sealed);
            });

            _fieldTypeObjs.ForEach (x => x.SetActive (false));
            _fieldTypeObjs[(int) fieldModel.FieldSpecialType.Value].SetActive (true);
            var specialTypeDisposable = FieldModel.FieldSpecialType.Subscribe (type =>
            {
                _fieldTypeObjs.Where (x => x.activeSelf).ForEach (x => x.SetActive (false));
                _fieldTypeObjs[(int) type].SetActive (FieldModel.FieldRevealState.Value != FieldRevealState.Sealed);
            });

            _fieldGroundTypeObjs.ForEach (x => x.SetActive (false));
            var groundTypeDisposable = FieldModel.FieldGroundType.Subscribe (type =>
            {
                _fieldGroundTypeObjs.Where (x => x.activeSelf).ForEach (x => x.SetActive (false));
                _fieldGroundTypeObjs[(int) type]
                    .SetActive (FieldModel.FieldRevealState.Value != FieldRevealState.Sealed);
            });

            _fieldTypeDisposables.AddRange (new[]
            {
                stateDisposable, specialTypeDisposable, groundTypeDisposable
            });
            _fieldViewLayout = ProjectContext.Instance.Container.Resolve<FieldViewLayout> ();
            PositionModel = fieldModel.LandPosition;
            
            HighlightedField.SetActive (FieldModel.FieldHighlight.Value);

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