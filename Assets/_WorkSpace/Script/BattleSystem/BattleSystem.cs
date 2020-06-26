using System;
using System.Threading;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public enum BattleState
    {
        None,
        Idle,
        Moving,
        Behave,
        Death
    }
    
    public class BattleSystem : MonoBehaviour
    {
        #region Fields & Property
        
        public MovingSystem movingSystem;

        public BehaviourSystem behaviourSystem;
        
#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private CharacterModel _characterModel;
        public CharacterModel CharacterModel => _characterModel;
        
        private BattleState _battleState;
        public BattleState BattleState => _battleState;

        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// 애니메이션 액션.
        /// </summary>
        private Func<BattleState, UniTask> _playAnimationAction;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetCharacterData (CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }

        
        public void StartBattle (UnityAction<float> gageAction)
        {
            _cancellationToken = new CancellationTokenSource();
            _battleState = BattleState.Idle;
            
            behaviourSystem.Initialize (gageAction);
            
            CheckNextBehaviour ().Forget();
        }


        /// <summary>
        /// 전투 종료.
        /// </summary>
        public void EndBattle ()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose ();
        }


        public void PlayAnimationCallback (Func<BattleState, UniTask> animationCallback)
        {
            _playAnimationAction = animationCallback;
        }


        public async UniTask CheckNextBehaviour ()
        {
            _battleState = _battleViewmodel.CheckCharacterBattleState (_characterModel.CharacterSideType,
                _characterModel.PositionModel, out var targetPosition);

            switch (_battleState)
            {
                case BattleState.None:
                    break;
                case BattleState.Idle:
                    break;
                case BattleState.Moving:
                    await Move (targetPosition);
                    CheckNextBehaviour ().Forget();
                    break;
                case BattleState.Behave:
                    await Behaviour (targetPosition);
                    CheckNextBehaviour ().Forget();
                    break;
                case BattleState.Death:
                    break;
                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }
        
        
        /// <summary>
        /// 이동.
        /// </summary>
        private async UniTask Move (PositionModel positionModel)
        {
            Debug.Log ($"{_characterModel} move to {positionModel}");
            _characterModel.SetPredicatePosition (positionModel);
            await movingSystem.Moving (positionModel, _cancellationToken.Token);
            _battleViewmodel.CompleteMovement (_characterModel, positionModel);
            Debug.Log ("complete movement");
        }


        /// <summary>
        /// 공격/스킬 행동.
        /// </summary>
        private async UniTask Behaviour (PositionModel positionModel)
        {
            await _playAnimationAction (BattleState.Behave);
            await behaviourSystem.Behaviour (_characterModel, positionModel, _cancellationToken.Token);
        }
        
        #endregion
    }
}