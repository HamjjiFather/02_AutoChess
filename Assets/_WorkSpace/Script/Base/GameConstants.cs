using UnityEngine;

namespace HexaPuzzle
{
    public class GameConstants
    {
        public const int CreatedColumn = 3;

        public const float FlowCheckTime = 0.01f;

        public const float WaitCheckTime = 0.9f;

        public static float PuzzleMovementSpeed => Time.deltaTime;

        public const float ThreeMatchingPuzzleCheckValue = 30f;
        
        public const float FourMatchingPuzzleCheckValue = 45f;
        
        public const float FiveMatchingPuzzleCheckValue = 60f;
        
        public const float OverlapMatchingPuzzleCheckValue = 60f;
        
        public const float CombineSpecialMatchingPuzzleCheckValue = 45f;
        
        public const float PickMatchingPuzzleCheckValue = 30f;
        
    }
}