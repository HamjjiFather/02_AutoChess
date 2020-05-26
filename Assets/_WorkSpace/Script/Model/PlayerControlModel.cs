using System.Collections.Generic;
using System.Linq;

namespace HexaPuzzle
{

    public class PlayerControlModel
    {
        public readonly List<PuzzleModel> ControlPuzzles = new List<PuzzleModel> ();

        public PuzzleCheckTypes PlayerControlDirection;

        public bool IsPlayerControl;

        public PlayerControlModel (bool isPlayerControl)
        {
            ControlPuzzles.Clear ();
            IsPlayerControl = isPlayerControl;
        }

        public void AddControledPuzzles (IEnumerable<PuzzleModel> movedPuzzles)
        {
            ControlPuzzles.Clear ();
            ControlPuzzles.AddRange (movedPuzzles);
        }

        public IEnumerable<PuzzleModel> PickColorTypePuzzleModels =>
            ControlPuzzles.Where (x => x.PuzzleSpecialTypes == PuzzleSpecialTypes.PickColors);


        public PuzzleModel Origin => IsPlayerControl ? ControlPuzzles[0] : null;

        public PuzzleModel Target => IsPlayerControl ? ControlPuzzles[1] : null;
    }
}