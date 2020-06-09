using System.Collections.Generic;
using System.Linq;
using KKSFramework.LocalData;

namespace HexaPuzzle
{
    public partial class PuzzleViewmodel
    {
        #region Fields & Property

        public Dictionary<PuzzleMatchingType, SpecialPuzzleModel> AllSpecialPuzzles =
            new Dictionary<PuzzleMatchingType, SpecialPuzzleModel> ();


#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
        
        
        public override void InitTableData ()
        {
            base.InitTableData ();
            AllSpecialPuzzles = TableDataManager.Instance.SpecialPuzzleDict
                .ToDictionary (x => x.Value.PuzzleMatchingType, x => new SpecialPuzzleModel (true, x.Value));
        }

        
        public override void InitLocalData ()
        {
            var savedData = LocalDataHelper.GetSpecialPuzzleBundle ();
            
            savedData.Exps.Foreach ((exp, index) =>
            {
                AllSpecialPuzzles.Values.ToList ()[index].Exp.Value = exp;
            });
            
            base.InitLocalData ();
            
        }


        #region Methods

        public void AddPuzzleExp (PuzzleMatchingType puzzleMatchingType)
        {
            if (!AllSpecialPuzzles.ContainsKey (puzzleMatchingType))
                return;
            
            AllSpecialPuzzles[puzzleMatchingType].AddPuzzleExp (AllSpecialPuzzles[puzzleMatchingType].SpecialPuzzleData.CheckValue);
        }


        public int GetPuzzleValue (PuzzleMatchingType puzzleMatchingType)
        {
            if (!AllSpecialPuzzles.ContainsKey (puzzleMatchingType))
                return 0;

            return AllSpecialPuzzles[puzzleMatchingType].SpecialPuzzleData.CheckValue;
        }
        
        
        public bool IsUnlocked (PuzzleMatchingType puzzleMatchingType)
        {
            return AllSpecialPuzzles.ContainsKey (puzzleMatchingType) && AllSpecialPuzzles[puzzleMatchingType].IsUnlock;
        }


        public bool Unlock (PuzzleMatchingType puzzleMatchingType)
        {
            if (!AllSpecialPuzzles.ContainsKey (puzzleMatchingType))
                return false;
            
            var isUnlocked = AllSpecialPuzzles[puzzleMatchingType].IsUnlock;
            AllSpecialPuzzles[puzzleMatchingType].IsUnlock = true;
            return AllSpecialPuzzles.ContainsKey (puzzleMatchingType) && !isUnlocked;
        }
        

        #endregion


        #region GetLevel

        public PuzzleLevel GetLevelData (int exp)
        {
            return TableDataManager.Instance.PuzzleLevelDict.Values.FirstOrDefault (x => x.AccReqExp > exp);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}