using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

namespace AutoChess
{
    /// <summary>
    /// 확률에 대한 계산.
    /// </summary>
    public static class ProbabilityHelper
    {
        #region Fields & Property

        public static readonly ObscuredInt BaseProbabilityValue = 10000;

        /// <summary>
        /// 능력치 등급당 부여될 확률.
        /// </summary>
        public static readonly ObscuredInt[] ProbPerAbilityGrade = {
            1000,
            1250,
            1800,
            2400,
            1600,
            800,
            650,
            500
        };

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public static bool Chance (ObscuredFloat prob)
        {
            ObscuredFloat rand = Random.Range (0, BaseProbabilityValue);
            return rand <= prob;
        }


        public static int GetAbilityGradeIndex ()
        {
            var prob = 0;
            var rand = new System.Random ().Next (BaseProbabilityValue);
            for (var i = 0; i < ProbPerAbilityGrade.Length; i++)
            {
                prob += ProbPerAbilityGrade[i];
                
                if (prob + ProbPerAbilityGrade[i] < rand)
                {
                    return i;
                }
            }

            return ProbPerAbilityGrade.Length - 1;
        }
        

        #endregion


        #region Event

        #endregion

        #endregion
    }
}