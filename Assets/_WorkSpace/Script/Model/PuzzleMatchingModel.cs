using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;

namespace HexaPuzzle
{
    public class PuzzleMatchingResultModel : ModelBase
    {
        public bool isMatching;

        public List<TotalPuzzleCheckResultModel> checkResultModels = new List<TotalPuzzleCheckResultModel> ();


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

        public void AddCheckResult (TotalPuzzleCheckResultModel puzzleCheckResultModel)
        {
            checkResultModels.Add (puzzleCheckResultModel);
            checkResultModels = checkResultModels.OrderByDescending (x => x.CheckPuzzles.Count).ToList ();
        }


        public (TotalPuzzleCheckResultModel, bool) FindContainedResultModels (IEnumerable<PuzzleModel> puzzleModels)
        {
            var checkResultTuple = checkResultModels
                .Select (x => x.ContainResult (puzzleModels))
                .FirstOrDefault (x => x.Item2);
            return checkResultTuple;
        }
    }
}