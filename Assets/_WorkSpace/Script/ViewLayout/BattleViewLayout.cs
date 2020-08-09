using System;
using System.Collections.Generic;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UniRx;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using KKSFramework;

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
        
#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        private readonly List<BattleCharacterElement> _playerBattleCharacterElements = new List<BattleCharacterElement> ();

        /// <summary>
        /// 적 캐릭터.
        /// </summary>
        private readonly List<BattleCharacterElement> _aiBattleCharacterElements = new List<BattleCharacterElement> ();
        
        /// <summary>
        /// 전투 참여 캐릭터.
        /// </summary>
        private BattleCharacterListArea _battleCharacterListArea;
        
        private bool _atAfterSummon;

        private IDisposable _endBattleDisposable;
        
        #endregion


        #region UnityMethods

        public override void Initialize ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            ProjectContext.Instance.Container.BindInstance (particleManagingModule);
            _battleCharacterListArea = ProjectContext.Instance.Container.Resolve<BattleCharacterListArea> ();

            startButton.onClick.AddListener (ClickStartButton);
            base.Initialize ();
        }

        #endregion


        #region Methods
        
        public override async UniTask ActiveLayout ()
        {
            verticalLayoutGroups.Foreach (x => x.SetLayoutVertical ());
            await base.ActiveLayout ();
        }


        public async UniTask StartBattle ()
        {
            SummonPlayerCharacter();
            await SummonEnemyCharacter ();
        }
        
        
        private void EndBattle (bool isWin)
        {
            startButton.gameObject.SetActive (true);
            _endBattleDisposable.DisposeSafe ();
        }
        
        
        /// <summary>
        /// 플레이어 캐릭터 소환.
        /// </summary>
        public void SummonPlayerCharacter ()
        {
            if (_atAfterSummon)
            {
                _playerBattleCharacterElements.Foreach (element =>
                {
                    var landElement = GetLandElement (element.ElementData.PositionModel);
                    element.transform.position = landElement.characterPositionTransform.position;
                });
                return;
            }

            _atAfterSummon = true;
            _characterViewmodel.BattleCharacterModels.Foreach ((battlePlayer, index) =>
            {
                var landElement = GetLandElement (battlePlayer.PositionModel);
                var characterElement = ObjectPoolingHelper.GetResources<BattleCharacterElement> (
                    ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof (BattleCharacterElement), landElement.characterPositionTransform);

                characterElement.SetInfoElement (_battleCharacterListArea.battleCharacterInfoElements[index]);
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
            _battleViewmodel.BattleAiCharacterModels.Foreach (battleMonster =>
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

        #endregion


        #region EventMethods

        private void ClickStartButton ()
        {
            _endBattleDisposable = _battleViewmodel.EndBattleCommand.Subscribe (EndBattle);
            
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
        
        
        public LandElement GetLandElement (PositionModel positionModel)
        {
            return lineElements[positionModel.Column].landElements[positionModel.Row];
        }
    }
}