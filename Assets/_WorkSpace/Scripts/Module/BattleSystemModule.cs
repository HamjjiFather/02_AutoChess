using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Helper;
using MasterData;
using ResourcesLoad;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public enum BattleState
    {
        None,
        Idle,
        Blocked,
        Moving,
        Jump,
        Behave,
        Death
    }

    public class BattleSystemModule : MonoBehaviour
    {
        #region Fields & Property

        public MovingSystemModule movingSystemModule;

        public BehaviourSystemModule behaviourSystemModule;

        public Transform damageElementParents;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private SkillViewmodel _skillViewmodel;

        [Inject]
        private BattleViewLayout _battleViewLayout;

        [Inject (Id = "BulletParents")]
        private Transform _bulletParents;

#pragma warning restore CS0649

        public CharacterModel CharacterModel => _battleCharacterPackage.battleCharacterElement.ElementData;

        /// <summary>
        /// 전투 상태.
        /// </summary>
        private BattleState _battleState;

        public BattleState BattleState => _battleState;

        /// <summary>
        /// Task Cancellation TokenSource.
        /// </summary>
        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// 애니메이션 액션.
        /// </summary>
        private Func<BattleState, CancellationToken, UniTask> _playAnimationAction;

        /// <summary>
        /// 체력 이벤트.
        /// </summary>
        private readonly HealthEvent _healthEvent = new HealthEvent ();

        /// <summary>
        /// 체력.
        /// </summary>
        private FloatReactiveProperty _health;

        public FloatReactiveProperty Health => _health;

        /// <summary>
        /// 체력
        /// </summary>
        private IDisposable _healthDisposable;

        /// <summary>
        /// 지속 상태.
        /// </summary>
        private List<IDisposable> _registeredDisposables;

        /// <summary>
        /// 생성된 데미지 표시 엘리먼트.
        /// </summary>
        private readonly List<BattleDamageElement> _spawnedDamageElements = new List<BattleDamageElement> ();

        /// <summary>
        /// 컴포넌트 패키지.
        /// </summary>
        private BattleCharacterPackage _battleCharacterPackage;

        private bool _atFirst;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void InitModule (BattleCharacterPackage characterPackage)
        {
            _battleCharacterPackage = characterPackage;
        }


        public void StartBattle (UnityAction<float> gageAction)
        {
            if (BattleState == BattleState.Death)
            {
                return;
            }

            _cancellationToken = new CancellationTokenSource ();
            _health = _health ?? new FloatReactiveProperty (CharacterModel.GetTotalStatusValue (StatusType.Health));
            _battleState = BattleState.Idle;
            _healthDisposable = _health.Subscribe (hp => { _healthEvent.Invoke ((int) hp); });
            _registeredDisposables = new List<IDisposable> ();
            behaviourSystemModule.Initialize (gageAction);

            CheckNextBehaviour (true).Forget ();
        }


        /// <summary>
        /// 전투 종료(사망 처리).
        /// </summary>
        public void EndBattle ()
        {
            // 이미 종료처리가 되어있음.
            if (_battleState == BattleState.Death)
                return;

            behaviourSystemModule.Dispose ();
            movingSystemModule.Dispose ();
            _cancellationToken?.Cancel ();
            _cancellationToken?.Dispose ();
            _registeredDisposables.ForEach (x => x.Dispose ());
            _registeredDisposables.Clear ();

            _spawnedDamageElements.ForEach (element => { ObjectPoolingHelper.Despawn (element.transform); });
            _spawnedDamageElements.Clear ();
        }


        public void Dead ()
        {
            EndBattle ();
            _battleState = BattleState.Death;
        }


        public void SetCallbacks (UnityAction<int> healthAction,
            Func<BattleState, CancellationToken, UniTask> animationCallback)
        {
            _healthEvent.AddListener (healthAction);
            _playAnimationAction = animationCallback;
        }


        /// <summary>
        /// 다음 행동을 계산. 
        /// </summary>
        /// <param name="atFirst"> 암살자 점프를 위한 플래그. </param>
        public async UniTask CheckNextBehaviour (bool atFirst = false)
        {
            var result = _battleViewmodel.CheckBehaviour (_battleCharacterPackage.battleCharacterElement, atFirst);
            _battleState = result.ResultState;

            switch (_battleState)
            {
                case BattleState.None:
                    break;

                case BattleState.Idle:
                    break;

                // 사방이 막힌 상태.
                case BattleState.Blocked:
                    await UniTask.Delay (TimeSpan.FromSeconds (0.5f));
                    CheckNextBehaviour ().Forget ();
                    break;

                case BattleState.Moving:
                    await Move (result);
                    CheckNextBehaviour ().Forget ();
                    break;

                case BattleState.Jump:
                    await Move (result);
                    CheckNextBehaviour ().Forget ();
                    break;

                case BattleState.Behave:
                    await Behaviour (result);
                    CheckNextBehaviour ().Forget ();
                    break;

                case BattleState.Death:
                    break;

                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }


        /// <summary>
        /// 이동 처리.
        /// </summary>
        /// <param name="behaviourResultModel"></param>
        /// <param name="speed"> 이동 완료까지 시간. </param>
        /// <returns></returns>
        private async UniTask Move (BehaviourResultModel behaviourResultModel, float speed = 0.75f)
        {
            var position = behaviourResultModel.TargetPosition;
            Debug.Log ($"{CharacterModel} move to {position}");
            CharacterModel.SetPredicatePosition (position);
            await movingSystemModule.Moving (_battleViewLayout.GetLandElement (position), _cancellationToken.Token,
                speed);
            _battleViewmodel.CompleteMovement (CharacterModel, position);
            Debug.Log ("complete movement");
        }


        /// <summary>
        /// 공격/스킬 행동.
        /// </summary>
        private async UniTask Behaviour (BehaviourResultModel behaviourResultModel)
        {
            await _playAnimationAction (BattleState.Behave, _cancellationToken.Token);
            await behaviourSystemModule.Behaviour (CharacterModel, behaviourResultModel, _cancellationToken.Token,
                ApplyAfterSkill);
        }


        /// <summary>
        /// 스킬 적용.
        /// </summary>
        public void ApplySkill (SkillModel skillModel, SkillValueModel skillValueModel)
        {
            if (_battleState == BattleState.Death)
                return;

            CheckStatus (skillModel, skillValueModel);
            PlayCommonParticle (skillModel.SkillData.ApplyParticleIndex);
        }


        /// <summary>
        /// 스킬 사용 후 처리.
        /// </summary>
        public void ApplyAfterSkill (SkillModel skillModel)
        {
            if (skillModel.ApplyBullet)
                CreateBullet ();

            var afterSkillIndex = skillModel.SkillData.AfterSkillIndex;
            if (afterSkillIndex == Constants.INVALID_INDEX)
                return;

            _skillViewmodel.InvokeAfterSkill (skillModel);

            void CreateBullet ()
            {
                skillModel.TargetCharacters.ForEach (character =>
                {
                    var target = _battleViewmodel.FindCharacterElement (character);
                    var bulletModel = new BattleBulletModel
                    {
                        SkillModel = skillModel,
                        Origin = _battleCharacterPackage.battleCharacterElement.transform.position,
                        Target = target.transform
                    };

                    var bulletObj = ObjectPoolingHelper.Spawn<BattleBulletElement> (
                        ResourceRoleType.Bundles.ToString (), ResourcesType.Element.ToString (),
                        character.CharacterData.BulletResName, _bulletParents);
                    bulletObj.SetElement (bulletModel);
                });
            }
        }


        /// <summary>
        /// 스킬 적용 처리.
        /// </summary>
        public void CheckStatus (SkillModel skillModel, SkillValueModel skillValueModel)
        {
            // 체력을 증감시키는 스킬이라면.
            if (skillModel.SkillData.SkillStatusType == StatusType.Health)
            {
                // 피해를 입음.
                if (skillModel.DamageType == DamageType.Damage)
                {
                    behaviourSystemModule.AddSkillValue (Constants.RESTORE_SKILL_GAGE_ON_HIT);

                    // 방어력.
                    var defenseValue = CharacterModel.GetTotalStatusValue (StatusType.Defense);
                    var calcedDefenseValue = EasingDefenseValue (defenseValue * 1 / Constants.MAX_DEFENSE_VALUE);
                    var appliedValue = skillValueModel.PreApplyValue -
                                       skillValueModel.PreApplyValue * calcedDefenseValue;

                    skillValueModel.SetAppliedValue ((float) appliedValue);
                    Debug.Log (
                        $"DamageValue = {skillValueModel.PreApplyValue}, DefenseValue = {defenseValue}, CalcedValue = {calcedDefenseValue}, AppliedValue = {appliedValue}");

                    double EasingDefenseValue (float value)
                    {
                        return 1 - Math.Pow (1 - value, 3);
                    }
                }

                DamageElement (Math.Abs (skillValueModel.AppliedValue), skillModel.DamageType);
                SetApplyParticle (skillModel.DamageType);
                SetHealth (skillValueModel.AppliedValue);
                return;
            }

            // 지속 시간이 존재함.
            if (skillModel.SkillData.InvokeTime > 0)
            {
                CharacterModel.SkillStatusModel.AddStatus (skillModel.SkillData.SkillStatusType, new BaseStatusModel
                {
                    StatusData = TableDataHelper.Instance.GetStatus (skillModel.SkillData.SkillStatusType),
                    StatusValue = skillValueModel.PreApplyValue
                });

                var disposable = Observable.Timer (TimeSpan.FromSeconds (skillModel.SkillData.InvokeTime)).Subscribe (
                    _ => { });

                _registeredDisposables.Add (disposable);
            }
        }


        /// <summary>
        /// 체력 설정.
        /// </summary>
        public void SetHealth (float applyValue)
        {
            var calcedValue = _health.Value + applyValue;
            _health.Value = Mathf.Clamp (calcedValue, 0, CharacterModel.GetTotalStatusValue (StatusType.Health));
        }


        /// <summary>
        /// 전체 체력 대비 체력 설정.
        /// </summary>
        /// <param name="perValue"> between 0 to 1 </param>
        public void SetHealthByPercent (float perValue)
        {
            var applyValue = CharacterModel.GetTotalStatusValue (StatusType.Health) * perValue;
            SetHealth (applyValue);
        }


        /// <summary>
        /// 회복/데미지 출력.
        /// </summary>
        private void DamageElement (float skillValue, DamageType damageType)
        {
            var damageModel = new BattleDamageModel
            {
                Amount = (int) skillValue,
                DamageType = damageType
            };
            var damageElement = ObjectPoolingHelper.Spawn<BattleDamageElement> (ResourceRoleType
                    .Bundles.ToString (), ResourcesType.Element.ToString (), nameof (BattleDamageElement),
                damageElementParents);
            damageElement.GetComponent<RectTransform> ().SetLocalReset ();
            damageElement.SetElement (damageModel);
            damageElement.SetDespawn (DespawnDamageElement, _cancellationToken.Token).Forget ();
            _spawnedDamageElements.Add (damageElement);

            void DespawnDamageElement (BattleDamageElement battleDamageElement)
            {
                ObjectPoolingHelper.Despawn (battleDamageElement.transform);
                _spawnedDamageElements.Remove (battleDamageElement);
            }
        }

        /// <summary>
        /// 스킬 적용시 파티클 실행.
        /// </summary>
        private void SetApplyParticle (DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Heal:
                    break;

                case DamageType.Damage:
                    PlayBuiltInParticle (CharacterBuiltInParticleType.Hit);
                    _battleCharacterPackage.characterAppearanceModule.DoFlashImageTween ();
                    break;

                case DamageType.CriticalHeal:
                    break;

                case DamageType.CriticalDamage:
                    PlayBuiltInParticle (CharacterBuiltInParticleType.CriticalHit);
                    break;

                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }

        /// <summary>
        /// 파티클 실행.
        /// </summary>
        private void PlayBuiltInParticle (CharacterBuiltInParticleType particleType)
        {
            _battleCharacterPackage.characterParticleModule.PlayParticle (particleType);
        }


        private void PlayCommonParticle (int index)
        {
            _battleCharacterPackage.characterParticleModule.GenerateParticle (index);
        }

        #endregion
    }
}