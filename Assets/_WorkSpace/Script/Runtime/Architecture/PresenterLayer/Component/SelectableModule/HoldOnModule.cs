using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Module
{
    [Serializable]
    public class HoldOnModule : SelectableModuleBase
    {
        public HoldOnModule()
        {
            useModule = false;
        }

        public override bool AutoIntializable => false;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("기본 홀드 대기 시간"), SuffixLabel("초", true)]
#endif
        private float baseHoldOnDurationSeconds = 0.1f;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("홀드 감지 시간"), SuffixLabel("초", true)]
#endif
        private float throttleDurationSecondsAtFirst = 0.2f;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("최소 홀드 대기 시간"), SuffixLabel("초", true)]
#endif
        private float minDurationSeconds = 0.02f;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("홀드 이벤트 처리마다 줄어들 홀드 대기 시간"), SuffixLabel("초", true)]
#endif
        private float decDurationSecPerCallEvent = 0.02f;

        public bool Initialized;

        private Action _holdOnAction;

        private bool _onHold;

        private IDisposable _clickDisposable, _holdOnDisposable;

        public void ModuleSubscribe(Selectable selectable, Action holdOnAction)
        {
            Initialized = true;
            
            _holdOnAction = holdOnAction;
            _clickDisposable.DisposeSafe();
            _clickDisposable = selectable.OnPointerClickAsObservable()
                .TakeUntilDestroy(selectable)
                .Where(x => !_onHold && selectable.interactable)
                .Subscribe(_ => holdOnAction());

            Execute(selectable);
        }


        public override void Execute(Selectable selectable)
        {
            var durationSec = throttleDurationSecondsAtFirst;
            var stackedSec = 0f;
            var c = 0;

            _holdOnDisposable.DisposeSafe();
            _holdOnDisposable = selectable.UpdateAsObservable()
                .TakeUntilDestroy(selectable)
                .Where(_ => useModule)
                .SkipUntil(selectable.OnPointerDownAsObservable().Do(_ => { StartHold(); }))
                .TakeUntil(selectable.OnPointerUpAsObservable().Do(_ => EndHold()))
                .Subscribe(_ => CheckDelay());

            void CheckDelay()
            {
                if (!useModule || !selectable.interactable || !selectable.gameObject.activeSelf || !selectable.enabled)
                {
                    EndHold();
                    return;
                }

                stackedSec += Time.deltaTime;
                if (stackedSec < durationSec) return;
                stackedSec -= durationSec;
                durationSec = Mathf.Max(minDurationSeconds,
                    baseHoldOnDurationSeconds - decDurationSecPerCallEvent * ++c);

                _holdOnAction();
            }


            void StartHold()
            {
                if (!useModule || !selectable.interactable || !selectable.gameObject.activeSelf || !selectable.enabled)
                    return;

                _onHold = true;
            }


            void EndHold()
            {
                _onHold = false;
                Execute(selectable);
            }
        }


        public void ReSubscribe(Selectable selectable)
        {
            _onHold = false;
            Execute(selectable);
        }

        public override void DisposeModule()
        {
            Initialized = false;
            _clickDisposable.DisposeSafe();
            _holdOnDisposable.DisposeSafe();
            _onHold = false;
        }
    }
}