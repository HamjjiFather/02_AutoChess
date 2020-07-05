using System;
using System.Collections.Generic;
using System.Threading;
using KKSFramework.ResourcesLoad;
using UniRx;
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
    
    public class BattleSystemModule : MonoBehaviour
    {
        #region Fields & Property
        
        public MovingSystemModule movingSystemModule;

        public BehaviourSystemModule behaviourSystemModule;

        public Transform damageElementParents;
        
#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 캐릭터 모델.
        /// </summary>
        private CharacterModel _characterModel;
        public CharacterModel CharacterModel => _characterModel;
        
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
        
        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void InitModule (BattleCharacterPackage characterPackage)
        {
            _battleCharacterPackage = characterPackage;
        }

        
        public void SetCharacterData (CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }

        
        public void StartBattle (UnityAction<float> gageAction)
        {
            _cancellationToken = new CancellationTokenSource();
            _health = new FloatReactiveProperty (_characterModel.GetTotalStatusValue (StatusType.Health));
            _battleState = BattleState.Idle;
            
            _healthDisposable = _health.Subscribe (hp =>
            {
                _healthEvent.Invoke ((int)hp);
                if (hp <= 0)
                {
                    Debug.Log ($"{_characterModel} Death");
                    EndBattle ();
                }
            });
            
            _registeredDisposables = new List<IDisposable> ();
            
            behaviourSystemModule.Initialize (gageAction);
            
            CheckNextBehaviour ().Forget();
        }


        /// <summary>
        /// 전투 종료(사망 처리).
        /// </summary>
        private void EndBattle ()
        {
            _battleState = BattleState.Death;
            
            behaviourSystemModule.Dispose ();
            _cancellationToken.Cancel();
            _cancellationToken.DisposeSafe ();
            _healthDisposable.DisposeSafe ();
            _registeredDisposables.Foreach (x => x.DisposeSafe ());
            _registeredDisposables.Clear ();
            
            _spawnedDamageElements.Foreach (element =>
            {
                element.PoolingObject ();
            });
            _spawnedDamageElements.Clear ();
        }


        public void SetCallbacks (UnityAction<int> healthAction, Func<BattleState, CancellationToken, UniTask> animationCallback)
        {
            _healthEvent.AddListener (healthAction);
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
            await movingSystemModule.Moving (positionModel, _cancellationToken.Token);
            _battleViewmodel.CompleteMovement (_characterModel, positionModel);
            Debug.Log ("complete movement");
        }


        /// <summary>
        /// 공격/스킬 행동.
        /// </summary>
        private async UniTask Behaviour (PositionModel positionModel)
        {
            await _playAnimationAction (BattleState.Behave, _cancellationToken.Token);
            await behaviourSystemModule.Behaviour (_characterModel, positionModel, _cancellationToken.Token, ApplyAfterSkill);
        }
        
        
        /// <summary>
        /// 스킬 적용.
        /// </summary>
        public void ApplySkill (SkillModel skillModel, float skillValue)
        {
            if (_battleState == BattleState.Death)
                return;
            
            CheckStatus (skillModel, skillValue);
        }


        /// <summary>
        /// 스킬 사용 후 처리.
        /// </summary>
        public void ApplyAfterSkill (SkillModel skillModel)
        {
            
        }

        
        /// <summary>
        /// 스킬 적용 처리.
        /// </summary>
        public void CheckStatus (SkillModel skillModel, float skillValue)
        {
            // 체력을 증감시키는 스킬이라면.
            if (skillModel.SkillData.SkillStatusType == StatusType.Health)
            {
                if(skillModel.DamageType == DamageType.Damage)
                    behaviourSystemModule.AddSkillValue (Constant.RestoreSkillGageOnHit);
                
                DamageElement (Math.Abs (skillValue), skillModel.DamageType);
                SetApplyParticle (skillModel.DamageType);
                
                var calcedValue = _health.Value + skillValue;
                _health.Value = Mathf.Clamp (calcedValue, 0, _characterModel.GetTotalStatusValue (StatusType.Health));
                return;
            }
            
            // 지속 시간이 존재함.
            if (skillModel.SkillData.InvokeTime > 0)
            {
                _characterModel.SkillStatusModel.AddStatus (skillModel.SkillData.SkillStatusType, new BaseStatusModel
                {
                    StatusData = TableDataManager.Instance.StatusDict[(int)DataType.Status + (int)skillModel.SkillData.SkillStatusType],
                    StatusValue = skillValue
                });
                
                var disposable = Observable.Timer (TimeSpan.FromSeconds (skillModel.SkillData.InvokeTime)).Subscribe (
                    _ =>
                    {

                    });
                
                _registeredDisposables.Add (disposable);
            }
        }


        /// <summary>
        /// 회복/데미지 출력.
        /// </summary>
        private void DamageElement (float skillValue, DamageType damageType)
        {
            var damageModel = new BattleDamageModel
            {
                Amount = (int)skillValue,
                DamageType = damageType
            };
            var damageElement = ObjectPoolingHelper.GetResources<BattleDamageElement> (ResourceRoleType._Prefab,
                ResourcesType.Element, nameof (BattleDamageElement), damageElementParents);
            damageElement.GetComponent<RectTransform> ().SetInstantiateTransform ();
            damageElement.SetElement (damageModel);
            damageElement.SetDespawn (DespawnDamageElement, _cancellationToken.Token).Forget();
            _spawnedDamageElements.Add (damageElement);

            void DespawnDamageElement (BattleDamageElement battleDamageElement)
            {
                battleDamageElement.PoolingObject ();
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
                    PlayParticle (CharacterParticleType.Hit);
                    _battleCharacterPackage.characterAppearanceModule.DoFlashImageTween ();
                    break;
                    
                case DamageType.CriticalHeal:
                    break;
                    
                case DamageType.CriticalDamage:
                    PlayParticle (CharacterParticleType.CriticalHit);
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }
        
        /// <summary>
        /// 파티클 실행.
        /// </summary>
        private void PlayParticle (CharacterParticleType particleType)
        {
            _battleCharacterPackage.characterParticleModule.PlayParticle (particleType);
        }
        
        #endregion
    }
}