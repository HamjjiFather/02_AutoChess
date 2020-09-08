using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace KKSFramework.LocalData
{
    [Serializable]
    public class GameBundle : Bundle
    {
        public int LastCharacterUniqueId;

        public int LastEquipmentUniqueId;
    }


    [Serializable]
    public class CharacterBundle : Bundle
    {
        public List<CharacterStatusGrade> CharacterStatusGrades = new List<CharacterStatusGrade> ();

        public List<int> CharacterUniqueIds = new List<int> ();

        public List<int> CharacterIds = new List<int> ();

        public List<int> CharacterExps = new List<int> ();

        public List<CharacterEquipmentUIds> EquipmentUIds = new List<CharacterEquipmentUIds> ();

        public List<int> BattleCharacterUniqueIds = new List<int> ();

        [Serializable]
        public class CharacterEquipmentUIds
        {
            public List<int> EquipmentUIds = new List<int> ();

            public CharacterEquipmentUIds (IEnumerable<int> uids)
            {
                EquipmentUIds = uids.ToList ();
            }
        }

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


            public CharacterStatusGrade (float healthStatusGrade, float attackStatusGrade,
                float abilityPointStatusGrade, float defenseStatusGrade)
            {
                HealthStatusGrade = healthStatusGrade;
                AttackStatusGrade = attackStatusGrade;
                AbilityPointStatusGrade = abilityPointStatusGrade;
                DefenseStatusGrade = defenseStatusGrade;
            }
        }
    }


    [Serializable]
    public class BattleCharacterPositionBundle : Bundle
    {
        public StringReactiveProperty BattleCharacterPositions =
            new StringReactiveProperty (Constant.PlayerCharacterPosition);
    }


    [Serializable]
    public class EquipmentBundle : Bundle
    {
        public List<int> EquipmentUniqueIds = new List<int> ();

        public List<int> EquipmentIds = new List<int> ();

        public List<StarGrade> EquipmentGrades = new List<StarGrade> ();

        public List<EquipmentData> EquipmentDatas = new List<EquipmentData> ();


        [Serializable]
        public class EquipmentData
        {
            public List<int> EquipmentStatusIndexes = new List<int> ();

            public List<float> EquipmentStatusGrades = new List<float> ();
        }
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
            var characterBundle =
                LocalDataManager.Instance.LoadGameData<CharacterBundle> (LocalDataClass.CharacterBundle);
            LocalDataClass.CharacterBundle = characterBundle;

            var battleCharacterPositionBundle =
                LocalDataManager.Instance.LoadGameData<BattleCharacterPositionBundle> (LocalDataClass
                    .BattleCharacterPositionBundle);
            LocalDataClass.BattleCharacterPositionBundle = battleCharacterPositionBundle;
            LocalDataClass.BattleCharacterPositionBundle.BattleCharacterPositions.Subscribe (value =>
            {
                LocalDataManager.Instance.SaveGameData (LocalDataClass.BattleCharacterPositionBundle);
            });

            var equipmentBundle =
                LocalDataManager.Instance.LoadGameData<EquipmentBundle> (LocalDataClass.EquipmentBundle);
            LocalDataClass.EquipmentBundle = equipmentBundle;

            var gameBundle = LocalDataManager.Instance.LoadGameData<GameBundle> (LocalDataClass.GameBundle);
            LocalDataClass.GameBundle = gameBundle;
        }


        public static CharacterBundle GetCharacterBundle ()
        {
            return LocalDataClass.CharacterBundle;
        }
        
        public static BattleCharacterPositionBundle GetBattleCharacterPositionBundle ()
        {
            return LocalDataClass.BattleCharacterPositionBundle;
        }
        
        public static string GetBattleCharacterPosition ()
        {
            return LocalDataClass.BattleCharacterPositionBundle.BattleCharacterPositions.Value;
        }
        

        public static EquipmentBundle GetEquipmentBundle ()
        {
            return LocalDataClass.EquipmentBundle;
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
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
            LocalDataManager.Instance.SaveGameData (LocalDataClass.EquipmentBundle);
            LocalDataManager.Instance.SaveGameData (LocalDataClass.GameBundle);
        }


        public static void SaveCharacterUniqueIdData (int uniqueId)
        {
            LocalDataClass.GameBundle.LastCharacterUniqueId = uniqueId;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.GameBundle);
        }


        public static void SaveCharacterIdData (List<int> characterUids, List<int> characterIds,
            List<int> characterExps, List<CharacterBundle.CharacterEquipmentUIds> equipmentUid)
        {
            LocalDataClass.CharacterBundle.CharacterUniqueIds = characterUids;
            LocalDataClass.CharacterBundle.CharacterIds = characterIds;
            LocalDataClass.CharacterBundle.CharacterExps = characterExps;
            LocalDataClass.CharacterBundle.EquipmentUIds = equipmentUid;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveCharacterExpData (List<int> characterExps)
        {
            LocalDataClass.CharacterBundle.CharacterExps = characterExps;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveCharacterStatusGradeData (List<float> hp, List<float> attack, List<float> ap,
            List<float> defense)
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


        public static void SaveBattleCharacterPositionData (IEnumerable<string> characterPositions)
        {
            var saveResult = string.Join ("/", characterPositions);
            GetBattleCharacterPositionBundle ().BattleCharacterPositions.Value = saveResult;
        }


        public static void SaveEquipmentUniqueIdData (int uniqueId)
        {
            LocalDataClass.GameBundle.LastEquipmentUniqueId = uniqueId;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.GameBundle);
        }


        public static void SaveEquipmentStatusData (List<int> equipmentUids, List<int> equipmentIds,
            List<StarGrade> equipmentGrades,
            List<List<int>> indexes, List<List<float>> statusGrades)
        {
            LocalDataClass.EquipmentBundle.EquipmentUniqueIds = equipmentUids;
            LocalDataClass.EquipmentBundle.EquipmentIds = equipmentIds;
            LocalDataClass.EquipmentBundle.EquipmentGrades = equipmentGrades;
            LocalDataClass.EquipmentBundle.EquipmentDatas.Clear ();

            for (var i = 0; i < equipmentUids.Count; i++)
            {
                var equipmentData = new EquipmentBundle.EquipmentData
                {
                    EquipmentStatusIndexes = indexes[i], EquipmentStatusGrades = statusGrades[i]
                };

                LocalDataClass.EquipmentBundle.EquipmentDatas.Add (equipmentData);
            }
        }

        #endregion


        public static void DeleteData ()
        {
            LocalDataManager.Instance.DeleteData ();
        }


        [Serializable]
        public class LocalData
        {
            public GameBundle GameBundle = new GameBundle ();

            public CharacterBundle CharacterBundle = new CharacterBundle ();

            public BattleCharacterPositionBundle BattleCharacterPositionBundle = new BattleCharacterPositionBundle ();

            public EquipmentBundle EquipmentBundle = new EquipmentBundle ();
        }
    }
}