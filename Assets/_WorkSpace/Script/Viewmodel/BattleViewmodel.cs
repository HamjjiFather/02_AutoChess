using System.Collections.Generic;
using System.Linq;
using AutoChess.Helper;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
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

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private StageViewmodel _stageViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 전투 출현 적 몬스터.
        /// </summary>
        private readonly List<CharacterModel> _battleMonsterModels = new List<CharacterModel> ();
        public List<CharacterModel> BattleMonsterModels => _battleMonsterModels;

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        public List<BattleCharacterElement> PlayerCharacterElements { get; } = new List<BattleCharacterElement> ();

        public List<BattleCharacterElement> AiCharacterElements { get; } = new List<BattleCharacterElement> ();

        private UnityAction _excuteBattleAction;

        #endregion


        public override void Initialize ()
        {
            IsDataChanged = true;
            InitializeLines ();
        }
        
        
        #region Methods


        public bool CheckCharacters (CharacterSideType sideType)
        {
            var result = GetAllOfEqualElements (sideType).All (x => x.BattleState == BattleState.Death);

            if (result)
            {
                var isClear = sideType == CharacterSideType.AI;
                
                ClearAiCharacter ();
                _characterViewmodel.ResetBattleCharacters (isClear ? _stageViewmodel.Exp : 0);
                
                if (isClear)
                {
                    _stageViewmodel.SetNextStage ();
                }
                else
                {
                    _stageViewmodel.RetryStage ();
                }
                _excuteBattleAction.Invoke ();
            }
            
            return result;
        }


        public void SetExcuteBattleAction (UnityAction excuteBattleAction)
        {
            _excuteBattleAction = excuteBattleAction;
        }
        

        /// <summary>
        /// AI 캐릭터 세팅.
        /// </summary>
        public void SetBattleAiCharacter (StageModel stageModel)
        {
            _characterViewmodel.BattleCharacterModels.Foreach (model =>
            {
                model.ResetState ();
            });

            // 적 AI 세팅.
            stageModel.StageData.MonsterIndexes.Foreach ((monsterIndex, index) =>
            {
                var characterModel = new CharacterModel ();
                var characterData = TableDataManager.Instance.CharacterDict[monsterIndex];
                var characterLevel = TableDataHelper.Instance.GetCharacterLevelByLevel (stageModel.StageData.MonsterLevels[index]);
                var statusGrade = MonsterStatusGradeValue ();
                var statusModel = _characterViewmodel.GetBaseStatusModel (characterData, characterLevel, statusGrade);
                var attackData = TableDataManager.Instance.SkillDict[characterData.AttackIndex];
                var skillData = TableDataManager.Instance.SkillDict[characterData.SkillIndex];

                characterModel.SetBaseData (characterData, attackData, skillData);
                characterModel.SetStatusModel (statusModel);
                characterModel.SetPositionModel (new PositionModel (stageModel.StageData.MonsterPosition[index]));
                characterModel.SetEmptyEquipmentModel ();
                characterModel.SetSide (CharacterSideType.AI);

                characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.AbilityPoint).SetGradeValue (statusGrade.AbilityPointStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);
                characterModel.ResetState ();
                
                _battleMonsterModels.Add (characterModel);
            });
        }


        public void ClearAiCharacter ()
        {
            PlayerCharacterElements.Foreach (x => x.EndBattle ());
            AiCharacterElements.Foreach (x => x.EndBattle ());
            _battleMonsterModels.Clear ();
            PlayerCharacterElements.Clear ();
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