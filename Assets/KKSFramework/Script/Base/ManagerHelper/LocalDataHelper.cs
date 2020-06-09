using System;
using System.Collections.Generic;

namespace KKSFramework.LocalData
{
    [Serializable]
    public class GameBundle : Bundle
    {
        public int LastCharacterUniqueId;
    }
    
    
    [Serializable]
    public class SpecialPuzzleBundle : Bundle
    {
        public List<int> Exps = new List<int> ();
    }

    [Serializable]
    public class CharacterBundle : Bundle
    {
        public List<int> CharacterUniqueIds = new List<int> ();
        
        public List<int> CharacterIds = new List<int> ();

        public List<int> CharacterExps = new List<int> ();
    }

    [Serializable]
    public class StageBundle : Bundle
    {
        public int StageIndex;
    }
    
    public static class LocalDataHelper
    {
        private static readonly LocalData LocalDataClass = new LocalData();

        #region Load

        /// <summary>
        /// 게임 데이터 로드.
        /// </summary>
        public static void LoadAllGameData()
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
        public static void SaveAllGameData()
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


        public static void SaveCharacterIdData (List<int> characterUIds, List<int> characterIds, List<int> characterExps)
        {
            LocalDataClass.CharacterBundle.CharacterUniqueIds = characterUIds;
            LocalDataClass.CharacterBundle.CharacterIds = characterIds;
            LocalDataClass.CharacterBundle.CharacterExps = characterExps;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.CharacterBundle);
        }


        #endregion

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