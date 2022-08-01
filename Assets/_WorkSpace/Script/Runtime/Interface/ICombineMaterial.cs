using UnityEngine;

namespace AutoChess
{
    public interface ICombineMaterial
    {
        void Combine ();

        int Index { get; }

        string NameString { get; }

        int Grade { get; }

        Sprite ImageSprite { get; }
    }
}