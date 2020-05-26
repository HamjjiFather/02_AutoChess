using System;

namespace HexaPuzzle
{
    /// <summary>
    /// Puzzle position data model.
    /// </summary>
    [Serializable]
    public struct PositionModel
    {
        public int Column;
        public int Row;

        public PositionModel (int column, int row)
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


        public void Set (PositionModel positionModel)
        {
            Column = positionModel.Column;
            Row = positionModel.Row;
        }


        public bool Equals (PositionModel other)
        {
            return Column == other.Column && Row == other.Row;
        }


        public static PositionModel EmptyPositionModel => new PositionModel (int.MaxValue, int.MaxValue);

        public override string ToString ()
        {
            return $"({Column}, {Row})";
        }
    }
}