namespace AutoChess
{
    public struct PositionModel
    {
        #region Fields & Property

        public int Column;

        public int Row;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
        
        public PositionModel (string positionString)
        {
            var splitString = positionString.Split (',');
            Column = int.Parse (splitString[0]);
            Row = int.Parse (splitString[1]);
        }
        
        
        public PositionModel (int column, int row)
        {
            Column = column;
            Row = row;
        }


        #region Methods


        public void Set (int column, int row)
        {
            Column = column;
            Row = row;
        }
        
        
        public void Clear ()
        {
            this = Empty;
        }


        public static PositionModel Empty { get; } = new PositionModel(-1, -1);

        public override bool Equals (object obj)
        {
            if (obj is PositionModel asPosition)
            {
                return Column == asPosition.Column && Row == asPosition.Row;
            }

            return false;
        }

        
        public bool Equals (PositionModel other)
        {
            return Column == other.Column && Row == other.Row;
        }


        public override string ToString ()
        {
            return $"({Column},{Row})";
        }

        #endregion
    }
}