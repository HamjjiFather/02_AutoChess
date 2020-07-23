using System.Collections.Generic;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class BattleViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public Text stageText;
        
        public Button startButton;
        
        public LineElement[] lineElements;

        public VerticalLayoutGroup[] verticalLayoutGroups;

        public Transform characterParents;

        public Transform upperCharacterParents;

        public BattleViewParticleManagingModule particleManagingModule;
        
        /// <summary>
        /// 출전 캐릭터 디스플레이 영역.
        /// </summary>
        public BattleCharacterListArea battleCharacterListArea;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private StageViewmodel _stageViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        private List<BattleCharacterElement> _playerBattleCharacterElements = new List<BattleCharacterElement> ();

        /// <summary>
        /// 적 캐릭터.
        /// </summary>
        private List<BattleCharacterElement> _aiBattleCharacterElements = new List<BattleCharacterElement> ();

        #endregion


        #region UnityMethods

        public override void Initialize ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            ProjectContext.Instance.Container.BindInstance (particleManagingModule);
            
            _stageViewmodel.RegistReactiveCommand (async stageModel =>
            {
                SetStageText ();
                _battleViewmodel.SetBattleAiCharacter (stageModel);
                await SummonPlayerCharacter(true);
                await SummonEnemyCharacter ();
            });

            startButton.onClick.AddListener (ClickStartButton);
            base.Initialize ();
        }

        #endregion


        #region Methods

        private void SetStageText ()
        {
            stageText.text = $"{_stageViewmodel.LastStageIndex / 10 + 1} - {_stageViewmodel.LastStageIndex % 10 + 1}";
        }
        

        public LandElement GetLandElement (PositionModel positionModel)
        {
            return lineElements[positionModel.Column].landElements[positionModel.Row];
        }


        public override async UniTask ActiveLayout ()
        {
            verticalLayoutGroups.Foreach (x => x.SetLayoutVertical ());
            await base.ActiveLayout ();
        }


        /// <summary>
        /// 플레이어 캐릭터 소환.
        /// </summary>
        public async UniTask SummonPlayerCharacter (bool isNextStage = false)
        {
            if (!_characterViewmodel.IsDataChanged)
            {
                if (!isNextStage)
                {
                    await UniTask.CompletedTask;
                    return;
                }
            }

            _playerBattleCharacterElements.Foreach (x => x.PoolingObject ());
            _playerBattleCharacterElements.Clear ();

            _characterViewmodel.BattleCharacterModels.Foreach ((battlePlayer, index) =>
            {
                var landElement = lineElements[battlePlayer.PositionModel.Column]
                    .landElements[battlePlayer.PositionModel.Row];
                var characterElement = ObjectPoolingHelper.GetResources<BattleCharacterElement> (
                    ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof (BattleCharacterElement), landElement.characterPositionTransform);

                characterElement.SetInfoElement (battleCharacterListArea.battleCharacterInfoElements[index]);
                characterElement.SetElement (battlePlayer);

                _playerBattleCharacterElements.Add (characterElement);
                _battleViewmodel.AddPlayerBattleCharacterElement (characterElement);
            });
        }


        /// <summary>
        /// 적 캐릭터 소환.
        /// </summary>
        public async UniTask SummonEnemyCharacter ()
        {
            if (!_stageViewmodel.IsClearStage)
            {
                await UniTask.CompletedTask;
                return;
            }
            
            _battleViewmodel.BattleMonsterModels.Foreach (battleMonster =>
            {
                var landElement = lineElements[battleMonster.PositionModel.Column]
                    .landElements[battleMonster.PositionModel.Row];
                var characterElement = ObjectPoolingHelper.GetResources<BattleCharacterElement> (
                    ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof (BattleCharacterElement), landElement.characterPositionTransform);

                characterElement.SetElement (battleMonster);

                _aiBattleCharacterElements.Add (characterElement);
                _battleViewmodel.AddAiBattleCharacterElement (characterElement);
            });
        }


        private void ExcuteBattle ()
        {
            startButton.gameObject.SetActive (true);
        }

        #endregion


        #region EventMethods

        private void ClickStartButton ()
        {
            _battleViewmodel.SetExcuteBattleAction (ExcuteBattle);
            _playerBattleCharacterElements.Foreach (element =>
            {
                element.transform.SetParent (characterParents);
                element.StartBattle ();
            });
            _aiBattleCharacterElements.Foreach (element =>
            {
                element.transform.SetParent (characterParents);
                element.StartBattle ();
            });
            
            startButton.gameObject.SetActive (false);
        }

        #endregion
    }
}