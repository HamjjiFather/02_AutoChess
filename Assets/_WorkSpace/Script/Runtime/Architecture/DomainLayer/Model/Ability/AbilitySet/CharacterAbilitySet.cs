using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChess
{
    public class CharacterAbilitySet
    {
        public CharacterAbilitySet(IEnumerable<int> points)
        {
            primeAbilityModels =
                points.Select((x, i) =>
                    new PrimeAbilityPointModel((PrimePointType) i, x)).ToArray();
        }

        #region Fields & Property

        public PrimeAbilityPointModel[] primeAbilityModels;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void AddPrimePoint(PrimePointType primePointType, int amount)
        {
            primeAbilityModels[(int)primePointType].AddPrimePoint(amount);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}