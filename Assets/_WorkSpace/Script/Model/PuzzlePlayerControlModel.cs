using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace HexaPuzzle
{
    public class PuzzlePlayerControlModel : ModelBase
    {
        public readonly List<PuzzleModel> ControlPuzzles = new List<PuzzleModel> ();

        public PuzzleCheckTypes PlayerControlDirection;

        public bool IsPlayerControl;

        public PuzzlePlayerControlModel (bool isPlayerControl)
        {
            ControlPuzzles.Clear ();
            IsPlayerControl = isPlayerControl;
        }

        public void AddControledPuzzles (IEnumerable<PuzzleModel> movedPuzzles)
        {
            ControlPuzzles.Clear ();
            ControlPuzzles.AddRange (movedPuzzles);
        }


        public PuzzleModel Origin => IsPlayerControl ? ControlPuzzles[0] : null;

        public PuzzleModel Target => IsPlayerControl ? ControlPuzzles[1] : null;
    }
}