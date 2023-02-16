using DG.DemiEditor;

namespace AutoChess
{
    public static class StringHelper
    {
        public const string LevelStringFormat = "Lv.{0}";
        
        public static string ToLevelString (int level) => string.Format(LevelStringFormat, level);
    }
}