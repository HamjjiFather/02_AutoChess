using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UnityEngine;

namespace AutoChess
{
    /// <summary>
    /// 확률에 대한 계산.
    /// </summary>
    public static class ProbabilityHelper
    {
        #region Fields & Property

        /// <summary>
        /// 능력치 등급당 부여될 확률.
        /// </summary>
        public static readonly int[] ProbPerAbilityGrade = {
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
        
        
        public static int RandomValue => Random.Range(0, Constant.BaseProbabilityValue) + 1;


        public static bool Chance(int prob) => Chance(RandomValue, prob);


        public static bool Chance(int rand, int prob) => rand <= prob;


        public static int GetAbilityGradeIndex ()
        {
            var prob = 0;
            var rand = new System.Random ().Next (Constant.BaseProbabilityValue) + 1;
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


        /// <summary>
        /// 확률에 의한 아이템 획득 - 다중 아이템 획득 가능.
        /// </summary>
        public static IEnumerable<int> GetItemsForAll(int[] probabilities)
        {
            var pick = probabilities.Select((p, i) => (p, i)).Where(tp => Chance(tp.p)).Select(tp => tp.i);
            return pick;
        }
        
        
        /// <summary>
        /// 확률에 의한 아이템 획득 - 단일 아이템 획득 가능.
        /// </summary>
        public static int GetItemsOnlyOnce(int[] probabilities)
        {
            if (probabilities.Sum() == Constant.BaseProbabilityValue)
                return Constant.InvalidIndex;

            var i = 0;
            var rand = RandomValue;
            var getEnum = probabilities.GetEnumerator();
            while (getEnum.MoveNext())
            {
                var prob = getEnum.Current;
                if (Chance(rand, (int)prob!))
                {
                    return i;
                }

                i++;
            }

            return Constant.InvalidIndex;
        }
        

        #endregion


        #region Event

        #endregion

        #endregion
    }
}