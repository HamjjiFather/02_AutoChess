using System;
using System.Collections.Generic;

namespace KKSFramework.LocalData
{
    [Serializable]
    public class SpecialPuzzleBundle : Bundle
    {
        public List<int> Exps = new List<int> ();
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
            var loadedData =
                LocalDataManager.Instance.LoadGameData<SpecialPuzzleBundle> (LocalDataClass.SpecialPuzzleBundle);
            LocalDataClass.SpecialPuzzleBundle = loadedData;
        }


        public static SpecialPuzzleBundle GetSpecialPuzzleBundle ()
        {
            return LocalDataClass.SpecialPuzzleBundle;
        }
        

        #endregion


        #region Save

        /// <summary>
        /// 게임 데이터 저장.
        /// </summary>
        public static void SaveAllGameData()
        {
            LocalDataManager.Instance.SaveGameData (LocalDataClass.SpecialPuzzleBundle);
        }


        public static void SaveSpecialPuzzleData (List<int> exps)
        {
            LocalDataClass.SpecialPuzzleBundle.Exps = exps;
            LocalDataManager.Instance.SaveGameData (LocalDataClass.SpecialPuzzleBundle);
        }


        #endregion

        /// <summary>
        /// 게임 데이터 클래스.
        /// </summary>
        [Serializable]
        public class LocalData
        {
            public SpecialPuzzleBundle SpecialPuzzleBundle = new SpecialPuzzleBundle ();
        }
    }
}