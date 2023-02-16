using KKSFramework.Module;
using UnityEngine;

namespace KKSFramework.ModuleAllBlue
{
    /// <summary>
    /// 모듈화된 컴포넌트 베이스 클래스.
    /// + 모듈의 진입 시점.
    /// </summary>
    public abstract class ModuleBehaviour : MonoBehaviour, IModule
    {
        #region Fields & Property

        public ModuleInitializePointType initializePoint = ModuleInitializePointType.None;

        #endregion


        #region Methods

        #region Override
        
        protected virtual void Awake()
        {
            if (initializePoint == ModuleInitializePointType.Awake)
                Execute();
        }


        protected virtual void Start()
        {
            if (initializePoint == ModuleInitializePointType.Start)
                Execute();
        }


        protected virtual void OnEnable()
        {
            if (initializePoint == ModuleInitializePointType.OnEnable)
                Execute();
        }


        public abstract void Execute();

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}