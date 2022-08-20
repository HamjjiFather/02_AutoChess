using System.Collections.Generic;
using JetBrains.Annotations;
using KKSFramework.Presenter;
using UniRx;
using Zenject;

namespace AutoChess.Presenter
{
    public enum CharacterJobType
    {
        /// <summary>
        /// 힘 계열.
        /// </summary>
        Guard,
        Initiaer,
        Viking,
        Crusher,
        HolyGuardian,
        Leader,
        BloodSucker,
        Babarian,
        DeathKnight,
        Charger,
        
        /// <summary>
        /// 민첩 계열.
        /// </summary>
        Shadow,
        Assassin,
        Silencer,
        GuardBreaker,
        HawkEye,
        Poison,
        Hermes,
        Ranger,
        Hunter,
        Sniper,
        Bomber,
        PierceArrow,
        Mortar,
        
        /// <summary>
        /// 지능 계열.
        /// </summary>
        Summoner,
        Wizard,
        SpellBreaker,
        Cleric,
        Dreamer,
        Druid,
        Undertaker,
        Space,
    }
    
    [UsedImplicitly]
    public class CharacterViewModel : IViewModel
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private GameSetting _gameSetting;

#pragma warning restore CS0649

        private int _lastUniqueId;

        /// <summary>
        /// 모든 캐릭터.
        /// </summary>
        public List<CharacterModelBase> AllCharacterModels { get; private set; } = new();
        
        // public List<BattleCharacterModel> ToBattleCharacterModels => 
        /// <summary>

        public bool IsDataChanged;


        public bool IsDeathCharacterDataChanged;

        /// <summary>
        /// 고용 가능한 캐릭터.
        /// </summary>
        public List<CharacterModelBase> AllEmployableCharacterModels { get; private set; } = new List<CharacterModelBase> ();

        /// <summary>
        /// 고용 가능한 캐릭터가 출현하는 수량.
        /// </summary>
        public IntReactiveProperty EmployableCharacterCount;

        /// <summary>
        /// 새로 고용 가능한지 여부.
        /// </summary>
        public bool IsNewEmployment;

        #endregion


        public void Initialize ()
        {
            // Initialize_Employ ();
        }


        #region Methods

        public void ResetCharacterPosition (int index, CharacterModelBase characterBase)
        {
            // var positionData = LocalDataHelper.GetBattleCharacterPosition ();
            // characterData.SetPositionModel (new PositionModel (positionData.Split ('/')[index]));
        }


        public void AddCharacterExps (int exp)
        {
        }


        public void SaveCharacterData ()
        {
            // LocalDataHelper.SaveCharacterIdData (
            //     AllCharacterModels.Select (x => x.UniqueCharacterId).ToList (),
            //     AllCharacterModels.Select (x => x.CharacterTable.Id).ToList (),
            //     AllCharacterModels.Select (x => x.Exp.Value).ToList (),
            //     AllCharacterModels.Select (x =>
            //             new CharacterBundle.CharacterEquipmentUIds (x.EquipmentStatusModel.EquipmentUId.ToList ()))
            //         .ToList ());
        }

        public void SaveCharacterStatusGradeData ()
        {
            // LocalDataHelper.SaveCharacterStatusGradeData (
            //     AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Health)).ToList (),
            //     AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Attack)).ToList (),
            //     AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.AbilityPoint)).ToList (),
            //     AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Defense)).ToList ());
        }


        public void SaveBattleCharacterData ()
        {
            // LocalDataHelper.SaveBattleCharacterUidData (BattleCharacterModels.Select (x => x.UniqueCharacterId)
            //     .ToList ());
        }


        public void InBattleCharacter (int index, BattleCharacterModel characterBase)
        {
            SaveBattleCharacterData ();
        }


        public void SetNewEmployment ()
        {
            IsNewEmployment = true;
        }


        /// <summary>
        /// 모든 캐릭터를 고용함.
        /// </summary>
        public void EmployAll ()
        {
            AllCharacterModels.AddRange (AllEmployableCharacterModels);
            AllEmployableCharacterModels.Clear ();
            EmployableCharacterCount.Value = 0;
            SaveCharacterData ();
            SaveCharacterStatusGradeData ();
            IsDataChanged = true;
        }


        /// <summary>
        /// 캐릭터를 고용함.
        /// </summary>
        public void EmployCharacter (CharacterModelBase characterBase)
        {
            AllEmployableCharacterModels.Remove (characterBase);
            AllCharacterModels.Add (characterBase);
            EmployableCharacterCount.Value--;
            SaveCharacterData ();
            SaveCharacterStatusGradeData ();
            IsDataChanged = true;
        }


        /// <summary>
        /// 등용 가격 책정.
        /// </summary>
        public int GetEmployPrice (CharacterModelBase characterBase)
        {
            // var gradeValue = ((int)characterData.StarGrade + 1) * 10;
            // var statusValue = characterData.StatusModel.Status.Values.Sum (x => x.GradeValue) / 10f;

            return 0; //gradeValue + (int)statusValue;
        }


        /// <summary>
        /// 등용 가격 책정.
        /// </summary>
        public string GetEmployPriceText (CharacterModelBase characterBase)
        {
            return $"{GetEmployPrice (characterBase)}G";
        }

        #endregion


        #region GetMethods


        public CharacterModelBase GetCharacterModel (int uid)
        {
            return AllCharacterModels[uid];
        }


        public int CombineUniqueId (int uniqueIndex)
        {
            return _gameSetting.baseCharacterUniqueId + uniqueIndex;
        }

        public int NewUniqueId ()
        {
            _lastUniqueId++;
            // LocalDataHelper.SaveCharacterUniqueIdData (_lastUniqueId);
            return _gameSetting.baseCharacterUniqueId + _lastUniqueId;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}