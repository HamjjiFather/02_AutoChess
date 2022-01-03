using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class BehaviourSystemModule : MonoBehaviour
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private SkillViewmodel _skillViewmodel;

#pragma warning restore CS0649

        private readonly FloatReactiveProperty _skillGageValue = new FloatReactiveProperty ();

        
        /// <summary>
        /// 행동 가능 여부.
        /// </summary>
        private bool _canBehaviour;
        public bool CanBehaviour => _canBehaviour;

        /// <summary>
        /// 스킬 게이지 충전 여부.
        /// </summary>
        private bool _isFullSkillGage;
        public bool IsFullSkillGage => _isFullSkillGage;

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
                if (_skillGageValue.Value >= Constant.MaxSkillGageValue)
                {
                    if (_isFullSkillGage)
                        return;

                    _isFullSkillGage = true;
                }

                _skillGageValue.Value += Time.deltaTime * 10;
            });
        }


        public void Dispose ()
        {
            _canBehaviour = false;
            _isFullSkillGage = false;
            _skillValueDisposable.DisposeSafe ();
            _skillValueAddDisposable.DisposeSafe ();
        }


        /// <summary>
        /// 공격 / 스킬
        /// </summary>
        public async UniTask Behaviour (CharacterData characterData, BehaviourResultModel behaviourResultModel,
            CancellationToken cancellationToken, UnityAction<SkillModel> skillCallback)
        {
            if (_canBehaviour)
            {
                if (_isFullSkillGage)
                {
                    Debug.Log ("Skill behaviour");
                    UseSkill (characterData, behaviourResultModel, cancellationToken, characterData.CharacterTable.SkillIndex,
                        skillCallback, false);
                    _skillGageValue.Value = 0;
                    _isFullSkillGage = false;
                    await WaitForCanBehaveState ();
                    return;
                }

                Debug.Log ("Attack behaviour");
                UseSkill (characterData, behaviourResultModel, cancellationToken, characterData.CharacterTable.AttackIndex,
                    skillCallback, characterData.CharacterTable.CharacterRoleType == CharacterRoleType.Range);
                AddSkillValue (Constant.RestoreSkillGageOnAttack);
                await WaitForCanBehaveState ();
            }

            async UniTask WaitForCanBehaveState ()
            {
                await UniTask.WaitUntil (() => _canBehaviour, cancellationToken: cancellationToken);
            }
        }


        /// <summary>
        /// 스킬 사용.
        /// </summary>
        public void UseSkill (CharacterData characterData, BehaviourResultModel behaviourResultModel,
            CancellationToken cancellationToken, int index, UnityAction<SkillModel> skillCallback, bool applyBullet)
        {
            var skillModel = _skillViewmodel.InvokeSkill (characterData, behaviourResultModel, index, applyBullet);
            CheckAttackSpeed (characterData, cancellationToken).Forget ();
            skillCallback.Invoke (skillModel);
        }


        /// <summary>
        /// 발사체 생성.
        /// </summary>
        private void CreateBullet (SkillModel skillModel)
        {
            var bulletModel = new BattleBulletModel ();
            bulletModel.Origin = transform.position;
        }


        /// <summary>
        /// 공격 속도 대기.
        /// </summary>
        private async UniTask CheckAttackSpeed (CharacterData characterData, CancellationToken cancellationToken)
        {
            _canBehaviour = false;
            // var attackDelay = 1 / characterData.GetTotalStatusValue (StatusType.AttackSpeed);
            // await UniTask.Delay (TimeSpan.FromSeconds (attackDelay), cancellationToken: cancellationToken);
            // Debug.Log ($"{characterData} Attack Delay = {attackDelay}");
            _canBehaviour = true;
        }


        public void AddSkillValue (float skillValue)
        {
            var calcedValue = _skillGageValue.Value + skillValue;
            _skillGageValue.Value = Mathf.Clamp (calcedValue, 0, Constant.MaxSkillGageValue);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}