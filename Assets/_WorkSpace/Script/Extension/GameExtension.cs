using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoChess
{
    public static class GameExtension
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        public static CharacterLevel GetCharacterLevel (float exp)
        {
            return TableDataManager.Instance.CharacterLevelDict.Values.FirstOrDefault (x => x.AccReqExp > exp);
        }

        
        public static CharacterLevel GetCharacterLevel (int level)
        {
            return TableDataManager.Instance.CharacterLevelDict[(int) DataType.CharacterLevel + level];
        }

        
        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}