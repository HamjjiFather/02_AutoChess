using System;
using System.Collections.Generic;

namespace AutoChess
{
    public struct AppliedGradeRange
    {
        public AppliedGradeRange (int gradeRangeIndex, double appliedValue)
        {
            this.gradeRangeIndex = gradeRangeIndex;
            this.appliedValue = appliedValue;
        }

        public int gradeRangeIndex;

        public double appliedValue;
    }
    
    [Serializable]
    public class PlayableCharacterAbilityContainer : AbilityTypeContainer
    {
        #region Fields & Property

        private Dictionary<AbilityType, AppliedGradeRange> _appliedGradeRange = new Dictionary<AbilityType, AppliedGradeRange> ();

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void AddAbilityValue (AbilityType abilityType, string allocatorToString, AppliedGradeRange appliedGradeRange)
        {
            _appliedGradeRange.Add (abilityType, appliedGradeRange);
            base.AddAbilityValue (abilityType, allocatorToString, appliedGradeRange.appliedValue);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}