using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KKSFramework.Module
{
    [Serializable]
    public class InteractableEvent : UnityEvent<bool>
    {
    }


    [Serializable]
    public class InteractableModule : SelectableModuleBase
    {
        public InteractableModule()
        {
            useModule = false;
        }

        public override bool AutoIntializable => true;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("활성화일 경우 True로 작동되는 이벤트")]
#endif
        public InteractableEvent interactableEvent;

        [SerializeField]
#if ODIN_INSPECTOR
        [ShowIf(nameof(useModule)), LabelText("활성화일 경우 False로 작동되는 이벤트")]
#endif
        public InteractableEvent oppositionInteractableEvent;

        public override void Execute(Selectable selectable)
        {
            selectable
                .ObserveEveryValueChanged(x => x.interactable)
                .TakeUntilDestroy(selectable)
                .Subscribe(b =>
                {
                    interactableEvent.Invoke(b);
                    oppositionInteractableEvent.Invoke(!b);
                });
        }
    }
}