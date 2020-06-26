using System.Collections.Generic;
using System.Linq;
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

        public LineElement[] lineElements;

        public BattleCharacterListArea battleCharacterListArea;

        public Button startButton;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private int _lastBattleIndex = -1;

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        private List<BattleCharacterElement> _playerBattleCharacterElements = new List<BattleCharacterElement> ();

        /// <summary>
        /// 적 캐릭터.
        /// </summary>
        private List<BattleCharacterElement> _monsterBattleCharacterElements = new List<BattleCharacterElement> ();

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            startButton.onClick.AddListener (ClickStartButton);
        }

        #endregion


        #region Methods


        public LandElement GetLandElement (PositionModel positionModel)
        {
            return lineElements[positionModel.Column].landElements[positionModel.Row];
        }
        

        public override async UniTask ActiveLayout ()
        {
            ProjectContext.Instance.Container.BindInstance (this);

            battleCharacterListArea.SetCharacterList (_characterViewmodel.BattleCharacterModels);
            await SummonPlayerMonster ();
            await SummonEnemyMonster ();
            await base.ActiveLayout ();
        }


        /// <summary>
        /// 플레이어 캐릭터 소환.
        /// </summary>
        public async UniTask SummonPlayerMonster ()
        {
            if (!_characterViewmodel.IsDataChanged)
            {
                await UniTask.CompletedTask;
                return;
            }

            _playerBattleCharacterElements.Foreach (x => x.PoolingObject ());
            _playerBattleCharacterElements.Clear ();

            _characterViewmodel.BattleCharacterModels.Foreach (battlePlayer =>
            {
                var landElement = lineElements[battlePlayer.PositionModel.Column]
                    .landElements[battlePlayer.PositionModel.Row];
                var characterElement = ObjectPoolingHelper.GetResources<BattleCharacterElement> (ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof (BattleCharacterElement), landElement.characterPositionTransform);
                
                characterElement.SetElement (battlePlayer);

                _playerBattleCharacterElements.Add (characterElement);
                _battleViewmodel.AddPlayerBattleCharacterElement (characterElement);
            });
        }


        /// <summary>
        /// 적 캐릭터 소환.
        /// </summary>
        public async UniTask SummonEnemyMonster ()
        {
            if (_lastBattleIndex.Equals (_battleViewmodel.LastStageIndex))
            {
                await UniTask.CompletedTask;
                return;
            }

            _battleViewmodel.StartBattle ();
            _battleViewmodel.BattleMonsterModels.Foreach (battleMonster =>
            {
                var landElement = lineElements[battleMonster.PositionModel.Column]
                    .landElements[battleMonster.PositionModel.Row];
                var res = ResourcesLoadHelper.GetResources<BattleCharacterElement> (ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof (BattleCharacterElement));

                var characterElement =
                    res.InstantiateObject<BattleCharacterElement> (landElement.characterPositionTransform);
                characterElement.GetComponent<RectTransform> ().SetInstantiateTransform ();
                characterElement.SetElement (battleMonster);
                
                _monsterBattleCharacterElements.Add (characterElement);
                _battleViewmodel.AddAiBattleCharacterElement (characterElement);
            });
        }

        #endregion


        #region EventMethods

        private void ClickStartButton ()
        {
            _lastBattleIndex = _battleViewmodel.LastStageIndex;

            _playerBattleCharacterElements.Foreach (x => x.StartBattle ());
            _monsterBattleCharacterElements.Foreach (element => { element.StartBattle (); });
        }

        #endregion
    }
}