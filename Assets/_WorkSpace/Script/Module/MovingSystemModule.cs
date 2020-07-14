using System;
using System.Threading;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class MovingSystemModule : MonoBehaviour
    {
        #region Fields & Property

        public Transform movingTarget;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private BattleViewLayout _battleViewLayout;

#pragma warning restore CS0649

        private IDisposable _movingDisposable;

        private LandElement _moveTargetLandElement;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public async UniTask Moving (PositionModel positionModel, CancellationToken cancellationToken)
        {
            _moveTargetLandElement = _battleViewLayout.GetLandElement (positionModel);

            _movingDisposable = Observable.EveryUpdate ().Subscribe (_ =>
            {
                var element = _battleViewLayout.GetLandElement (positionModel).characterPositionTransform;
                movingTarget.MoveTowards (movingTarget.position, element.position, Time.deltaTime);
            });
            
            await UniTask.WaitWhile (() => Vector2.Distance (movingTarget.position, _moveTargetLandElement.characterPositionTransform.position) >
                                             float.Epsilon, cancellationToken:cancellationToken);
            
            _movingDisposable.DisposeSafe ();
        }

        #endregion
    }
}