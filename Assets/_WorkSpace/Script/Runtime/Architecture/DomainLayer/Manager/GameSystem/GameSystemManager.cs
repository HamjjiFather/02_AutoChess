using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using KKSFramework.Base;
using UniRx;
using Zenject;

namespace AutoChess.Domain
{
    [UsedImplicitly]
    public class GameSystemManager : ManagerBase
    {
        public GameSystemManager()
        {
            GameSystemState = (Enum.GetValues(typeof(GameSystemType)) as GameSystemType[])!.Skip(1)
                .ToDictionary(x => x, _ => new BoolReactiveProperty(false));
        }
        
        public readonly Dictionary<GameSystemType, BoolReactiveProperty> GameSystemState;
        
        public void Initialize()
        {
        }
        
        
        
        /// <summary>
        /// 게임 시스템 해금.
        /// </summary>
        public void UnlockGameSystem(GameSystemType gameSystemType)
        {
            GameSystemState[gameSystemType].SetValueAndForceNotify(true);
        }


        /// <summary>
        /// 게임 시스템 해금 여부.
        /// </summary>
        public bool HasUnlockedGameSystem(GameSystemType gameSystemType) => GameSystemState[gameSystemType].Value;
    }
}