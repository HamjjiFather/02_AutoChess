using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Module
{
    [Serializable]
    public class FreezeModule : SelectableModuleBase
    {
        public FreezeModule()
        {
            useModule = false;
        }

        public override bool AutoIntializable => true;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("프리즈 대기 시간"), SuffixLabel("초", true)]
#endif
        private float cooldownSeconds = 0.5f;

        public override void Execute(Selectable selectable)
        {
            selectable
                .OnPointerClickAsObservable()
                .Where(x => selectable.enabled && selectable.interactable)
                .TakeUntilDestroy(selectable)
                .Subscribe(_ =>
                {
                    selectable.enabled = false;

                    Observable.Timer(TimeSpan.FromSeconds(cooldownSeconds)).TakeUntilDestroy(selectable).Subscribe(_ =>
                    {
                        selectable.enabled = true;
                    });
                });
        }
    }
}