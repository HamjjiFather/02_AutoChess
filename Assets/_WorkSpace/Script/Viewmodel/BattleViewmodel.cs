using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
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

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 스테이지 모델.
        /// </summary>
        private StageModel _nowStageModel = new StageModel ();
        public StageModel NowStageModel => _nowStageModel;
        
        /// <summary>
        /// 최신 스테이지 인덱스.
        /// </summary>
        private int _lastStageIndex = 0;
        public int LastStageIndex => _lastStageIndex;

        /// <summary>
        /// 전투 출현 적 몬스터.
        /// </summary>
        private readonly List<CharacterModel> _battleMonsterModels = new List<CharacterModel> ();
        public List<CharacterModel> BattleMonsterModels => _battleMonsterModels;
        
        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        private List<BattleCharacterElement> _playerCharacterElements = new List<BattleCharacterElement> ();
        public List<BattleCharacterElement> PlayerCharacterElements => _playerCharacterElements;

        /// <summary>
        /// AI 플레이어 캐릭터.
        /// </summary>
        private List<BattleCharacterElement> _aiCharacterElements = new List<BattleCharacterElement> ();
        public List<BattleCharacterElement> AiCharacterElements => _aiCharacterElements;

        #endregion


        public override void Initialize ()
        {
            InitializeLines ();
        }
        
        
        #region Methods

        /// <summary>
        /// 전투 시작.
        /// </summary>
        public void StartBattle ()
        {
            _characterViewmodel.BattleCharacterModels.Foreach (model =>
            {
                model.StartBattle ();
            });

            var stage = TableDataManager.Instance.StageDict[(int) DataType.Stage + _lastStageIndex];
            _nowStageModel.SetStageData (stage);

            // 적 AI 세팅.
            stage.MonsterIndexes.Foreach ((monsterIndex, index) =>
            {
                var characterModel = new CharacterModel ();
                var characterData = TableDataManager.Instance.CharacterDict[monsterIndex];
                var characterLevel = GameExtension.GetCharacterLevel (stage.MonsterLevels[index]);
                var statusGrade = MonsterStatusGradeValue ();
                var statusModel = _characterViewmodel.GetBaseStatusModel (characterData, characterLevel, statusGrade);

                characterModel.SetCharacterData (characterData);
                characterModel.SetStatusModel (statusModel);
                characterModel.SetPositionModel (new PositionModel (stage.MonsterPosition[index]));
                characterModel.SetEquipmentModel (EquipmentViewmodel.EmptyEquipmentModel);
                characterModel.SetSide (CharacterSideType.AI);

                characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);
                characterModel.StartBattle ();
                
                _battleMonsterModels.Add (characterModel);
            });
            
            _battleMonsterModels.Foreach (model =>
            {
                model.StartBattle ();
            });
        }
        
        CharacterBundle.CharacterStatusGrade MonsterStatusGradeValue ()
        {
            return new CharacterBundle.CharacterStatusGrade
            {
                HealthStatusGrade = Constant.MonsterStatusGradeValue,
                AttackStatusGrade = Constant.MonsterStatusGradeValue,
                DefenseStatusGrade = Constant.MonsterStatusGradeValue
            };
        }


        public void AddPlayerBattleCharacterElement (BattleCharacterElement battleCharacterElement)
        {
            _playerCharacterElements.Add (battleCharacterElement);
        }


        public void AddAiBattleCharacterElement (BattleCharacterElement battleCharacterElement)
        {
            _aiCharacterElements.Add (battleCharacterElement);
        }


        public BattleCharacterElement FindCharacterElement (CharacterModel characterModel)
        {
            return allOfBattleCharacterElements.First (x => x.ElementData == characterModel);
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}