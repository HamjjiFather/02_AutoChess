using System.Linq;
using UnityEngine;

namespace HexaPuzzle
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


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}