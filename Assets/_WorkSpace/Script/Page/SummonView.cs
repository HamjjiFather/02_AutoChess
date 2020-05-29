using System;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace HexaPuzzle
{
    public class SummonView : MonoBehaviour
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private GameViewmodel _gameViewmodel;

#pragma warning restore CS0649
        
        public SummonGageElement summonGageElement;

        private IDisposable _summonValueDisposable;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _summonValueDisposable = _gameViewmodel.SummonResultModel.SummonGageValue.Subscribe (_ =>
            {
                summonGageElement.SetGageAnimation (_gameViewmodel.SummonResultModel.SummonGageValue.Value).Forget ();
            });
        }

        private void OnDestroy ()
        {
            _summonValueDisposable.DisposeSafe ();
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}