using System;
using System.Collections.Generic;

namespace KKSFramework.LocalData
{
    [Serializable]
    public class GameBundle : Bundle
    {
        public int LastCharacterUniqueId;

        public int LastEquipmentUniqueId;
    }


    [Serializable]
    public class SpecialPuzzleBundle : Bundle
    {
        public List<int> Exps = new List<int> ();
    }

    [Serializable]
    public class CharacterBundle : Bundle
    {
        public List<CharacterStatusGrade> CharacterStatusGrades = new List<CharacterStatusGrade> ();

        public List<int> CharacterUniqueIds = new List<int> ();

        public List<int> CharacterIds = new List<int> ();

        public List<int> CharacterExps = new List<int> ();

        public List<int> EquipmentUIds = new List<int> ();

        public List<int> BattleCharacterUniqueIds = new List<int> ();

        [Serializable]
        public class CharacterStatusGrade
        {
            public float HealthStatusGrade;

            public float AttackStatusGrade;

            public float AbilityPointStatusGrade;

            public float DefenseStatusGrade;


            public CharacterStatusGrade ()
            {
            }


            public CharacterStatusGrade (float healthStatusGrade, float attackStatusGrade, float abilityPointStatusGrade, float defenseStatusGrade)
            {
                HealthStatusGrade = healthStatusGrade;
                AttackStatusGrade = attackStatusGrade;
                AbilityPointStatusGrade = abilityPointStatusGrade;
                DefenseStatusGrade = defenseStatusGrade;
            }
        }
    }

    [Serializable]
    public class EquipmentBundle : Bundle
    {
        public List<int> EquipmentUniqueIds = new List<int> ();
    }

    [Serializable]
    public class StageBundle : Bundle
    {
        public int StageIndex;
    }

    public static class LocalDataHelper
    {
        private static readonly LocalData LocalDataClass = new LocalData ();


        #region Load

        /// <summary>
        /// 게임 데이터 로드.
        /// </summary>
        public static void LoadAllGameData ()
        {
            var specialPuzzleBundle =
                LocalDataManager.Instance.LoadGameData<SpecialPuzzleBundle> (LocalDataClass.SpecialPuzzleBundle);
            LocalDataClass.SpecialPuzzleBundle = specialPuzzleBundle;

            var characterBundle =
                LocalDataManager.Instance.LoadGameData<CharacterBundle> (LocalDataClass.CharacterBundle);
            LocalDataClass.CharacterBundle = characterBundle;

            var gameBundle = LocalDataManager.Instance.LoadGameData<GameBundle> (LocalDataClass.GameBundle);
            LocalDataClass.GameBundle = gameBundle;
        }


        public static SpecialPuzzleBundle GetSpecialPuzzleBundle ()
        {
            return LocalDataClass.SpecialPuzzleBundle;
        }

        public static CharacterBundle GetCharacterBundle ()
        {
            return LocalDataClass.CharacterBundle;
        }

        public static GameBundle GetGameBundle ()
        {
            return LocalDataClass.GameBundle;
        }

        #endregion


        #region Save

        /// <summary>
        /// 게임 데이터 저장.
        /// </summary>
        public static void SaveAllGameData ()
        {
            LocalDataManager.Instance.SaveGameData (LocalDataClass.SpecialPuzzleBundle);
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveGameUniqueIdData (int uniqueId)
        {
            LocalDataClass.GameBundle.LastCharacterUniqueId = uniqueId;
        }


        public static void SaveSpecialPuzzleData (List<int> exps)
        {
            LocalDataClass.SpecialPuzzleBundle.Exps = exps;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.SpecialPuzzleBundle);
        }


        public static void SaveCharacterIdData (List<int> characterUids, List<int> characterIds,
            List<int> characterExps, List<int> equipmentUid)
        {
            LocalDataClass.CharacterBundle.CharacterUniqueIds = characterUids;
            LocalDataClass.CharacterBundle.CharacterIds = characterIds;
            LocalDataClass.CharacterBundle.CharacterExps = characterExps;
            LocalDataClass.CharacterBundle.EquipmentUIds = equipmentUid;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveCharacterStatusGradeData (List<float> hp, List<float> attack, List<float> ap, List<float> defense)
        {
            for (var i = 0; i < hp.Count; i++)
            {
                LocalDataClass.CharacterBundle.CharacterStatusGrades.Add (
                    new CharacterBundle.CharacterStatusGrade (hp[i], attack[i], ap[i], defense[i]));
            }
        }


        public static void SaveBattleCharacterUidData (List<int> characterUids)
        {
            LocalDataClass.CharacterBundle.BattleCharacterUniqueIds = characterUids;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }

        #endregion


        public static void DeleteData ()
        {
            LocalDataManager.Instance.DeleteData ();
        }

        /// <summary>
        /// 게임 데이터 클래스.
        /// </summary>
        [Serializable]
        public class LocalData
        {
            public GameBundle GameBundle = new GameBundle ();

            public SpecialPuzzleBundle SpecialPuzzleBundle = new SpecialPuzzleBundle ();

            public CharacterBundle CharacterBundle = new CharacterBundle ();
        }
    }
}