using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class CharacterBundleSet : IBundleSet
    {
        public string uniqueIndexString;
        
        public int characterIndex;

        public int level;

        public int primeSkillLevel;

        public float[] primeAbilityGradeRatios;

        public int[] equippedGearUniqueIndexes;

        public int[] equippedSkillIndexes;

        public int[] skillLevels;

        public bool death;
    }
    
    
    [Serializable]
    public class CharacterBundle : BundleBase<CharacterBundleSet>
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override Dictionary<string, CharacterBundleSet> ToDictionaryLinq =>
            bundleSets.ToDictionary(x => x.uniqueIndexString, x => x);

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}