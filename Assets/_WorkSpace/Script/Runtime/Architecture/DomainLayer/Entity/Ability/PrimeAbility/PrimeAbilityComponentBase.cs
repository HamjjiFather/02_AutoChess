﻿using System.Collections.Generic;

namespace AutoChess
{
    /// <summary>
    /// 주요 능력치.
    /// </summary>
    public enum PrimeAbilityType
    {
        /// <summary>
        /// 신체.
        /// </summary>
        Body,
        
        /// <summary>
        /// 기술.
        /// </summary>
        Skill,
        
        /// <summary>
        /// 정신.
        /// </summary>
        Meltality,
        
        /// <summary>
        /// 지혜.
        /// </summary>
        Wisdom,
        
        /// <summary>
        /// 속도.
        /// </summary>
        Speed
    }
    
    public abstract class PrimeAbilityBase : IPrimeAbility
    {
        protected PrimeAbilityBase(int value, int investedValue)
        {
            BaseValue = value;
            InvestedValue = investedValue;
        }

        #region Fields & Property
        
        #endregion


        #region Methods

        #region Override
        
        /// <summary>
        /// 기본 주요 능력치.
        /// </summary>
        public int BaseValue { get; set; }

        /// <summary>
        /// 투자된 주요 능력치.
        /// </summary>
        public int InvestedValue { get; set; }

        /// <summary>
        /// 주요 능력치 타입.
        /// </summary>
        public PrimeAbilityType PrimeAbilityType => PrimeAbilityType.Body;

        /// <summary>
        /// 이 주요 능력치로 변동되는 보조 능력치 타입들.
        /// </summary>
        public abstract SubAbilityType[] SubAbilityTypes { get; set; }

        /// <summary>
        /// 보조 능력치 리턴.
        /// </summary>
        public abstract int GetSubAbilityValue(SubAbilityType subAbilityType);

        #endregion


        #region This
        
        /// <summary>
        /// 총 주요 능력치(기본 + 투자됨)
        /// </summary>
        public int TotalValue => BaseValue + InvestedValue;

        #endregion


        #region Event

        #endregion

        #endregion

    }
}