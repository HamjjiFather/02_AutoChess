using System;
using IdleFishing;
using UnityEngine;

namespace KKSFramework.Module
{
    /// <summary>
    /// 모듈화 베이스 클래스.
    /// + 모듈 사용 여부.
    /// </summary>
    [Serializable]
    public abstract class ModuleBase : IModule
    {
        public bool useModule;

        public abstract void Execute();
    }
}