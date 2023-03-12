using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace AutoChess
{
    public enum AnimationState
    {
        Idle,
        Movement,
        Behaviour,
    }

    
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationController : MonoBehaviour
    {
        #region Fields & Property

        private Animator _animator;
        public Animator Animator => _animator ??= GetComponent<Animator>();

        
        private AnimationState _animationState;

        private Dictionary<AnimationState, IAnimation> _animationMap = new();
        
        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            _animationMap.Add(AnimationState.Idle, new IdleAnimation(Animator));
            _animationMap.Add(AnimationState.Movement, new MovementAnimation(Animator));
            _animationMap.Add(AnimationState.Behaviour, new BehaviourAnimation(Animator));
        }

        #endregion


        #region This

        public IEnumerator WaitForAnimation(AnimationState state)
        {
            var anim = _animationMap[state];
            yield return anim.WaitForPlayAnimation(state);
        }

        #endregion


        #region Event

        [UsedImplicitly]
        public void BehaviourProcessPoint() => 
            ((BehaviourAnimation)_animationMap[AnimationState.Behaviour]).OnProcessPoint();

        #endregion

        #endregion
    }
}