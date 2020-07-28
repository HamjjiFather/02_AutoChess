using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace AutoChess
{
    public class MovingSystemModule : MonoBehaviour
    {
        #region Fields & Property

        public Transform movingTarget;

#pragma warning disable CS0649
        
#pragma warning restore CS0649

        private IDisposable _movingDisposable;

        public bool IsMoving { get; private set; }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void Dispose ()
        {
            _movingDisposable.DisposeSafe ();
        }


        public async UniTask Moving (LandElement landElement, CancellationToken cancellationToken)
        {
            IsMoving = true;
            _movingDisposable = Observable.EveryUpdate ().Subscribe (_ =>
            {
                var element = landElement.characterPositionTransform;
                movingTarget.MoveTowards (movingTarget.position, element.position, Time.deltaTime);
            });

            await UniTask.WaitWhile (() =>
                Vector2.Distance (movingTarget.position, landElement.characterPositionTransform.position) >
                float.Epsilon, cancellationToken: cancellationToken);

            IsMoving = false;
            _movingDisposable.DisposeSafe ();
        }


        public async UniTask Moving (IEnumerable<LandElement> landElements, Action<PositionModel> onArriveAction, CancellationToken cancellationToken)
        {
            var landQueue = new Queue<LandElement> ();
            landQueue.Enqueues (landElements);

            var element = landQueue.Dequeue ();
            IsMoving = true;
            
            _movingDisposable = Observable.EveryUpdate ().Subscribe (_ =>
            {
                var elementPosition = element.characterPositionTransform.position;
                movingTarget.MoveTowards (movingTarget.position, elementPosition, Time.deltaTime);
                if (!(Vector2.Distance (movingTarget.position, elementPosition) < float.Epsilon)) return;
                
                onArriveAction.CallSafe (element.PositionModel);
                if (landQueue.Count == 0)
                {
                    IsMoving = false;
                    _movingDisposable.DisposeSafe ();
                    return;
                }
                    
                element = landQueue.Dequeue ();
            });

            await UniTask.WaitWhile (() => IsMoving, cancellationToken: cancellationToken);
        }

        #endregion
    }
}