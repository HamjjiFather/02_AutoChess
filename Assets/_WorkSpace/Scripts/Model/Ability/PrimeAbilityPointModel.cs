using System.Linq;
using CodeStage.AntiCheat.ObscuredTypes;
using KKSFramework;

namespace AutoChess
{
    public class PrimeAbilityPointModel
    {
        public PrimeAbilityPointModel(PrimePointType primePointType, int pointValue)
        {
            PointValue = pointValue;

            AdditionalAbilities = AbilityDefines.AbilityPerPrimePoint[primePointType].Select(at =>
            {
                var abilityValue = PointValue * AbilityDefines.valuePerPrimePoint[at];
                return new AbilityModelBase(at, abilityValue);
            }).ToArray();
        }

        #region Fields & Property

        /// <summary>
        /// 주요 능력치로 변동이 되는 부가 능력치들.
        /// </summary>
        public readonly AbilityModelBase[] AdditionalAbilities;

        /// <summary>
        /// 포인트.
        /// </summary>
        public ObscuredInt PointValue;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void AddPrimePoint(int abilityValue)
        {
            PointValue = abilityValue;
            AdditionalAbilities.Foreach(aa =>
                aa.AbilityValue = PointValue * AbilityDefines.valuePerPrimePoint[aa.AdditionalAbilityType]);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}