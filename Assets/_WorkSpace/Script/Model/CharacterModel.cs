using System;
using KKSFramework.DesignPattern;
using UniRx;
using UnityEngine.Events;
using UnityEngine.UIElements;

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
        /// 능력치.
        /// </summary>
        public StatusModel StatusModel;

        /// <summary>
        /// 장비.
        /// </summary>
        public EquipmentModel EquipmentModel;

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
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly HealthEvent _healthEvent = new HealthEvent ();

        private FloatReactiveProperty _health;
        
        private IDisposable _healthDisposable;
        

        #endregion


        #region Methods


        public void StartBattle ()
        {
            _health = new FloatReactiveProperty (GetTotalStatusValue (StatusType.Health));
            _health.Subscribe (hp =>
            {
                _healthEvent.Invoke ((int)hp);
            });
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
        
        
        public float GetBaseStatus (StatusType statusType)
        {
            return StatusModel.GetStatusValue (statusType);
        }
        
        
        public float GetEquipmentStatus (StatusType statusType)
        {
            return EquipmentModel.GetStatusValue (statusType);
        }
        
        
        public float GetTotalStatusValue (StatusType statusType)
        {
            return GetBaseStatus (statusType) + GetEquipmentStatus (statusType);
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

        public void ApplySkill (SkillModel skillModel)
        {
            
        }

        #endregion


        public override string ToString ()
        {
            return $"{CharacterData.Name}({CharacterSideType}) - Position : {PositionModel},{PredicatedPositionModel}";
        }
    }
}