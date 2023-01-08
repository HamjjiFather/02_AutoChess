using System.Linq;

namespace AutoChess
{
    public readonly struct EquipmentProbabilityTable
    {
        public EquipmentProbabilityTable(int[] probabilities)
        {
            if (!probabilities.Sum().Equals(Constant.BaseProbabilityValue))
            {
                Probabilities = default;
                return;
            }

            Probabilities = probabilities.Select((p, i) => p + probabilities.Take(i).Sum()).ToArray();
        }

        #region Fields & Property

        public readonly int[] Probabilities;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public EquipmentGradeType Chance(int prob)
        {
            for (var i = 0; i < Probabilities.Length; i++)
            {
                var chance = ProbabilityHelper.Chance(prob, Probabilities[i]);
                
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