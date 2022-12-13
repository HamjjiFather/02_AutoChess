using System.Threading;
using AutoChess.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AutoChess
{
    public class HealthContainer
    {
        public HealthContainer(int maxHealth)
        {
            MaxHealth = maxHealth;
            NowHealth = MaxHealth;
        }

        public int MaxHealth { get; private set; }

        public int NowHealth { get; private set; }

        public float NowHealthRatio => Mathf.Clamp((float)NowHealth / MaxHealth, 0, 1);

        public float LoseHealthRatio => 1 - NowHealthRatio;

        public float MaxHealthPerValue(int percentValue) => MaxHealth * (float)percentValue / Constant.BasePercentValue;

        public int VariateHealth(int health)
        {
            NowHealth += health;
            return NowHealth;
        }

        /// <summary>
        /// 사망.
        /// </summary>
        public bool Death => NowHealthRatio <= 0;
    }
    
    
    /// <summary>
    /// 전투원 데이터 모델.
    /// </summary>
    public class BattleInteractableUnit : IBattleStateReceiver
    {
        public BattleInteractableUnit(BattleSideType sideType, CharacterUnit characterUnit)
        {
            BattleSideType = sideType;
            CharacterUnit = characterUnit;
            HealthContainer = new HealthContainer(CharacterUnit.GetAbilityValue(SubAbilityType.Health).FloatToInt());
            
            BattleFsmRunner = new BattleFsmRunner();
            BattleFsmRunner.RegistFsmState(BehaviourState.FindTarget, FindTargetFsmStateAsync);
            BattleFsmRunner.RegistFsmState(BehaviourState.CheckInteractionArea, CheckInteractionAreaFsmStateAsync);
            BattleFsmRunner.RegistFsmState(BehaviourState.FindInteractableArea, FindInteractableAreaFsmStateAsync);
            BattleFsmRunner.RegistFsmState(BehaviourState.DoInteract, DoInteractFsmStateAsync);
            BattleFsmRunner.RegistFsmState(BehaviourState.DoMove, DoMoveFsmStateAsync);
            BattleFsmRunner.RegistFsmState(BehaviourState.WaitForNextBehaviour, WaitForNextBehaviourStateAsync);

            BattleStatus = new BattleStatus();
        }

        #region Fields & Property

        public BattleSideType BattleSideType;

        public CharacterUnit CharacterUnit;

        public HealthContainer HealthContainer;

        public BattleFsmRunner BattleFsmRunner;

        public BattleStatus BattleStatus;

        public bool Death => HealthContainer.Death;
        
        #endregion


        #region Methods

        #region Override

        public void StartBattle()
        {
            BattleFsmRunner.StartFsm(BehaviourState.FindTarget);
        }

        
        public void EndBattle()
        {
            
        }

        #endregion


        #region This

        #endregion


        #region Fsm

        public UniTask FindTargetFsmStateAsync(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }
        
        
        public UniTask CheckInteractionAreaFsmStateAsync(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }
        
        
        public UniTask FindInteractableAreaFsmStateAsync(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }
        
        
        public UniTask DoInteractFsmStateAsync(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }
        
        
        public UniTask DoMoveFsmStateAsync(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }
        
        
        public UniTask WaitForNextBehaviourStateAsync(CancellationTokenSource cts)
        {
            return UniTask.CompletedTask;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}