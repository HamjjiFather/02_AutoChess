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
        
        public Character CharacterData;

        public StatusModel StatusModel;

        public EquipmentModel EquipmentModel;

        public PositionModel PositionModel;

        public CharacterSideType CharacterSideType;
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly HealthEvent _healthEvent = new HealthEvent ();
        
        private IDisposable _healthDisposable;
        

        #endregion


        #region Methods


        public void StartBattle ()
        {
            var health = new FloatReactiveProperty (GetTotalStatusValue (StatusType.Health));
            health.Subscribe (hp =>
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


        public void SetPositionModel (PositionModel positionModel)
        {
            PositionModel = positionModel;
        }


        public void SetSide (CharacterSideType sideType)
        {
            CharacterSideType = sideType;
        }

        #endregion


        public CharacterLevel GetLevelData ()
        {
            return GameExtension.GetCharacterLevel (Exp.Value);
        }


        public int NowExp ()
        {
            return Exp.Value - (int)GetLevelData ().CoExp;
        }
        

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
    }
}