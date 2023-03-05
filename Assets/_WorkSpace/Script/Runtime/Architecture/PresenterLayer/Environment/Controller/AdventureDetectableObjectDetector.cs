using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UniRx;
using UnityEngine;

namespace AutoChess
{
    /// <summary>
    /// Start이후 시점에서 보내줘야 한다.
    /// </summary>
    public readonly struct RegisterDetectableMessage
    {
        public readonly IDetectableObject DetectableObject;

        public RegisterDetectableMessage(IDetectableObject detectableObject)
        {
            DetectableObject = detectableObject;
        }
    }

    public class AdventureDetectableObjectDetector : MonoBehaviour
    {
        /// <summary>
        /// 탐지가 이루어지는 주기(초).
        /// </summary>
        public const float DetectUpdateSeconds = .1f;

        /// <summary>
        /// 탐지가능한 범위.
        /// </summary>
        public const float DetectRange = 2f;

        private readonly List<IDetectableObject> _detectableObjects = new();

        private IDisposable _disposable;


        #region Methods

        #region Override

        private void Awake()
        {
            MessageBroker.Default.Receive<RegisterDetectableMessage>()
                .TakeUntilDestroy(this)
                .Subscribe(RegisterDetectable);
        }

        #endregion


        #region This

        public void RegisterDetectable(RegisterDetectableMessage message)
        {
            _detectableObjects.Add(message.DetectableObject);
        }


        public void Activate()
        {
            _disposable.DisposeSafe();
            // ACTODO: 캐릭터가 움직일때 조건이 추가되어야 한다.
            _disposable = Observable.Interval(TimeSpan.FromSeconds(DetectUpdateSeconds)).Subscribe(_ => { Detect(); });
        }


        public void Deactivate()
        {
        }


        /// <summary>
        /// 주변을 감지함.
        /// </summary>
        public void Detect()
        {
            var pos = transform.position;

            _detectableObjects.ForEach(d =>
            {
                Debug.Log(d);
                
                var dPos = d.Position;
                var dist = Vector3.Distance(pos, dPos);
                var detect = dist <= DetectRange;

                switch (d.Detect)
                {
                    case true when !detect:
                        d.OnFarAway();
                        return;

                    case false when detect:
                        d.OnDetected();
                        break;
                }
            });
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}