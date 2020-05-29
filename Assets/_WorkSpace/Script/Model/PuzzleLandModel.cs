using KKSFramework.DesignPattern;

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

    public class PuzzleLandModel : ModelBase
    {
        public readonly int row;

        public readonly LandTypes landTypes;

        public PuzzleLandModel (int row, LandTypes landTypes)
        {
            this.row = row;
            this.landTypes = landTypes;
        }
    }
}