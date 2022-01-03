using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using MasterData;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AutoChess
{
    public partial class CharacterManager : GameManagerBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private AbilityManager _abilityManager;

        [Inject]
        private EquipmentManager _equipmentManager;

        [Inject]
        private GameSetting _gameSetting;

#pragma warning restore CS0649

        private int _lastUniqueId;

        /// <summary>
        /// 모든 캐릭터.
        /// </summary>
        public List<CharacterData> AllCharacterModels { get; private set; } = new List<CharacterData> ();

        /// <summary>
        /// 사망한 모든 캐릭터.
        /// </summary>
        public List<CharacterData> AllDeathCharacterModels =>
            AllCharacterModels.Where (x => x.CharacterDeathInfo.Death).ToList ();

        /// <summary>
        /// 전투 참여 캐릭터 모델.
        /// </summary>
        public ReactiveCollection<CharacterData> BattleCharacterModels { get; } =
            new ReactiveCollection<CharacterData> (Enumerable.Repeat (new CharacterData (), 8));


        private readonly int[] _startCharacterIndexes =
        {
            0,
            1,
            2,
            3
        };

        public bool IsDataChanged;


        public bool IsDeathCharacterDataChanged;

        /// <summary>
        /// 고용 가능한 캐릭터.
        /// </summary>
        public List<CharacterData> AllEmployableCharacterModels { get; private set; } = new List<CharacterData> ();

        /// <summary>
        /// 고용 가능한 캐릭터가 출현하는 수량.
        /// </summary>
        public IntReactiveProperty EmployableCharacterCount;

        /// <summary>
        /// 새로 고용 가능한지 여부.
        /// </summary>
        public bool IsNewEmployment;

        #endregion


        public override void Initialize ()
        {
            // Initialize_Employ ();
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
                _startCharacterIndexes.Foreach ((index, arrayIndex) =>
                {
                    var newIdx = (int)DataType.Character + index;
                    var characterModel = NewCharacter (newIdx, AllCharacterModels);
                    BattleCharacterModels[arrayIndex] = characterModel;
                });

                SaveCharacterData ();
                SaveCharacterStatusGradeData ();
                SaveBattleCharacterData ();
                IsDataChanged = true;
                return;
            }

            characterBundle.CharacterUniqueIds.Foreach ((uid, arrayIndex) =>
            {
                var characterData = new CharacterData ();
                var abilityGrade = characterBundle.CharacterAbilityGrades[arrayIndex];
                var characterTable = TableDataManager.Instance.CharacterDict[characterBundle.CharacterIds[arrayIndex]];
                var characterLevelTable =
                    TableDataHelper.Instance.GetCharacterLevelByExp (characterBundle.CharacterExps[arrayIndex]);
                // var statusModel = GetBaseStatusModel (characterData, characterLevel, statusGrade);
                var attackData = TableDataManager.Instance.SkillDict[characterTable.AttackIndex];
                var skillData = TableDataManager.Instance.SkillDict[characterTable.SkillIndex];

                characterData.SetUniqueData (uid, characterBundle.CharacterExps[arrayIndex]);
                characterData.SetBaseData (characterTable, attackData, skillData);
                characterData.SetSide (CharacterSideType.Player);
                characterData.SetScale (1);

                // characterData.GetBaseStatusModel (StatusType.Health).SetGradeValue (abilityGrade.HealthStatusGrade);
                // characterData.GetBaseStatusModel (StatusType.Attack).SetGradeValue (abilityGrade.AttackStatusGrade);
                // characterData.GetBaseStatusModel (StatusType.AbilityPoint)
                //     .SetGradeValue (statusGrade.AbilityPointStatusGrade);
                // characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);ata (uid, characterBundle.CharacterExps[arrayIndex]);
                // characterModel.SetBaseData (characterData, attackData, skillData);
                // characterModel.SetStatusModel (statusModel, statusGrade);
                // characterModel.SetSide (CharacterSideType.Player);
                // characterModel.SetScale (1);
                //
                // characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                // characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                // characterModel.GetBaseStatusModel (StatusType.AbilityPoint)
                //     .SetGradeValue (statusGrade.AbilityPointStatusGrade);
                // characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);

                var equipmentIds = characterBundle.EquipmentUIds[arrayIndex];
                var equipmentModels =
                    equipmentIds.EquipmentUIds.Select (x => _equipmentManager.GetEquipmentModel (x));
                characterData.SetEquipmentModel (equipmentModels);

                AllCharacterModels.Add (characterData);
            });

            // characterBundle.BattleCharacterUniqueIds.Foreach ((uid, arrayIndex) =>
            // {
            //     if (uid == Constant.InvalidIndex)
            //         return;
            //     
            //     var characterModel = AllCharacterModels.Single (x => x.UniqueCharacterId.Equals (uid));
            //     BattleCharacterModels[arrayIndex] = characterModel;
            // });

            IsDataChanged = true;
        }


        #region Methods

        public void ResetCharacterPosition (int index, CharacterData characterData)
        {
            var positionData = LocalDataHelper.GetBattleCharacterPosition ();
            characterData.SetPositionModel (new PositionModel (positionData.Split ('/')[index]));
        }


        public void AddCharacterExps (int exp)
        {
            BattleCharacterModels.Foreach ((model, index) =>
            {
                model.AddExp (exp);
                var characterLevel = TableDataHelper.Instance.GetCharacterLevelByExp (model.Exp.Value);
                var statusGrade = model.AbilityGrade;
                // var statusModel = GetBaseStatusModel (model.CharacterTable, characterLevel, statusGrade);
                // model.SetStatusModel (statusModel);
                //
                // model.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                // model.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                // model.GetBaseStatusModel (StatusType.AbilityPoint).SetGradeValue (statusGrade.AbilityPointStatusGrade);
                // model.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);
            });

            LocalDataHelper.SaveCharacterExpData (AllCharacterModels.Select (x => x.Exp.Value).ToList ());
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
            LocalDataHelper.SaveBattleCharacterUidData (BattleCharacterModels.Select (x => x.UniqueCharacterId)
                .ToList ());
        }


        /// <summary>
        /// 캐릭터 획득.
        /// </summary>
        public CharacterData NewCharacter (int characterIndex, List<CharacterData> modelDatas)
        {
            var characterData = new CharacterData ();
            var characterTable = TableDataManager.Instance.CharacterDict[characterIndex];
            var gradeStatusValues = NewStatusGradeValue ();
            // var characterStatus = GetBaseStatusModel (characterData,
            // TableDataManager.Instance.CharacterLevelDict.Values.First (), gradeStatusValues);
            var attackData = TableDataManager.Instance.SkillDict[characterTable.AttackIndex];
            var skillData = TableDataManager.Instance.SkillDict[characterTable.SkillIndex];

            var uid = NewUniqueId ();
            characterData.SetUniqueData (uid, 0);
            characterData.SetBaseData (characterTable, attackData, skillData);
            
            var abilityContainer = new PlayableCharacterAbilityContainer ();
            var hpGradeRange = _abilityManager.GetAppliedGradeRange (characterTable.Hp);
            var attackGradeRange = _abilityManager.GetAppliedGradeRange (characterTable.At);
            var apGradeRange = _abilityManager.GetAppliedGradeRange (characterTable.Ap);
            var defGradeRange = _abilityManager.GetAppliedGradeRange (characterTable.Df);
            
            var baseAbilityAllocatorString = CharacterDefines.GetCharacterAbilityOwnerString (uid);
            
            abilityContainer.AddAbilityValue (AbilityType.Health, baseAbilityAllocatorString, hpGradeRange);
            abilityContainer.AddAbilityValue (AbilityType.Attack, baseAbilityAllocatorString, attackGradeRange);
            abilityContainer.AddAbilityValue (AbilityType.AbilityPoint, baseAbilityAllocatorString, apGradeRange);
            abilityContainer.AddAbilityValue (AbilityType.Defense, baseAbilityAllocatorString, defGradeRange);
            characterData.AbilityTypeContainer = abilityContainer;
            // characterModel.SetStatusModel (characterStatus);
            characterData.SetEmptyEquipmentModel ();
            SetBaseAbility ();
            modelDatas.Add (characterData);

            return characterData;

            CharacterBundle.CharacterAbilityGrade NewStatusGradeValue ()
            {
                return new CharacterBundle.CharacterAbilityGrade ();
            }

            void SetBaseAbility ()
            {
                // characterStatus.SetNewStatusGradeValue (StatusType.Health, gradeStatusValues.HealthStatusGrade);
                // characterStatus.SetNewStatusGradeValue (StatusType.Attack, gradeStatusValues.AttackStatusGrade);
                // characterStatus.SetNewStatusGradeValue (StatusType.AbilityPoint,
                //     gradeStatusValues.AbilityPointStatusGrade);
                // characterStatus.SetNewStatusGradeValue (StatusType.Defense, gradeStatusValues.DefenseStatusGrade);
            }
        }


        public void InBattleCharacter (int index, CharacterData characterData)
        {
            BattleCharacterModels[index] = characterData;
            SaveBattleCharacterData ();
        }


        public void SetNewEmployment ()
        {
            IsNewEmployment = true;
        }


        /// <summary>
        /// 고용 가능한 캐릭터를 새로 생성.
        /// </summary>
        public void NewEmployCharacterModels ()
        {
            if (!IsNewEmployment)
                return;

            AllEmployableCharacterModels.Clear ();

            EmployableCharacterCount.Value.ForWhile (() =>
            {
                var characterIndex = TableDataManager.Instance.CharacterDict.Values.Choice ().Id;
                NewCharacter (characterIndex, AllEmployableCharacterModels);
            });
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
        public void EmployCharacter (CharacterData characterData)
        {
            AllEmployableCharacterModels.Remove (characterData);
            AllCharacterModels.Add (characterData);
            EmployableCharacterCount.Value--;
            SaveCharacterData ();
            SaveCharacterStatusGradeData ();
            IsDataChanged = true;
        }


        /// <summary>
        /// 등용 가격 책정.
        /// </summary>
        public int GetEmployPrice (CharacterData characterData)
        {
            var gradeValue = ((int)characterData.StarGrade + 1) * 10;
            // var statusValue = characterData.StatusModel.Status.Values.Sum (x => x.GradeValue) / 10f;

            return 0; //gradeValue + (int)statusValue;
        }


        /// <summary>
        /// 등용 가격 책정.
        /// </summary>
        public string GetEmployPriceText (CharacterData characterData)
        {
            return $"{GetEmployPrice (characterData)}G";
        }

        #endregion


        #region GetMethods

        public CharacterData GetBattleCharacterModel (int index)
        {
            return BattleCharacterModels.Count > index ? BattleCharacterModels[index] : default;
        }


        public CharacterData GetCharacterModel (int uid)
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
            LocalDataHelper.SaveCharacterUniqueIdData (_lastUniqueId);
            return _gameSetting.baseCharacterUniqueId + _lastUniqueId;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}