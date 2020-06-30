using System;
using System.Threading;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class BehaviourSystem : MonoBehaviour
    {
        #region Fields & Property
        
        public SkillModule skillModule;
        
#pragma warning disable CS0649
        
        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private readonly FloatReactiveProperty _skillGageValue = new FloatReactiveProperty ();

        private bool _canBehaviour;

        private bool _isFullSkillGage;

        private IDisposable _skillValueAddDisposable;

        private IDisposable _skillValueDisposable;

        private IDisposable _attackSpeedDisposable;

        private bool _fullSkillGage;
        
        #endregion


        #region UnityMethods

        #endregion


        #region Methods


        public void Initialize (UnityAction<float> gageAction)
        {
            _canBehaviour = true;
            _skillValueDisposable = _skillGageValue.Subscribe (gageAction.Invoke);
            _skillValueAddDisposable = Observable.EveryUpdate ().Subscribe (_ =>
            {
                if (_skillGageValue.Value >= 1f)
                {
                    if (_isFullSkillGage)
                        return;

                    _isFullSkillGage = true;
                }

                _skillGageValue.Value += Time.deltaTime * 0.01f;
            });
        }


        public void Dispose ()
        {
            _canBehaviour = false;
            _isFullSkillGage = false;
            _skillValueDisposable.DisposeSafe ();
            _skillValueAddDisposable.DisposeSafe ();
        }
        
        
        public async UniTask Behaviour (CharacterModel characterModel, PositionModel positionModel, CancellationToken cancellationToken)
        {
            if (_canBehaviour)
            {
                if (_isFullSkillGage)
                {
                    Debug.Log ("Skill behaviour");
                    skillModule.ProgressSkillEffect (characterModel, positionModel, 4000);
                    CheckAttackSpeed (characterModel, cancellationToken).Forget();
                    return;
                }
                
                Debug.Log ("Attack behaviour");
                skillModule.ProgressSkillEffect (characterModel, positionModel, 4000);
                CheckAttackSpeed (characterModel, cancellationToken).Forget();
                return;
            }

            await UniTask.WaitUntil (() => _canBehaviour, cancellationToken: cancellationToken);
        }


        /// <summary>
        /// 공격 속도 대기.
        /// </summary>
        private async UniTask CheckAttackSpeed (CharacterModel characterModel, CancellationToken cancellationToken)
        {
            _canBehaviour = false;
            var attackDelay =  1 / characterModel.GetTotalStatusValue (StatusType.AtSpd);
            Debug.Log ($"Attack Delay = {attackDelay}");
            await UniTask.Delay (TimeSpan.FromSeconds (attackDelay), cancellationToken:cancellationToken);
            _canBehaviour = true;
        }
        
        #endregion


        #region EventMethods

        #endregion
    }
}