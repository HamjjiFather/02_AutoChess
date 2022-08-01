using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace AutoChess
{
    public enum BehaviourType
    {
        Idle,
        Attack,
        MoveTo
    }

    /// <summary>
    /// 전투 체력 세트.
    /// </summary>
    public struct BattleHealthSet
    {
        public float Shield;
        
        public float Now;

        public float Max;
        
        public float NowHealth
        {
            get => Now;
            set
            {
                if (value < 0)
                {
                    var remain = Shield - value;
                    Now -= remain;
                }
                else
                {
                    Now += value;
                }
                
                Now = Mathf.Clamp(Now, 0, Max);
            }
        }

        /// <summary>
        /// 현재 체력 비율.
        /// </summary>
        public float NowRatio => Now / Max;
        
        /// <summary>
        /// 잃은 체력.
        /// </summary>
        public float Lost => (Max - Now);

        /// <summary>
        /// 잃은 체력 비율.
        /// </summary>
        public float LostHealthRatio => Lost / Max;

        /// <summary>
        /// 사망 여부.
        /// </summary>
        public bool IsDead => Now <= 0;

        public BattleHealthSet SetHealth(float health)
        {
            NowHealth += health;
            return this;
        }

        public BattleHealthSet SetShield(float shield)
        {
            Shield += shield;
            return this;
        }
    }
    

    public class BattleCharacterBase
    {
        #region Fields & Property

        public ListPool<IStateEffect> StateEffects;

        public BattleHealthSet BattleHealthSet;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This
        
        
        public void DoBehavior()
        {
            var behaveType = CanBehaveType();
        }
        
        
        public BehaviourType CanBehaveType()
        {
            return BehaviourType.Attack;
        }
        
        
        #endregion


        #region Event

        #endregion

        #endregion

    }
}