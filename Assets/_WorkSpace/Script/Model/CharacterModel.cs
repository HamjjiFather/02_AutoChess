using KKSFramework.DesignPattern;
using UniRx;
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
        
        
        public override string ToString ()
        {
            return $"{CharacterData.Name}({CharacterSideType}) - Position : {PositionModel},{PredicatedPositionModel}";
        }
    }
}