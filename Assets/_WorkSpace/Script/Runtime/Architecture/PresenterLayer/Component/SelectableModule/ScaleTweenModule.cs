using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Module
{
    /// <summary>
    /// 크기 변경 모듈.
    /// </summary>
    [Serializable]
    public class ScaleTweenModule : SelectableModuleBase
    {
        public ScaleTweenModule()
        {
            useModule = true;
        }


        public override bool AutoIntializable => true;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("변경 값")]
#endif
        private Vector3 scale = new Vector3(1.05f, 1.05f, 1f);

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("변경 시간"), SuffixLabel("초", true)]
#endif
        private float durationSeconds = 0.1f;
        
        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("크기 변경 타겟이 자신인지")]
#endif
        private bool targetIsSelf = true;


        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), HideIf(nameof(targetIsSelf)), LabelText("크기 변경 타겟이 자신이 아닐경우 크기 변경이 이루어지는 타겟 트랜스폼")]
#endif
        public Transform target;

        private Transform _rectTransform;

        public Transform TargetTransform => targetIsSelf ? _rectTransform : target;

        private Vector3 _originalScale;

        private bool _onTweening;

        public override void Execute(Selectable selectable)
        {
            if(targetIsSelf)
                _rectTransform = selectable.transform as RectTransform;
            
            _originalScale = TargetTransform.localScale;

            selectable
                .OnPointerDownAsObservable()
                .Where(x => !_onTweening && selectable.enabled && selectable.interactable)
                .TakeUntilDestroy(selectable)
                .Subscribe(_ =>
                {
                    _onTweening = true;
                    DoScale();
                });

            selectable
                .OnPointerUpAsObservable()
                .Where(x => _onTweening && selectable.enabled && selectable.interactable)
                .TakeUntilDestroy(selectable)
                .Subscribe(_ => { DoToOrigin(); });

            selectable
                .OnDisableAsObservable()
                .Where(x => _onTweening)
                .TakeUntilDestroy(selectable)
                .Subscribe(_ =>
                {
                    _onTweening = false;
                    TargetTransform.localScale = _originalScale;
                });
        }


        public void DoScale()
        {
#if ML_DOTWEEN_SUPPORT
            TargetTransform.DOScale(scale, durationSeconds).From(_originalScale);
#endif
        }


        public void DoToOrigin()
        {
            if (TargetTransform.localScale.Equals(_originalScale))
                return;

            _onTweening = false;
            TargetTransform.localScale = scale;
#if ML_DOTWEEN_SUPPORT
            TargetTransform.DOScale(_originalScale, durationSeconds).From(scale);
#endif
        }
    }
}