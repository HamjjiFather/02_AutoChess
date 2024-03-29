﻿using System.Collections.Generic;
using System.Linq;

namespace AutoChess
{
    public class PrimeAbilityContainer : IGetSubAbility
    {
        public PrimeAbilityContainer(IEnumerable<int> baseValues, IEnumerable<int> investedValues)
        {
            PrimeAbilities = baseValues
                .Zip(investedValues, (bv, iv) => (bv, iv))
                .Select((tp, i) => (tp.bv, tp.iv, i))
                .ToDictionary(tp => (PrimeAbilityType) tp.i, tp => GetPrimeAbility(tp.i, tp.bv, tp.iv));

            IPrimeAbility GetPrimeAbility(int index, int baseValue, int investedValue)
            {
                switch (index)
                {
                    case 0:
                        return new BodyAbility(baseValue, investedValue);

                    case 1:
                        return new MentalityAbility(baseValue, investedValue);

                    case 2:
                        return new SkillAbility(baseValue, investedValue);

                    case 3:
                        return new SpeedAbility(baseValue, investedValue);

                    case 4:
                        return new WisdomAbility(baseValue, investedValue);

                    default:
                        goto case 0;
                }
            }
        }

        #region Fields & Property

        /// <summary>
        /// 주요 능력치들.
        /// </summary>
        public readonly Dictionary<PrimeAbilityType, IPrimeAbility> PrimeAbilities;

        #endregion


        #region Methods

        #region Override

        public int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            var sum = PrimeAbilities.Values.Sum(pa => pa.GetSubAbilityValue(subAbilityType));
            return sum;
        }

        #endregion


        #region This

        public void InvestPoint(PrimeAbilityType primeAbility, int value)
        {
            PrimeAbilities[primeAbility].InvestedValue += value;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}