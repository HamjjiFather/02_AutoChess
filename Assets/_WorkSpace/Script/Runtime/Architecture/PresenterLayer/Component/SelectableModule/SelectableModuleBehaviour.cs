using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KKSFramework.Module
{
    public enum ModuleInitializePointType
    {
        Awake,
        Start,
        OnEnable,
        
        /// <summary>
        /// 모듈을 수동으로 초기화 해야한다.
        /// </summary>
        None,
    }


    [Serializable]
    public abstract class SelectableModuleBase : ModuleBase
    {
#if ODIN_INSPECTOR
        [LabelText("자동 초기화 시점"), EnableIf(nameof(AutoIntializable)), ShowIf(nameof(useModule))]
#endif
        public ModuleInitializePointType initPointType;

        public abstract bool AutoIntializable { get; }

        public abstract void Execute(Selectable selectable);

        public virtual void DisposeModule()
        {
        }

        public override void Execute() { }
    }


    /// <summary>
    /// 다른 프로젝트에 사용해도 괜찮음.
    /// 선택 가능한 컴포넌트의 추가 모듈 클래스.
    /// 클릭 사운드, 홀드 온 이벤트, 클릭 시 크기 변경, 프리즈, 활성화 모듈이 있다.
    /// 밑에 작성된 클래스 까지도 포함 가능하다.
    /// IFTODO - 나중에 Selectable이 아닌 RaycastTarget으로 변경해도 될 듯.
    /// <seealso cref="KKSFramework.ModuleButtonModuleBehaviour"/>
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class SelectableModuleBehaviour : MonoBehaviour
    {
        #region Fields & Property

        private Selectable _selectable;

        public Selectable Selectable
        {
            get
            {
                if (!_selectable)
                {
                    _selectable = GetComponent<Selectable>();
                }

                return _selectable;
            }
        }


        public bool interactable
        {
            set => Selectable.interactable = value;
            get => Selectable.IsInteractable();
        }
        
        
        public Button button => (Button) Selectable;
        
        public UnityEvent onClick => button.onClick;


        [SerializeField]
#if ODIN_INSPECTOR
        [FoldoutGroup(PointerClickSfxModuleGroupName), HideLabel]
#endif
        private PointerClickSfxModule sfxModule;

        [SerializeField]
#if ODIN_INSPECTOR
        [FoldoutGroup(HoldOnModuleGroupName), HideLabel]
#endif
        private HoldOnModule holdOnModule;

        [SerializeField]
#if ODIN_INSPECTOR
        [FoldoutGroup(ScaleTweenModuleGroupName), HideLabel]
#endif
        private ScaleTweenModule scaleTweenModule;

        [SerializeField]
#if ODIN_INSPECTOR
        [FoldoutGroup(FreezeModuleGroupName), HideLabel]
#endif
        private FreezeModule freezeModule;

        [SerializeField]
#if ODIN_INSPECTOR
        [FoldoutGroup(InteractableModuleGroupName), HideLabel]
#endif
        private InteractableModule interactableModule;

        [UsedImplicitly] private const string PointerClickSfxModuleGroupName = "클릭 사운드 모듈";

        [UsedImplicitly] private const string HoldOnModuleGroupName = "홀드 온 이벤트 모듈";

        [UsedImplicitly] private const string ScaleTweenModuleGroupName = "크기 변경 모듈";

        [UsedImplicitly] private const string FreezeModuleGroupName = "프리즈 모듈";

        [UsedImplicitly] private const string InteractableModuleGroupName = "활성화 모듈";

        protected readonly List<SelectableModuleBase> Modules = new List<SelectableModuleBase>();

        #endregion


        #region Methods

        #region Override

        protected virtual void Awake()
        {
            EntryModules();

            Modules
                .Where(x => x.useModule && x.AutoIntializable && x.initPointType == ModuleInitializePointType.Awake)
                .Foreach(x => x.Execute(Selectable));
        }


        private void Start()
        {
            Modules
                .Where(x => x.useModule && x.AutoIntializable && x.initPointType == ModuleInitializePointType.Start)
                .Foreach(x => x.Execute(Selectable));
        }


        private void OnEnable()
        {
            Modules
                .Where(x => x.useModule && x.AutoIntializable && x.initPointType == ModuleInitializePointType.OnEnable)
                .Foreach(x => x.Execute(Selectable));
        }

        #endregion


        #region This

        protected virtual void EntryModules()
        {
            Modules.Add(sfxModule);
            Modules.Add(holdOnModule);
            Modules.Add(scaleTweenModule);
            Modules.Add(freezeModule);
            Modules.Add(interactableModule);
        }


        public void DisposeAll()
        {
            Modules.Where(x => x.useModule).Foreach(x => x.DisposeModule());
        }


        public void SetUseHoldOnModuleState(bool use) => holdOnModule.useModule = use;


        public void HoldOnModuleSubscribe(Action holdOnAction) =>
            holdOnModule.ModuleSubscribe(Selectable, holdOnAction);


        public void DisposeHoleOnModule() => holdOnModule.DisposeModule();

        
        public void ReSubscribe(Selectable selectable) => holdOnModule.ReSubscribe(selectable);

        
        public bool Initialized => holdOnModule.Initialized;


        public void SetTargetScaleTweenModuleTarget(Transform target)
        {
            scaleTweenModule.target = target;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}