using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace KKSFramework.LocalData
{
    [Serializable]
    public class GameBundle : Bundle
    {
        public int LastCharacterUniqueId;

        public int LastEquipmentUniqueId;
    }


    [Serializable]
    public class PlayerBundle : Bundle
    {
        public int Level;

        public int Exp;
    }


    [Serializable]
    public class CharacterBundle : Bundle
    {
        public List<CharacterAbilityGrade> CharacterAbilityGrades = new List<CharacterAbilityGrade> ();

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

        /// <summary>
        /// 캐릭터 능력치 등급.
        /// </summary>
        [Serializable]
        public class CharacterAbilityGrade
        {
            public float HealthAbilityGrade;

            public float AttackDamageAbilityGrade;

            public float SpellDamageAbilityGrade;

            public float DefenseAbilityGrade;


            public CharacterAbilityGrade ()
            {
                HealthAbilityGrade = UnityEngine.Random.Range (0, 1f);
                AttackDamageAbilityGrade = UnityEngine.Random.Range (0, 1f);
                SpellDamageAbilityGrade = UnityEngine.Random.Range (0, 1f);
                DefenseAbilityGrade = UnityEngine.Random.Range (0, 1f);
            }


            public CharacterAbilityGrade (float healthAbilityGrade, float attackDamageAbilityGrade,
                float spellDamageAbilityGrade, float defenseAbilityGrade)
            {
                HealthAbilityGrade = healthAbilityGrade;
                AttackDamageAbilityGrade = attackDamageAbilityGrade;
                SpellDamageAbilityGrade = spellDamageAbilityGrade;
                DefenseAbilityGrade = defenseAbilityGrade;
            }
        }
    }
    
    


    [Serializable]
    public class BattleCharacterPositionBundle : Bundle
    {
        public const string PLAYER_CHARACTER_POSITION = "1,0/2,0/3,0/4,0/5,0";

        
        public StringReactiveProperty BattleCharacterPositions =
            new StringReactiveProperty (PLAYER_CHARACTER_POSITION);
    }


    [Serializable]
    public class EquipmentBundle : Bundle
    {
        public List<int> EquipmentUniqueIds = new List<int> ();

        public List<int> EquipmentIds = new List<int> ();

        public List<EquipmentGrade> EquipmentGrades = new List<EquipmentGrade> ();

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
            LocalDataProjectManager.Instance.LoadGameData (LocalDataClass.PlayerBundle);
            LocalDataProjectManager.Instance.LoadGameData (LocalDataClass.CharacterBundle);
            LocalDataProjectManager.Instance.LoadGameData (LocalDataClass
                .BattleCharacterPositionBundle);
            LocalDataClass.BattleCharacterPositionBundle.BattleCharacterPositions.Subscribe (value =>
            {
                LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.BattleCharacterPositionBundle);
            });

            LocalDataProjectManager.Instance.LoadGameData (LocalDataClass.EquipmentBundle);
            LocalDataProjectManager.Instance.LoadGameData (LocalDataClass.GameBundle);
        }


        public static PlayerBundle GetPlayerBundle ()
        {
            return LocalDataClass.PlayerBundle;
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
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.EquipmentBundle);
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.GameBundle);
        }


        public static void SaveCharacterUniqueIdData (int uniqueId)
        {
            LocalDataClass.GameBundle.LastCharacterUniqueId = uniqueId;
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.GameBundle);
        }


        public static void SaveCharacterIdData (List<int> characterUids, List<int> characterIds,
            List<int> characterExps, List<CharacterBundle.CharacterEquipmentUIds> equipmentUid)
        {
            LocalDataClass.CharacterBundle.CharacterUniqueIds = characterUids;
            LocalDataClass.CharacterBundle.CharacterIds = characterIds;
            LocalDataClass.CharacterBundle.CharacterExps = characterExps;
            LocalDataClass.CharacterBundle.EquipmentUIds = equipmentUid;
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveCharacterExpData (List<int> characterExps)
        {
            LocalDataClass.CharacterBundle.CharacterExps = characterExps;
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveCharacterStatusGradeData (List<float> hp, List<float> attack, List<float> ap,
            List<float> defense)
        {
            LocalDataClass.CharacterBundle.CharacterAbilityGrades.Clear ();
            for (var i = 0; i < hp.Count; i++)
            {
                LocalDataClass.CharacterBundle.CharacterAbilityGrades.Add (
                    new CharacterBundle.CharacterAbilityGrade (hp[i], attack[i], ap[i], defense[i]));
            }

            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveBattleCharacterUidData (List<int> characterUids)
        {
            LocalDataClass.CharacterBundle.BattleCharacterUniqueIds = characterUids;
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        public static void SaveBattleCharacterPositionData (IEnumerable<string> characterPositions)
        {
            var saveResult = string.Join ("/", characterPositions);
            GetBattleCharacterPositionBundle ().BattleCharacterPositions.Value = saveResult;
        }


        public static void SaveEquipmentUniqueIdData (int uniqueId)
        {
            LocalDataClass.GameBundle.LastEquipmentUniqueId = uniqueId;
            LocalDataProjectManager.Instance.SaveGameData (LocalDataClass.GameBundle);
        }


        public static void SaveEquipmentStatusData (List<int> equipmentUids, List<int> equipmentIds,
            List<EquipmentGrade> equipmentGrades,
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
            LocalDataProjectManager.Instance.DeleteData ();
        }


        [Serializable]
        public class LocalData
        {
            public GameBundle GameBundle = new GameBundle ();

            public PlayerBundle PlayerBundle = new PlayerBundle ();

            public CharacterBundle CharacterBundle = new CharacterBundle ();

            public BattleCharacterPositionBundle BattleCharacterPositionBundle = new BattleCharacterPositionBundle ();

            public EquipmentBundle EquipmentBundle = new EquipmentBundle ();
        }
    }
}