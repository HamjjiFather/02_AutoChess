using System;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using KKSFramework.ResourcesLoad;
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

        /// <summary>
        /// 등급.
        /// </summary>
        public StarGrade StarGrade;
        
        /// <summary>
        /// 경험치.
        /// </summary>
        public readonly IntReactiveProperty Exp = new IntReactiveProperty ();
        
        /// <summary>
        /// 캐릭터 데이터.
        /// </summary>
        public Character CharacterData;

        /// <summary>
        /// 기본 공격 스킬 데이터.
        /// </summary>
        public Skill AttackData;

        /// <summary>
        /// 스킬 데이터.
        /// </summary>
        public Skill SkillData;

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
        public PositionModel PredicatedPositionModel = BattleViewmodel.EmptyPosition;

        /// <summary>
        /// 캐릭터 진영(플레이어, AI).
        /// </summary>
        public CharacterSideType CharacterSideType;
        
        /// <summary>
        /// 사망 여부.
        /// </summary>
        public bool IsExcuted;
        
        /// <summary>
        /// 아이콘 이미지 리소스.
        /// </summary>
        public Sprite IconImageResources => ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
            ResourcesType.Monster, CharacterData.SpriteResName);
        
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        public Subject<CharacterModel> _valueChangeSubject;

        #endregion


        #region Methods


        public void StartBattle ()
        {
            IsExcuted = false;
        }


        public void EndBattle ()
        {
            PositionModel.Clear ();
            PredicatedPositionModel.Clear ();
            SkillStatusModel.Clear ();
            IsExcuted = true;
        }
        
        
        public void SetUniqueData (int uid, int exp)
        {
            UniqueCharacterId = uid;
            Exp.Value = exp;
        }

        
        public void SetBaseData (Character character, Skill attackSkill, Skill skill)
        {
            CharacterData = character;
            AttackData = attackSkill;
            SkillData = skill;
        }
        
        
        public void SetStatusModel (StatusModel statusModel)
        {
            StatusModel = statusModel;
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


        public void AddExp (int exp)
        {
            Exp.Value += exp;
            _valueChangeSubject?.OnNext (this);
        }
        
        #endregion


        #region Equipment

        /// <summary>
        /// 장비 장착.
        /// </summary>
        public void SetEquipmentModel (EquipmentModel equipmentModel)
        {
            EquipmentModel = equipmentModel;
            _valueChangeSubject?.OnNext (this);
        }


        /// <summary>
        /// 장비 장착 여부.
        /// </summary>
        public bool IsExistEquipment ()
        {
            return !EquipmentModel.Equals (EquipmentViewmodel.EmptyEquipmentModel);
        }
        

        #endregion


        #region Status

        public BaseStatusModel GetBaseStatusModel (StatusType statusType)
        {
            return StatusModel.GetBaseStatusModel (statusType);
        }
        
        
        public BaseStatusModel GetEquipmnetStatusModel (StatusType statusType)
        {
            return EquipmentModel.GetBaseStatusModel (statusType);
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


        #region Skill

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
            PredicatedPositionModel = BattleViewmodel.EmptyPosition;
        }

        #endregion


        #region Util

        public void CharacterInfoSubscribe (Action<CharacterModel> onNextAction)
        {
            _valueChangeSubject = new Subject<CharacterModel> ();
            _valueChangeSubject.Subscribe (onNextAction);
        }

        public void DisposeSubscribe ()
        {
            _valueChangeSubject.DisposeSafe ();
            _valueChangeSubject = null;
        }

        #endregion
        
        
        public override string ToString ()
        {
            return $"{CharacterData.Name}({CharacterSideType}) - Position : {PositionModel},{PredicatedPositionModel}";
        }
    }
}