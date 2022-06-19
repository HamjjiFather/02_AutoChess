using System;
using System.Collections.Generic;
using KKSFramework;
using Helper;
using MasterData;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using KKSFramework.ResourcesLoad;
using ResourcesLoad;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace AutoChess
{
    /// <summary>
    /// 사망 정보.
    /// </summary>
    public struct DeathInfo
    {
        public bool Death;

        public string DeathDate;
    }
    
    public class HealthEvent : UnityEvent<int>
    {
    }


    public class CharacterData
    {
        #region Fields & Property

        public bool IsAssigned => !UniqueCharacterId.Equals (0);

        /// <summary>
        /// 유니크 ID.
        /// </summary>
        public int UniqueCharacterId;

        /// <summary>
        /// 경험치.
        /// </summary>
        public readonly IntReactiveProperty Exp = new IntReactiveProperty ();

        /// <summary>
        /// 직업.
        /// </summary>
        public CharacterJobType[] Jobs;

        /// <summary>
        /// 기본 캐릭터.
        /// </summary>
        public Character CharacterTable;
        
        /// <summary>
        /// 기본 공격 스킬 데이터.
        /// </summary>
        public CharacterSkill AttackData;

        /// <summary>
        /// 스킬 데이터.
        /// </summary>
        public CharacterSkill SkillData;

        /// <summary>
        /// 캐릭터 등급.
        /// </summary>
        public CharacterPrimeAbility PrimeAbility;

        /// <summary>
        /// 현재 위치.
        /// </summary>
        public PositionModel PositionModel;

        /// <summary>
        /// 이동 예정 위치.
        /// </summary>
        public PositionModel PredicatedPositionModel = PathFindingHelper.Instance.EmptyPosition;

        /// <summary>
        /// 캐릭터 진영(플레이어, AI).
        /// </summary>
        public CharacterSideType CharacterSideType;
        
        /// <summary>
        /// 사망 정보.
        /// </summary>
        public DeathInfo CharacterDeathInfo;
        
        /// <summary>
        /// 아이콘 이미지 리소스.
        /// </summary>
        public Sprite IconImageResources => ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
            ResourcesType.Monster, CharacterTable.SpriteResName);


        public RuntimeAnimatorController CharacterAnimatorResources =>
            ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                CharacterTable.AnimatorResName);


        #region Interface Implements

        /// <summary>
        /// 능력치 보유자.
        /// </summary>
        public string OwnerString => CharacterDefines.GetCharacterAbilityOwnerString (UniqueCharacterId);

        public int Index => CharacterTable.Id;

        public string NameString => CharacterTable.Name;

        public Sprite ImageSprite => IconImageResources;
        
        #endregion


#pragma warning disable CS0649

#pragma warning restore CS0649

        private Subject<CharacterData> _valueChangeSubject;

        // private Dictionary<AbilityType, PlayableCharacterAbilityContainer> _abilityGradeBoundValue = new Dictionary<AbilityType, PlayableCharacterAbilityContainer> ();

        #endregion

        
        #region Methods


        public void EndBattle ()
        {
            PositionModel.Clear ();
            PredicatedPositionModel.Clear ();
        }
        
        
        public void SetUniqueData (int uid, int exp)
        {
            UniqueCharacterId = uid;
            Exp.Value = exp;
        }

        
        public void SetBaseData (Character character, CharacterSkill attackSkill, CharacterSkill skill)
        {
            CharacterTable = character;
            AttackData = attackSkill;
            SkillData = skill;
        }
        

        public void SetSide (CharacterSideType sideType)
        {
            CharacterSideType = sideType;
        }

        

        public void AddStatus ()
        {
            
        }


        #endregion


        #region Level

        public CharacterLevel GetLevelData ()
        {
            return TableDataHelper.Instance.GetCharacterLevelByExp (Exp.Value);
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

        
        public void SetEmptyEquipmentModel ()
        {
            _valueChangeSubject?.OnNext (this);
        }
        
        
        
        public void SetEquipmentModel (IEnumerable<EquipmentModel> equipmentModels)
        {
            _valueChangeSubject?.OnNext (this);
        }
        
        
        /// <summary>
        /// 장비 장착.
        /// </summary>
        public void ChangeEquipmentModel (int index, EquipmentModel equipmentModel)
        {
            _valueChangeSubject?.OnNext (this);
        }


        #endregion

        #region Skill

        #endregion


        #region Position
        
        public void SetPositionModel (PositionModel positionModel)
        {
            PositionModel = positionModel;
            PredicatedPositionModel = positionModel;
        }
        

        public void SetPredicatePosition (PositionModel positionModel)
        {
            PredicatedPositionModel = positionModel;
        }

        
        public void RemovePredicatePosition ()
        {
            PredicatedPositionModel = PathFindingHelper.Instance.EmptyPosition;
        }

        #endregion


        #region Util

        public void CharacterInfoSubscribe (Action<CharacterData> onNextAction)
        {
            _valueChangeSubject = new Subject<CharacterData> ();
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
            return $"{CharacterTable.Name}({CharacterSideType}) - Position : {PositionModel},{PredicatedPositionModel}";
        }

    }
}