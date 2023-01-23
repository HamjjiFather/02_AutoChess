using System.Linq;
using UnityEngine;

namespace AutoChess
{
    public readonly struct EquipmentProbabilityTable
    {
        public EquipmentProbabilityTable(int[] probabilities)
        {
            if (!probabilities.Sum().Equals(Constant.BaseProbabilityValue))
            {
                Debug.LogError($"확률의 합이 {Constant.BaseProbabilityValue}가 아니다. 확인 필요.");
                _probabilities = default;
                return;
            }

            _probabilities = probabilities.Select((p, i) => p + probabilities.Take(i).Sum()).ToArray();
        }

        #region Fields & Property

        private readonly int[] _probabilities;

        #endregion


        #region Methods

        #region Override

        public static EquipmentProbabilityTable operator +(EquipmentProbabilityTable a, EquipmentProbabilityTable b)
        {
            return new EquipmentProbabilityTable(a._probabilities.Zip(b._probabilities, (ap, bp) => ap + bp).ToArray());
        }

        #endregion


        #region This

        public EquipmentGradeType Chance(int prob)
        {
            for (var i = 0; i < _probabilities.Length; i++)
            {
                var chance = ProbabilityHelper.Chance(prob, _probabilities[i]);

                if (chance)
                    return EquipmentDefine.UsedEquipmentGradeTypes[i];
            }

            return EquipmentDefine.UsedEquipmentGradeTypes.First();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}