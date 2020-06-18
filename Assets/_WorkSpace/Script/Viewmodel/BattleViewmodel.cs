using System.Collections.Generic;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using Zenject;

namespace AutoChess
{
    public class BattleViewmodel : ViewModelBase
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

        #endregion


        public override void Initialize ()
        {

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

                characterModel.GetBaseStatusModel (StatusType.Health).SetGradeValue (statusGrade.HealthStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Attack).SetGradeValue (statusGrade.AttackStatusGrade);
                characterModel.GetBaseStatusModel (StatusType.Defense).SetGradeValue (statusGrade.DefenseStatusGrade);
                
                _battleMonsterModels.Add (characterModel);
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

        #endregion


        #region EventMethods

        #endregion
    }
}