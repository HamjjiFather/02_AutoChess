namespace HexaPuzzle
{
    /// <summary>
    /// 기본 필드가 표시되는지.
    /// </summary>
    public enum LandTypes
    {
        Show,
        Hide,
    }

    public class LandModel
    {
        public readonly int row;

        public readonly LandTypes landTypes;

        public LandModel (int row, LandTypes landTypes)
        {
            this.row = row;
            this.landTypes = landTypes;
        }
    }
}