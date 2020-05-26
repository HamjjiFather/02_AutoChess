using UnityEngine;

namespace HexaPuzzle
{
    public class GameConstants
    {
        public const int CreatedColumn = 3;

        public const float FlowCheckTime = 0.01f;

        public const float WaitCheckTime = 0.9f;

        public static float PuzzleMovementSpeed => Time.deltaTime;
    }
}