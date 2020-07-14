using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AutoChess
{
    public class CharacterViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private EquipmentViewmodel _equipmentViewmodel;

        [Inject]
        private GameSetting _gameSetting;

#pragma warning restore CS0649

        private int _lastUniqueId;

        private readonly List<CharacterModel> _allCharacterModels = new List<CharacterModel> ();
        public List<CharacterModel> AllCharacterModels => _allCharacterModels;

        private readonly ReactiveCollection<CharacterModel> _battleCharacterModels = new ReactiveCollection<CharacterModel> ();
        public  ReactiveCollection<CharacterModel> BattleCharacterModels => _battleCharacterModels;

        private readonly int[] _startCharacterIndexes =
        {
            2000,
            2001,
            2002,
            2003,
            2004
        };

        public bool IsDataChanged { get; set; }
        
        public static BaseStatusModel EmptyStatusModel = new BaseStatusModel();

        #endregion


        public override void Initialize ()
        {
        }


        public override void InitTableData ()
        {
            base.InitTableData ();
        }


        public override void InitLocalData ()
        {
            _lastUniqueId = LocalDataHelper.GetGameBundle ().LastCharacterUniqueId;

            var characterBundle = LocalDataHelper.GetCharacterBundle ();

            if (!characterBundle.CharacterUniqueIds.Any ())
            {
                _startCharacterIndexes.Foreach ((index, arrayIndex) =>
                {
                    var characterModel = NewCharacter (index);
                    _battleCharacterModels.Add (characterModel);
                    characterModel.SetPositionModel (new PositionModel (_gameSetting.PlayerCharacterPosition[arrayIndex]));
                });

                SaveCharacterData ();
                SaveCharacterStatusGradeData ();
                SaveBattleCharacterData ();
                IsDataChanged = true;
                return;
            }

            characterBundle.CharacterUniqueIds.Foreach ((uid, index) =>
            {
                var characterModel = new CharacterModel ();
                var statusGrade = characterBundle.CharacterStatusGrades[index];
                var characterData = TableDataManager.Instance.CharacterDict[characterBundle.CharacterIds[index]];
                var characterLevel = GameExtension.GetCharacterLevel (characterBundle.CharacterExps[index]);
                var statusModel = GetBaseStatusModel (characterData, characterLevel,
                    characterBundle.CharacterStatusGrades[index]);
                var skillData = TableDataManager.Instance.SkillDict[characterData.SkillIndex];

                characterModel.SetUniqueData (uid, characterBundle.CharacterExps[index]);
                characterModel.SetBaseData (characterData, skillData);
                characterModel.SetStatusModel (statusModel);
                characterModel.SetPositionModel (new PositionModel (_gameSetting.PlayerCharacterPosition[index]));
                characterModel.SetSide (CharacterSideType.Player);

                characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.AbilityPoint).SetGradeValue (statusGrade.AbilityPointStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);

                var equipment = _equipmentViewmodel.GetEquipmentModel (characterBundle.EquipmentUIds[index]);
                characterModel.SetEquipmentModel (equipment);

                _allCharacterModels.Add (characterModel);
                _battleCharacterModels.Add (characterModel);
            });

            IsDataChanged = true;
        }


        #region Methods

        public void SetBattleCharacter (int index, CharacterModel characterModel)
        {
            characterModel.SetPositionModel (new PositionModel (_gameSetting.PlayerCharacterPosition[index]));
            _battleCharacterModels[index] = characterModel;
        }

        public void SetEquipment (int characterUid, EquipmentModel equipmentModel)
        {
            GetCharacterModel (characterUid).SetEquipmentModel (equipmentModel);
        }


        public void SaveCharacterData ()
        {
            LocalDataHelper.SaveCharacterIdData (
                _allCharacterModels.Select (x => x.UniqueCharacterId).ToList (),
                _allCharacterModels.Select (x => x.CharacterData.Id).ToList (),
                _allCharacterModels.Select (x => x.Exp.Value).ToList (),
                _allCharacterModels.Select (x => x.EquipmentModel.UniqueEquipmentId).ToList ());
        }

        public void SaveCharacterStatusGradeData ()
        {
            LocalDataHelper.SaveCharacterStatusGradeData (
                _allCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Health)).ToList (),
                _allCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Attack)).ToList (),
                _allCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.AbilityPoint)).ToList (),
                _allCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Defense)).ToList ());
        }


        public void SaveBattleCharacterData ()
        {
            LocalDataHelper.SaveBattleCharacterUidData (_battleCharacterModels.Select (x => x.UniqueCharacterId)
                .ToList ());
        }


        /// <summary>
        /// 캐릭터 획득.
        /// </summary>
        public CharacterModel NewCharacter (int characterIndex)
        {
            var characterModel = new CharacterModel ();
            var characterData = TableDataManager.Instance.CharacterDict[characterIndex];
            var gradeStatusValues = NewStatusGradeValue ();
            var characterStatus = GetBaseStatusModel (characterData,
                TableDataManager.Instance.CharacterLevelDict.Values.First (), gradeStatusValues);
            var skillData = TableDataManager.Instance.SkillDict[characterData.SkillIndex];
            var equipmentModel = _equipmentViewmodel.GetEquipmentModel (Constant.InvalidIndex);

            characterModel.SetUniqueData (NewUniqueId (), 0);
            characterModel.SetBaseData (characterData, skillData);
            characterModel.SetStatusModel (characterStatus);
            characterModel.SetEquipmentModel (equipmentModel);
            SetStatusGradeValue ();
            _allCharacterModels.Add (characterModel);

            return characterModel;

            CharacterBundle.CharacterStatusGrade NewStatusGradeValue ()
            {
                return new CharacterBundle.CharacterStatusGrade
                {
                    HealthStatusGrade = Random.Range (0, 1f),
                    AttackStatusGrade = Random.Range (0, 1f),
                    AbilityPointStatusGrade = Random.Range (0, 1f),
                    DefenseStatusGrade = Random.Range (0, 1f)
                };
            }

            void SetStatusGradeValue ()
            {
                characterStatus.SetNewStatusGradeValue (StatusType.Health, gradeStatusValues.HealthStatusGrade);
                characterStatus.SetNewStatusGradeValue (StatusType.Attack, gradeStatusValues.AttackStatusGrade);
                characterStatus.SetNewStatusGradeValue (StatusType.AbilityPoint, gradeStatusValues.AbilityPointStatusGrade);
                characterStatus.SetNewStatusGradeValue (StatusType.Defense, gradeStatusValues.DefenseStatusGrade);
            }
        }


        public void InBattleCharacter (int index, CharacterModel characterModel)
        {
            _battleCharacterModels[index] = characterModel;
            SaveBattleCharacterData ();
        }

        #endregion


        #region GetMethods

        public CharacterModel GetBattleCharacterModel (int index)
        {
            return _battleCharacterModels.Count > index ? _battleCharacterModels[index] : default;
        }


        public CharacterModel GetCharacterModel (int uid)
        {
            return _allCharacterModels[uid];
        }


        public StatusModel GetBaseStatusModel (Character character, CharacterLevel characterLevel,
            CharacterBundle.CharacterStatusGrade characterStatusGrade)
        {
            var characterStatus = GetBaseStatusDict (character, characterLevel, characterStatusGrade);
            var status = new StatusModel ();
            status.SetStatus (characterStatus);
            return status;
        }


        public int CombineUniqueId (int uniqueIndex)
        {
            return _gameSetting.baseCharacterUniqueId + uniqueIndex;
        }

        public int NewUniqueId ()
        {
            _lastUniqueId++;
            LocalDataHelper.SaveCharacterUniqueIdData (_lastUniqueId);
            return _gameSetting.baseCharacterUniqueId + _lastUniqueId;
        }

        #endregion


        private Dictionary<StatusType, BaseStatusModel> GetBaseStatusDict (Character character,
            CharacterLevel characterLevel, CharacterBundle.CharacterStatusGrade characterStatusGrade)
        {
            var dict = new Dictionary<StatusType, BaseStatusModel> ();
            var enums = Enum.GetValues (typeof (StatusType)) as StatusType[];
            enums?.Skip (1).Foreach (statustype =>
            {
                dict.Add (statustype,
                    new BaseStatusModel (TableDataManager.Instance.StatusDict[(int) DataType.Status + (int) statustype]));
            });

            var healthValue = Mathf.Lerp (character.Hp[0], character.Hp[1], characterStatusGrade.HealthStatusGrade) +
                              character.HpInc * characterLevel.Level;
            var attackValue = Mathf.Lerp (character.At[0], character.At[1], characterStatusGrade.AttackStatusGrade) +
                              character.AtInc * characterLevel.Level;
            var apValue = Mathf.Lerp (character.Ap[0], character.Ap[1], characterStatusGrade.AbilityPointStatusGrade) +
                          character.ApInc * characterLevel.Level;
            var defenseValue = Mathf.Lerp (character.Df[0], character.Df[1], characterStatusGrade.DefenseStatusGrade) +
                               character.DfInc * characterLevel.Level;

            Debug.Log (
                $"Character name : {character.Name}, Hp : {healthValue}, Attack : {attackValue}, Ap : {apValue}, Df : {defenseValue}");
            dict[StatusType.Health].SetStatusValue (healthValue);
            dict[StatusType.Attack].SetStatusValue (attackValue);
            dict[StatusType.AbilityPoint].SetStatusValue (apValue);
            dict[StatusType.Defense].SetStatusValue (defenseValue);
            dict[StatusType.AttackSpeed].SetStatusValue (character.AttackSpeed);
            dict[StatusType.CriticalDamage].SetStatusValue (Constant.CriticalDamage);
            dict[StatusType.CriticalProbability].SetStatusValue (Constant.CriticalProbability);

            return dict;
        }


        #region EventMethods

        #endregion
    }
}