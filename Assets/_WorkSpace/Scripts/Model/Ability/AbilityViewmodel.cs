using System;

namespace AutoChess
{
    /// <summary>
    /// 캐릭터 능력치 등급.
    /// </summary>
    [Serializable]
    public class CharacterPrimeAbility
    {
        public const int MaxPrimeAbility = 99;
            
        /// <summary>
        /// 체력.
        /// </summary>
        public int Health;

        /// <summary>
        /// 힘.
        /// </summary>
        public int Strength;

        /// <summary>
        /// 정신력.
        /// </summary>
        public int Mentality;


        public CharacterPrimeAbility (int heath, int power, int mentality)
        {
        }
    }
    
    
    public class AbilityViewmodel : GameManagerBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}