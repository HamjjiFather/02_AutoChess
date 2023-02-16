using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Module
{
    [Serializable]
    public class PointerClickSfxModule : SelectableModuleBase
    {
        public PointerClickSfxModule()
        {
            useModule = true;
        }

        /// <summary>
        /// ModuleInitializePointType에 자동으로 초기화 되어 자체 실행할 수 있는지.
        /// </summary>
        public override bool AutoIntializable => true;

        [SerializeField]
#if ODIN_INSPECTOR
        [LabelText("클릭 시 출력 될 Sfx"), ShowIf(nameof(useModule))]
#endif
        // public Fx pointerClickFx = Fx.EFFECT_BUTTON_CLICK;

        public override void Execute(Selectable selectable)
        {
            // selectable
            //     .OnPointerClickAsObservable()
            //     .Where(x => selectable.enabled && selectable.interactable)
            //     .TakeUntilDestroy(selectable)
            //     .Subscribe(b => { AudioHelper.Play(pointerClickFx); });
        }
    }
}