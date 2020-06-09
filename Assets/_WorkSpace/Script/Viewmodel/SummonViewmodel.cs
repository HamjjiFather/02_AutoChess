using System.Linq;
using KKSFramework.DesignPattern;
using UnityEngine;

namespace HexaPuzzle
{
    public class SummonViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        public override void Initialize ()
        {
        }


        #region Methods

        public void Summon (float value)
        {
            var probability =
                TableDataManager.Instance.ProbabilityRangeDict.Values.FirstOrDefault (x =>
                    x.Min <= value && x.Max > value);

            if (probability == null)
                return;

            probability = TableDataManager.Instance.ProbabilityRangeDict.Values.Last ();

            var characterProbTable = TableDataManager.Instance.CharacterProbabilityDict[probability.CharProbIdx];

            var randomValue = Random.Range (0, 100);

            var grade = Grade.None;

            for (var i = 0; i < characterProbTable.Probs.Length; i++)
            {
                if (randomValue >= characterProbTable.Probs[i]) continue;
                grade = (Grade) i;
                break;
            }

            var character = TableDataManager.Instance.CharacterDict.Values.Where (x => x.StartGrade == grade)
                .RandomSource ();

            Debug.Log (value + "//" + randomValue + "//" +
                       TableDataManager.Instance.GlobalTextDict[character.Name].GlobalTexts[0]);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}