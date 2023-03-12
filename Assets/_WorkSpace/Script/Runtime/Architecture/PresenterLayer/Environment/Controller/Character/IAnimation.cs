using System.Collections;
using UnityEngine;

namespace AutoChess
{
    public interface IAnimation
    {
        Animator Animator { get; set; }

        IEnumerator WaitForPlayAnimation(AnimationState state);
    }
}