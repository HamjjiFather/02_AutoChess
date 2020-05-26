using System.Collections.Generic;
using System.Linq;


namespace HexaPuzzle
{
    public enum PuzzleCheckTypes
    {
        None,
        ToVertical = 0,
        ToUpLeftDownRight = 1,
        ToUpRightDownLeft = 2,
    }


    /// <summary>
    /// 라인 체크 방향.
    /// </summary>
    public enum CheckDirectionTypes
    {
        ToUpward = 180,
        ToDownward = 0,
        ToUpLeft = 120,
        ToDownLeft = 60,
        ToUpRight = 240,
        ToDownRight = 300,
    }

    public class TotalCheckResultModel : CheckResultModel
    {
        public PuzzleModel OverlapPuzzle;

        public PuzzleModel PickedPuzzle;

        public void SetOverlapPuzzle (PuzzleModel puzzleModel)
        {
            OverlapPuzzle = puzzleModel;
        }

        public void SetPickedPuzzle (PuzzleModel puzzleModel)
        {
            PickedPuzzle = puzzleModel;
        }

        public void SetTotalMatchingType (IEnumerable<PuzzleMatchingTypes> puzzleMatchingTypes)
        {
            puzzleMatchingTypes.Foreach (types =>
            {
                PuzzleMatchingTypes = PuzzleMatchingTypes >= types ? PuzzleMatchingTypes : types;
            });
        }


        public (TotalCheckResultModel, bool) ContainResult (IEnumerable<PuzzleModel> puzzleModels)
        {
            if (puzzleModels.Any () &&
                puzzleModels.Count () >= CheckPuzzles.Count &&
                puzzleModels.All (CheckPuzzles.Contains))
            {
                return (this, true);
            }

            return (null, false);
        }
    }


    public class CheckResultModel
    {
        public PuzzleMatchingTypes PuzzleMatchingTypes;

        public List<PuzzleModel> CheckPuzzles = new List<PuzzleModel> ();

        public bool IsChecked => PuzzleMatchingTypes != PuzzleMatchingTypes.None;

        public CheckResultModel ()
        {
        }

        public void AddCheckedPuzzle (PuzzleModel puzzleModel)
        {
            CheckPuzzles.Add (puzzleModel);
        }

        public void SetMatchingType (PuzzleMatchingTypes puzzleMatchingTypes)
        {
            PuzzleMatchingTypes = puzzleMatchingTypes;

            if (!IsChecked)
                CheckPuzzles.Clear ();
        }

        public void AddRangeMatchPuzzle (IEnumerable<PuzzleModel> puzzleModels)
        {
            if (!puzzleModels.Any ())
                return;

            puzzleModels.ToList ().Remove (null);
            puzzleModels = puzzleModels.Distinct ().ToList ();

            CheckPuzzles.AddRange (puzzleModels);
            CheckPuzzles = CheckPuzzles.Distinct ().ToList ();
        }
    }
}