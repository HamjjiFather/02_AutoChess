using System;

namespace HexaPuzzle
{
    /// <summary>
    /// Puzzle position data model.
    /// </summary>
    [Serializable]
    public struct PuzzlePositionModel
    {
        public int Column;
        public int Row;

        public PuzzlePositionModel (int column, int row)
        {
            Column = column;
            Row = row;
        }

        public void Clear ()
        {
            Column = -int.MaxValue;
            Row = -int.MaxValue;
        }

        public void Set (int column, int row)
        {
            Column = column;
            Row = row;
        }


        public void Set (PuzzlePositionModel puzzlePositionModel)
        {
            Column = puzzlePositionModel.Column;
            Row = puzzlePositionModel.Row;
        }


        public bool Equals (PuzzlePositionModel other)
        {
            return Column == other.Column && Row == other.Row;
        }


        public static PuzzlePositionModel EmptyPuzzlePositionModel => new PuzzlePositionModel (int.MaxValue, int.MaxValue);

        public override string ToString ()
        {
            return $"({Column}, {Row})";
        }
    }
}