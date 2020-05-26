using System.Collections.Generic;
using System.Linq;

namespace HexaPuzzle
{
    public enum PuzzleMatchingTypes
    {
        None,
        ThreeMatching = 3,
        FourMatching = 4,
        FiveMatching = 5,
        Overlap,
        CombineSpecial,
        Pick
    }

    public class MatchingResultModel
    {
        public bool isMatching;

        public List<TotalCheckResultModel> checkResultModels = new List<TotalCheckResultModel> ();


        public void Reset ()
        {
            isMatching = false;
            checkResultModels.Clear ();
        }

        public void SetMathingState (bool matching)
        {
            isMatching = isMatching || matching;
        }

        public void RemovePuzzle (PuzzleModel puzzleModel)
        {
            checkResultModels.Foreach (totalCheckResultModel =>
            {
                if (totalCheckResultModel.CheckPuzzles.Contains (puzzleModel))
                {
                    totalCheckResultModel.CheckPuzzles.Remove (puzzleModel);
                }
            });
        }

        public void AddCheckResult (TotalCheckResultModel checkResultModel)
        {
            checkResultModels.Add (checkResultModel);
            checkResultModels = checkResultModels.OrderByDescending (x => x.CheckPuzzles.Count).ToList ();
        }


        public (TotalCheckResultModel, bool) FindContainedResultModels (IEnumerable<PuzzleModel> puzzleModels)
        {
            var checkResultTuple = checkResultModels
                .Select (x => x.ContainResult (puzzleModels))
                .FirstOrDefault (x => x.Item2);
            return checkResultTuple;
        }
    }
}