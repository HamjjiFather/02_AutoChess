namespace AutoChess
{
    public readonly struct PositionModel
    {
        #region Fields & Property

        public readonly int Column;

        public readonly int Row;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #endregion


        public PositionModel (string @string)
        {
            var splitString = @string.Split (',');
            Column = int.Parse (splitString[0]);
            Row = int.Parse (splitString[1]);
        }

        public override string ToString ()
        {
            return $"{Column},{Row}";
        }
    }
}