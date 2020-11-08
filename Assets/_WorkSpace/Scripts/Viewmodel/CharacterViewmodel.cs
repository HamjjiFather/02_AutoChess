using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using MasterData;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AutoChess
{
    public partial class CharacterViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private EquipmentViewmodel _equipmentViewmodel;

        [Inject]
        private GameSetting _gameSetting;

#pragma warning restore CS0649

        private int _lastUniqueId;

        /// <summary>
        /// 모든 캐릭터.
        /// </summary>
        public List<CharacterModel> AllCharacterModels { get; private set; } = new List<CharacterModel> ();

        /// <summary>
        /// 사망한 모든 캐릭터.
        /// </summary>
        public List<CharacterModel> AllDeathCharacterModels => AllCharacterModels.Where (x => x.CharacterDeathInfo.Death).ToList ();

        /// <summary>
        /// 전투 참여 캐릭터 모델.
        /// </summary>
        public ReactiveCollection<CharacterModel> BattleCharacterModels { get; } =
            new ReactiveCollection<CharacterModel> (Enumerable.Repeat (new CharacterModel (), 8));

        

        private readonly int[] _startCharacterIndexes =
        {
            1000,
            1001,
            1002,
            1003
        };

        public bool IsDataChanged { get; set; }


        public bool IsDeathCharacterDataChanged { get; set; }


        public static BaseStatusModel EmptyStatusModel = new BaseStatusModel ();

        #endregion


        public override void Initialize ()
        {
            Initialize_Employ ();
        }


        public override void InitAfterLoadTableData ()
        {
            base.InitAfterLoadTableData ();
        }


        public override void InitAfterLoadLocalData ()
        {
            _lastUniqueId = LocalDataHelper.GetGameBundle ().LastCharacterUniqueId;

            var characterBundle = LocalDataHelper.GetCharacterBundle ();

            if (!characterBundle.CharacterUniqueIds.Any ())
            {
                _startCharacterIndexes.ForEach ((index, arrayIndex) =>
                {
                    var characterModel = NewCharacter (index, AllCharacterModels);
                    BattleCharacterModels[arrayIndex] = characterModel;
                });

                SaveCharacterData ();
                SaveCharacterStatusGradeData ();
                SaveBattleCharacterData ();
                IsDataChanged = true;
                return;
            }

            characterBundle.CharacterUniqueIds.ForEach ((uid, arrayIndex) =>
            {
                var characterModel = new CharacterModel ();
                var statusGrade = characterBundle.CharacterStatusGrades[arrayIndex];
                var characterData = Character.Manager.GetItemByIndex (characterBundle.CharacterIds[arrayIndex]);
                var characterLevel =
                    TableDataHelper.Instance.GetCharacterLevelByExp (characterBundle.CharacterExps[arrayIndex]);
                var statusModel = GetBaseStatusModel (characterData, characterLevel, statusGrade);
                var attackData = Skill.Manager.GetItemByIndex (characterData.AttackIndex);
                var skillData = Skill.Manager.GetItemByIndex (characterData.SkillIndex);

                characterModel.SetUniqueData (uid, characterBundle.CharacterExps[arrayIndex]);
                characterModel.SetBaseData (characterData, attackData, skillData);
                characterModel.SetStatusModel (statusModel, statusGrade);
                characterModel.SetSide (CharacterSideType.Player);
                characterModel.SetScale (1);

                characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.AbilityPoint)
                    .SetGradeValue (statusGrade.AbilityPointStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);

                var equipmentIds = characterBundle.EquipmentUIds[arrayIndex];
                var equipmentModels =
                    equipmentIds.EquipmentUIds.Select (x => _equipmentViewmodel.GetEquipmentModel (x));
                characterModel.SetEquipmentModel (equipmentModels);

                AllCharacterModels.Add (characterModel);
            });
            
            characterBundle.BattleCharacterUniqueIds.ForEach ((uid, arrayIndex) =>
            {
                if (uid == Constants.INVALID_INDEX)
                    return;
                
                var characterModel = AllCharacterModels.Single (x => x.UniqueCharacterId.Equals (uid));
                BattleCharacterModels[arrayIndex] = characterModel;
            });

            IsDataChanged = true;
        }


        #region Methods

        public void ResetCharacterPosition (int index, CharacterModel characterModel)
        {
            var positionData = LocalDataHelper.GetBattleCharacterPosition ();
            characterModel.SetPositionModel (new PositionModel (positionData.Split ('/')[index]));
        }


        public void AddCharacterExps (int exp)
        {
            BattleCharacterModels.ForEach ((model, index) =>
            {
                model.AddExp (exp);
                var characterLevel = TableDataHelper.Instance.GetCharacterLevelByExp (model.Exp.Value);
                var statusGrade = model.StatusGrade;
                var statusModel = GetBaseStatusModel (model.CharacterData, characterLevel, statusGrade);
                model.SetStatusModel (statusModel);

                model.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                model.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                model.GetBaseStatusModel (StatusType.AbilityPoint).SetGradeValue (statusGrade.AbilityPointStatusGrade);
                model.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);
            });

            LocalDataHelper.SaveCharacterExpData (AllCharacterModels.Select (x => x.Exp.Value).ToList ());
        }


        public void SaveCharacterData ()
        {
            LocalDataHelper.SaveCharacterIdData (
                AllCharacterModels.Select (x => x.UniqueCharacterId).ToList (),
                AllCharacterModels.Select (x => x.CharacterData.Index).ToList (),
                AllCharacterModels.Select (x => x.Exp.Value).ToList (),
                AllCharacterModels.Select (x =>
                        new CharacterBundle.CharacterEquipmentUIds (x.EquipmentStatusModel.EquipmentUId.ToList ()))
                    .ToList ());
        }

        public void SaveCharacterStatusGradeData ()
        {
            LocalDataHelper.SaveCharacterStatusGradeData (
                AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Health)).ToList (),
                AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Attack)).ToList (),
                AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.AbilityPoint)).ToList (),
                AllCharacterModels.Select (x => x.StatusModel.GetStatusGradeValue (StatusType.Defense)).ToList ());
        }


        public void SaveBattleCharacterData ()
        {
            LocalDataHelper.SaveBattleCharacterUidData (BattleCharacterModels.Select (x => x.UniqueCharacterId)
                .ToList ());
        }


        /// <summary>
        /// 캐릭터 획득.
        /// </summary>
        public CharacterModel NewCharacter (int characterIndex, List<CharacterModel> modelDatas)
        {
            var characterModel = new CharacterModel ();
            var characterData = Character.Manager.GetItemByIndex (characterIndex);
            var gradeStatusValues = NewStatusGradeValue ();
            var characterStatus = GetBaseStatusModel (characterData,
                CharacterLevel.Manager.Values.First (), gradeStatusValues);
            var attackData = Skill.Manager.GetItemByIndex (characterData.AttackIndex);
            var skillData = Skill.Manager.GetItemByIndex (characterData.SkillIndex);

            characterModel.SetUniqueData (NewUniqueId (), 0);
            characterModel.SetBaseData (characterData, attackData, skillData);
            characterModel.SetStatusModel (characterStatus);
            characterModel.SetEmptyEquipmentModel ();
            SetStatusGradeValue ();
            modelDatas.Add (characterModel);

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
                characterStatus.SetNewStatusGradeValue (StatusType.AbilityPoint,
                    gradeStatusValues.AbilityPointStatusGrade);
                characterStatus.SetNewStatusGradeValue (StatusType.Defense, gradeStatusValues.DefenseStatusGrade);
            }
        }


        public void InBattleCharacter (int index, CharacterModel characterModel)
        {
            BattleCharacterModels[index] = characterModel;
            SaveBattleCharacterData ();
        }

        #endregion


        #region GetMethods

        public CharacterModel GetBattleCharacterModel (int index)
        {
            return BattleCharacterModels.Count > index ? BattleCharacterModels[index] : default;
        }


        public CharacterModel GetCharacterModel (int uid)
        {
            return AllCharacterModels[uid];
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
            enums?.Skip (1).ForEach (statustype =>
            {
                dict.Add (statustype,
                    new BaseStatusModel (TableDataHelper.Instance.GetStatus (statustype)));
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
            dict[StatusType.CriticalDamage].SetStatusValue (Constants.CRITICAL_DAMAGE);
            dict[StatusType.CriticalProbability].SetStatusValue (Constants.CRITICAL_PROBABTILITY);

            return dict;
        }


        #region EventMethods

        #endregion
    }
}