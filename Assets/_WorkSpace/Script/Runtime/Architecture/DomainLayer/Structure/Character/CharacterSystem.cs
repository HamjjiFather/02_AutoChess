using System.Linq;
using KKSFramework;
using UnityEngine;

namespace AutoChess
{
    public static class CharacterDefine
    {
        public const int MaxCharacterLevel = 30;
    }

    /// <summary>
    /// 아군 캐릭터 생성기.
    /// </summary>
    public static class CharacterGenerator
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public static CharacterBase GenerateCharacter(CharacterGenerateParameter generateParameter)
        {
            var characterTd = TableDataManager.Instance.PlayableCharacterDict.Values.Choice();
            var level = Random.Range(generateParameter.StartLevelRange.Item1, generateParameter.StartLevelRange.Item2);
            var characterBase =
                new CharacterBase(characterTd, Enumerable.Repeat(1, 5), Enumerable.Repeat(1, 5), level, 0);
            return characterBase;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}