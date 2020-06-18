using System.Threading;
using KKSFramework.Fsm;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public enum BaseState
    {
        None,
        Idle,
        Attack,
        DelayAttack,
        Death,
    }
    
    public class BattleCharacterPrefab : MonoBehaviour
    {
        #region Fields & Property

        public Animator animator;

        public Image monsterImage;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private CharacterModel _characterModel;

        private FsmRunner _baseAttackFsm;

        private FsmRunner _stateFsm;

        #endregion


        #region UnityMethods

        #endregion
  

        #region Methods

        /// <summary>
        /// 캐릭터 데이터 세팅.
        /// </summary>
        public void SetCharacterData (CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }
        
        
        private async UniTask IdleState (CancellationTokenSource cts)
        {
            await UniTask.CompletedTask;
        }
        
        private async UniTask AttackState (CancellationTokenSource cts)
        {
            await UniTask.CompletedTask;
        }

        private async UniTask WaitAttackState (CancellationTokenSource cts)
        {
            // await UniTask.WaitUntil ()
            await UniTask.CompletedTask;
        }
        
        private async UniTask DeathState (CancellationTokenSource cts)
        {
            await UniTask.CompletedTask;
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}