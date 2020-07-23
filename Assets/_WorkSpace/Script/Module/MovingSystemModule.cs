using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

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

            IsMoving = true;

            var element = landQueue.Dequeue ();
            
            _movingDisposable = Observable.EveryUpdate ().Subscribe (async _ =>
            {
                await CheckMove ();
                
                if (landQueue.Count != 0)
                {
                    onArriveAction.CallSafe (element.PositionModel);
                    element = landQueue.Dequeue ();
                    await CheckMove ();
                    return;
                }

                onArriveAction.CallSafe (element.PositionModel);
                _movingDisposable.DisposeSafe ();
                IsMoving = false;
                
                async UniTask CheckMove ()
                {
                    movingTarget.MoveTowards (movingTarget.position, element.characterPositionTransform.position, Time.deltaTime);
                    await UniTask.WaitWhile (() =>
                            Vector2.Distance (movingTarget.position, element.characterPositionTransform.position) > float.Epsilon,
                        cancellationToken: cancellationToken);
                }
            });
        }

        #endregion
    }
}