using System.Collections.Generic;
using JetBrains.Annotations;
using MasterData;
using UnityEngine;

namespace AutoChess
{
    [UsedImplicitly]
    public class AbilityManager : GameManagerBase
    {
        #region Fields & Property

        /// <summary>
        /// 정적 능력치.
        /// </summary>
        private readonly Dictionary<string, AbilityTypeContainer> _staticAbilityTypeContainer =
            new Dictionary<string, AbilityTypeContainer> ();

        /// <summary>
        /// 동적 능력치.
        /// </summary>
        private readonly Dictionary<string, AbilityTypeContainer> _dynamicAbilityTypeContainer =
            new Dictionary<string, AbilityTypeContainer> ();

        #endregion


        #region Methods

        #region Override

        public override void Initialize ()
        {
            // throw new System.NotImplementedException ();
        }

        #endregion


        #region This

        /// <summary>
        /// 총 능력치를 리턴. 
        /// </summary>
        public double GetTotalAbilityValue (string ownerToString, AbilityType type)
        {
            var getAbility = _staticAbilityTypeContainer[ownerToString].GetNumberValues (type);
            return getAbility;
        }


        public void SetStaticAbilityValue (string ownerToString, string allocator, AbilitySet abilitySet)
        {
            if (!_staticAbilityTypeContainer.ContainsKey (ownerToString))
                _staticAbilityTypeContainer.Add (ownerToString, new AbilityTypeContainer ());

            _staticAbilityTypeContainer[ownerToString]
                .SetAbilityValue (abilitySet.AbilityType, allocator, abilitySet.AbilityValue);
        }


        public void AddStaticAbilityValue (string ownerToString, AbilityType type, string allocator,
            double value)
        {
            if (!_staticAbilityTypeContainer.ContainsKey (ownerToString))
                _staticAbilityTypeContainer.Add (ownerToString, new AbilityTypeContainer ());

            _staticAbilityTypeContainer[ownerToString].AddAbilityValue (type, allocator, value);
        }


        /// <summary>
        /// 해당 주인의 정적 능력치를 초기화함.
        /// </summary>
        public void RemoveStaticAbility (string ownerToString)
        {
            if (_staticAbilityTypeContainer.ContainsKey (ownerToString))
                _staticAbilityTypeContainer.Remove (ownerToString);
        }


        public AppliedGradeRange GetAppliedGradeRange (float[] abilityBounds)
        {
            var gradeIndex = ProbabilityHelper.GetAbilityGradeIndex ();
            var abilityGradeTable =
                TableDataHelper.Instance.GetBaseTableByEnum<AbilityGradeRange> (DataType.AbilityGrade, gradeIndex);

            var bound = Random.Range (abilityGradeTable.Min, abilityGradeTable.Max);
            var value = Mathf.Lerp (abilityBounds[0], abilityBounds[1], bound);

            return new AppliedGradeRange(gradeIndex, value);
        }

        #endregion

        #endregion
    }
}