using System;
using System.Collections.Generic;
using KKSFramework.DesignPattern;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace AutoChess
{
    public class HealthEvent : UnityEvent<int>
    {
    }

    public class CharacterModel : ModelBase
    {
        #region Fields & Property

        public int UniqueCharacterId;

        public CharacterGrade StarGrade;
        
        public readonly IntReactiveProperty Exp = new IntReactiveProperty ();
        
        /// <summary>
        /// 캐릭터 데이터.
        /// </summary>
        public Character CharacterData;

        /// <summary>
        /// 캐릭터 기본 능력치.
        /// </summary>
        public StatusModel StatusModel;

        /// <summary>
        /// 장비 능력치.
        /// </summary>
        public EquipmentModel EquipmentModel;

        /// <summary>
        /// 스킬로 인해 변동된 능력치.
        /// </summary>
        public SkillStatusModel SkillStatusModel = new SkillStatusModel ();

        /// <summary>
        /// 현재 위치.
        /// </summary>
        public PositionModel PositionModel;

        /// <summary>
        /// 이동 예정 위치.
        /// </summary>
        public PositionModel PredicatedPositionModel = PositionModel.Empty;

        /// <summary>
        /// 캐릭터 진영(플레이어, AI).
        /// </summary>
        public CharacterSideType CharacterSideType;
        
        /// <summary>
        /// 사망 여부.
        /// </summary>
        public bool IsExcuted;

        
#pragma warning disable CS0649

#pragma warning restore CS0649

        /// <summary>
        /// 체력 이벤트.
        /// </summary>
        private readonly HealthEvent _healthEvent = new HealthEvent ();

        /// <summary>
        /// 체력.
        /// </summary>
        private FloatReactiveProperty _health;
        
        /// <summary>
        /// 체력
        /// </summary>
        private IDisposable _healthDisposable;

        /// <summary>
        /// 지속 상태.
        /// </summary>
        private List<IDisposable> _registeredDisposables;

        
        #endregion


        #region Methods


        public void StartBattle ()
        {
            IsExcuted = false;
            _health = new FloatReactiveProperty (GetTotalStatusValue (StatusType.Health));
            _healthDisposable = _health.Subscribe (hp =>
            {
                _healthEvent.Invoke ((int)hp);
                if (hp <= 0)
                {
                    EndBattle ();
                }
            });
            
            _registeredDisposables = new List<IDisposable> ();
        }


        public void EndBattle ()
        {
            PositionModel.Clear ();
            PredicatedPositionModel.Clear ();
            _healthDisposable.DisposeSafe ();
            _registeredDisposables.Foreach (x => x.DisposeSafe ());
            _registeredDisposables.Clear ();
            SkillStatusModel.Clear ();
            IsExcuted = true;
        }
        
        
        public void AddHealthEvent (UnityAction<int> healthAction)
        {
            _healthEvent.AddListener (healthAction);
        }
        

        public void SetUniqueData (int uid, int exp)
        {
            UniqueCharacterId = uid;
            Exp.Value = exp;
        }

        
        public void SetCharacterData (Character character)
        {
            CharacterData = character;
        }
        
        
        public void SetStatusModel (StatusModel statusModel)
        {
            StatusModel = statusModel;
        }

        
        public void SetEquipmentModel (EquipmentModel equipmentModel)
        {
            EquipmentModel = equipmentModel;
        }


        public void SetSide (CharacterSideType sideType)
        {
            CharacterSideType = sideType;
        }

        #endregion


        #region Level

        public CharacterLevel GetLevelData ()
        {
            return GameExtension.GetCharacterLevel (Exp.Value);
        }


        public int NowExp ()
        {
            return Exp.Value - (int)GetLevelData ().CoExp;
        }
        
        #endregion


        #region Status

        public BaseStatusModel GetBaseStatusModel (StatusType statusType)
        {
            return StatusModel.GetBaseStatusModel (statusType);
        }
        
        
        public float GetBaseStatusValue (StatusType statusType)
        {
            return StatusModel.GetStatusValue (statusType);
        }
        
        
        public float GetEquipmentStatusValue (StatusType statusType)
        {
            return EquipmentModel.GetStatusValue (statusType);
        }


        public float GetSkillStatusValue (StatusType statusType)
        {
            return SkillStatusModel.GetStatusValue (statusType);
        }
        
        
        public float GetTotalStatusValue (StatusType statusType)
        {
            return GetBaseStatusValue (statusType) + GetEquipmentStatusValue (statusType) + GetSkillStatusValue(statusType);
        }
        
        #endregion


        #region Position
        
        public void SetPositionModel (PositionModel positionModel)
        {
            PositionModel = positionModel;
        }
        

        public void SetPredicatePosition (PositionModel positionModel)
        {
            PredicatedPositionModel = positionModel;
        }

        
        public void RemovePredicatePosition ()
        {
            PredicatedPositionModel = PositionModel.Empty;
        }

        #endregion


        #region Skill

        public void ApplySkill (SkillModel skillModel, float skillValue)
        {
            CheckStatus (skillModel, skillValue);
        }


        public void CheckStatus (SkillModel skillModel, float skillValue)
        {
            // 체력을 증감시키는 스킬이라면.
            if (skillModel.SkillData.SkillStatusType == StatusType.Health)
            {
                var calcedValue = _health.Value + skillValue;
                _health.Value = Mathf.Clamp (calcedValue, 0, GetTotalStatusValue (StatusType.Health));
                return;
            }
            
            // 지속 시간이 존재함.
            if (skillModel.SkillData.InvokeTime > 0)
            {
                SkillStatusModel.AddStatus (skillModel.SkillData.SkillStatusType, new BaseStatusModel
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
        
        #endregion


        public override string ToString ()
        {
            return $"{CharacterData.Name}({CharacterSideType}) - Position : {PositionModel},{PredicatedPositionModel}";
        }
    }
}