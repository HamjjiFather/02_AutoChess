using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;
using KKSFramework;
using UnityEngine;

namespace AutoChess
{
    public class MovingSystemModule : MonoBehaviour
    {
        #region Fields & Property


#pragma warning disable CS0649
        
#pragma warning restore CS0649

        private Transform _movingTarget;
        
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
                var element = landElement.transform;
                _movingTarget.MoveTowards (_movingTarget.position, element.position, Time.deltaTime);
            });

            await UniTask.WaitWhile (() =>
                Vector2.Distance (_movingTarget.position, landElement.transform.position) >
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
                var elementPosition = element.transform.position;
                var movingTargetPosition = _movingTarget.position;
                _movingTarget.MoveTowards (movingTargetPosition, elementPosition, Time.deltaTime);
                
                var distance = Vector2.Distance (movingTargetPosition, elementPosition);
                var dist = Math.Truncate (distance * 100) * 0.01f;
                if (dist > 0) return;
                
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


        public void SetMovingTarget (Transform transform)
        {
            _movingTarget = transform;
        }

        #endregion
    }
}