using UnityEngine;

namespace AutoChess
{
    public class EquipmentDurability
    {
        public EquipmentDurability(int maxDurability)
        {
            MaxDurability = maxDurability;
            CurrentDurability = maxDurability;
        }

        public EquipmentDurability(int maxDurability, int currentDurability)
        {
            MaxDurability = maxDurability;
            CurrentDurability = currentDurability;
        }


        #region Fields & Property

        /// <summary>
        /// 최대 내구도.
        /// </summary>
        protected int MaxDurability;

        /// <summary>
        /// 현재 내구도.
        /// </summary>
        protected int CurrentDurability;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        /// <summary>
        /// 내구도가 충분한지?
        /// </summary>
        public bool EnoughDurability(int reqDurability) => CurrentDurability >= reqDurability;

        
        /// <summary>
        /// 내구도 손상.
        /// </summary>
        public void Damaged(int damagedDurability)
        {
            CurrentDurability = Mathf.Clamp(CurrentDurability - damagedDurability, 0, MaxDurability);
        }


        /// <summary>
        /// 최대 내구도 변경.
        /// </summary>
        public void ResetMaxDurability(int maxDurability)
        {
            MaxDurability = maxDurability;
            CurrentDurability = Mathf.Min(CurrentDurability, MaxDurability);
        }

        
        /// <summary>
        /// 내구도 회복.
        /// </summary>
        public void RecoverDurability()
        {
            CurrentDurability = MaxDurability;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}