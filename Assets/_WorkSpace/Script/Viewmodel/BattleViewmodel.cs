using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using UniRx;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public enum CharacterSideType
    {
        Player,
        AI
    }

    public partial class BattleViewmodel : ViewModelBase
    {
        #region Fields & Property

        public bool IsDataChanged { get; set; }

        public ReactiveCommand<BattleStageModel> StartBattleCommand { get; private set; }

        public ReactiveCommand<bool> EndBattleCommand { get; private set; }

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 전투 출현 적 몬스터.
        /// </summary>
        private readonly List<CharacterModel> _battleAiCharacterModels = new List<CharacterModel> ();

        public List<CharacterModel> BattleAiCharacterModels => _battleAiCharacterModels;

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        public List<BattleCharacterElement> PlayerCharacterElements { get; } = new List<BattleCharacterElement> ();

        /// <summary>
        /// AI 캐릭터.
        /// </summary>
        public List<BattleCharacterElement> AiCharacterElements { get; } = new List<BattleCharacterElement> ();

        #endregion


        public override void Initialize ()
        {
            IsDataChanged = true;
            InitializeLines ();
        }


        #region Methods


        public void StartAdventure ()
        {
            StartBattleCommand = new ReactiveCommand<BattleStageModel> ();
            EndBattleCommand = new ReactiveCommand<bool> ();
        }

        
        public void EndAdventure ()
        {
            StartBattleCommand.DisposeSafe ();
            EndBattleCommand.DisposeSafe ();
        }


        public void StartBattle ()
        {
            var stageData = TableDataManager.Instance.BattleStageDict.First ().Value;
            var stageModel = new BattleStageModel (stageData);
            
            BattleStageModel = stageModel;
            _characterViewmodel.BattleCharacterModels.Foreach ((model, index) =>
            {
                _characterViewmodel.ResetCharacterPosition (index, model);
            });
            SetBattleAiCharacter (BattleStageModel);
            
            StartBattleCommand.Execute (BattleStageModel);
        }

        /// <summary>
        /// 전투 종료.
        /// </summary>
        public void EndBattle (bool isWin)
        {
            ResetCharacter ();
            EndBattleCommand.Execute (isWin);
        }
        
        
        public bool CheckCharacters (CharacterSideType sideType)
        {
            var allDead = GetAllOfEqualElements (sideType).All (x => x.BattleState == BattleState.Death);

            if (!allDead) return false;
            
            EndBattle (sideType == CharacterSideType.AI);
            return true;
        }

        
        /// <summary>
        /// AI 캐릭터 세팅.
        /// </summary>
        public void SetBattleAiCharacter (BattleStageModel battleStageModel)
        {
            // 적 AI 세팅.
            battleStageModel.StageData.MonsterIndexes.Foreach ((monsterIndex, index) =>
            {
                var characterModel = new CharacterModel ();
                var characterData = TableDataManager.Instance.CharacterDict[monsterIndex];
                var characterLevel =
                    TableDataHelper.Instance.GetCharacterLevelByLevel (battleStageModel.StageData.MonsterLevels[index]);
                var statusGrade = MonsterStatusGradeValue ();
                var statusModel = _characterViewmodel.GetBaseStatusModel (characterData, characterLevel, statusGrade);
                var attackData = TableDataManager.Instance.SkillDict[characterData.AttackIndex];
                var skillData = TableDataManager.Instance.SkillDict[characterData.SkillIndex];

                characterModel.SetBaseData (characterData, attackData, skillData);
                characterModel.SetStatusModel (statusModel);
                characterModel.SetPositionModel (new PositionModel (battleStageModel.StageData.MonsterPosition[index]));
                characterModel.SetEmptyEquipmentModel ();
                characterModel.SetSide (CharacterSideType.AI);

                characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.AbilityPoint)
                    .SetGradeValue (statusGrade.AbilityPointStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);

                _battleAiCharacterModels.Add (characterModel);
            });
        }


        public void ResetCharacter ()
        {
            PlayerCharacterElements.Foreach (x => x.EndBattle ());
            AiCharacterElements.Foreach (x => x.EndBattle ());
            _battleAiCharacterModels.Clear ();
            AiCharacterElements.Clear ();
        }


        CharacterBundle.CharacterStatusGrade MonsterStatusGradeValue ()
        {
            return new CharacterBundle.CharacterStatusGrade
            {
                HealthStatusGrade = Constant.MonsterStatusGradeValue,
                AttackStatusGrade = Constant.MonsterStatusGradeValue,
                AbilityPointStatusGrade = Constant.MonsterStatusGradeValue,
                DefenseStatusGrade = Constant.MonsterStatusGradeValue
            };
        }


        public void AddPlayerBattleCharacterElement (BattleCharacterElement battleCharacterElement)
        {
            PlayerCharacterElements.Add (battleCharacterElement);
        }


        public void AddAiBattleCharacterElement (BattleCharacterElement battleCharacterElement)
        {
            AiCharacterElements.Add (battleCharacterElement);
        }


        public BattleCharacterElement FindCharacterElement (CharacterModel characterModel)
        {
            return AllOfBattleCharacterElements.First (x => x.ElementData == characterModel);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}