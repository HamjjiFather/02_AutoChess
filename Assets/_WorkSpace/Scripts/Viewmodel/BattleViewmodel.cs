using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using MasterData;
using ModestTree;
using UniRx;
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

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

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
            var stageData = BattleStage.Manager.Values.First ();
            var stageModel = new BattleStageModel (stageData);

            BattleStageModel = stageModel;
            _characterViewmodel.BattleCharacterModels.Where (x => x.IsAssigned)
                .ForEach ((model, index) => { _characterViewmodel.ResetCharacterPosition (index, model); });
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

            if (isWin)
                _adventureViewmodel.AddExp (BattleStageModel.StageData.RewardExp);
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
            battleStageModel.StageData.MonsterIndexes.Where (x => x != 0).ForEach ((monsterIndex, index) =>
            {
                var characterModel = new CharacterModel ();
                var characterData = Character.Manager.GetItemByIndex (monsterIndex);
                var characterLevel =
                    TableDataHelper.Instance.GetCharacterLevelByLevel (battleStageModel.StageData.MonsterLevels[index]);
                var statusGrade = MonsterStatusGradeValue ();
                var statusModel = _characterViewmodel.GetBaseStatusModel (characterData, characterLevel, statusGrade);
                var attackData = Skill.Manager.GetItemByIndex (characterData.AttackIndex);
                var skillData = Skill.Manager.GetItemByIndex (characterData.SkillIndex);

                characterModel.SetBaseData (characterData, attackData, skillData);
                characterModel.SetStatusModel (statusModel);
                characterModel.SetPositionModel (new PositionModel (battleStageModel.StageData.MonsterPosition[index]));
                characterModel.SetEmptyEquipmentModel ();
                characterModel.SetSide (CharacterSideType.AI);
                characterModel.SetScale (battleStageModel.StageData.MonsterScale[index]);

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
            PlayerCharacterElements.ForEach (x => x.EndBattle ());
            AiCharacterElements.ForEach (x => x.EndBattle ());
            _battleAiCharacterModels.Clear ();
            AiCharacterElements.Clear ();
        }


        private CharacterBundle.CharacterStatusGrade MonsterStatusGradeValue ()
        {
            return new CharacterBundle.CharacterStatusGrade
            {
                HealthStatusGrade = Constants.MONSTER_STATUS_GRADE_VALUE,
                AttackStatusGrade = Constants.MONSTER_STATUS_GRADE_VALUE,
                AbilityPointStatusGrade = Constants.MONSTER_STATUS_GRADE_VALUE,
                DefenseStatusGrade = Constants.MONSTER_STATUS_GRADE_VALUE
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
            return AllOfBattleCharacterElements.FirstOrDefault (x => x.ElementData == characterModel);
        }


        public bool CharacterPlacable (CharacterModel characterModel, PositionModel targetPosition)
        {
            var scaledPositions = PathFindingHelper.Instance.GetAroundPositionModel (_allLineModels,
                targetPosition, characterModel.CharacterScale - 1);

            return scaledPositions.All (position =>
            {
                return _allLineModels.ContainsKey (position.Column) &&
                       _allLineModels[position.Column].ContainIndex (position.Row) &&
                       !position.Equals (PathFindingHelper.Instance.EmptyPosition) &&
                       AllOfCharacterModels.Except (characterModel).All (x =>
                           !x.PositionModel.Equals (position) && !x.PredicatedPositionModel.Equals (position));
            });
        }
        
        
        #endregion


        #region EventMethods

        #endregion
    }
}