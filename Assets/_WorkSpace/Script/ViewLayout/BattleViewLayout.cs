using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UniRx;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class BattleViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private LineElement[] _lineElements;

        [Resolver]
        private Transform _characterParents;

        [Resolver]
        private Transform _upperCharacterParents;

        [Resolver]
        private BattleViewParticleManagingModule _particleManagingModule;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 플레이어 캐릭터.
        /// </summary>
        private readonly List<BattleCharacterElement> _playerBattleCharacterElements =
            new List<BattleCharacterElement> ();

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
            ProjectContext.Instance.Container.BindInstance (_particleManagingModule);
            _battleCharacterListArea = ProjectContext.Instance.Container.Resolve<BattleCharacterListArea> ();
            CreateField ().Forget ();

            base.Initialize ();
        }

        #endregion


        #region Methods

        public override async UniTask ActiveLayout ()
        {
            _lineElements.ForEach (x => x.MyVerticalLayoutGroup.SetLayoutVertical ());
            await base.ActiveLayout ();
        }


        public async UniTask StartBattle ()
        {
            await UniTask.WaitForEndOfFrame ();
            SummonPlayerCharacter ();
            await SummonEnemyCharacter ();

            _endBattleDisposable = _battleViewmodel.EndBattleCommand.Subscribe (EndBattle);

            _playerBattleCharacterElements.ForEach (element =>
            {
                element.transform.SetParent (_characterParents);
                element.StartBattle ();
            });
            _aiBattleCharacterElements.ForEach (element =>
            {
                element.transform.SetParent (_characterParents);
                element.StartBattle ();
            });
        }

        /// <summary>
        /// 전투 필드 생성.
        /// </summary>
        private async UniTask CreateField ()
        {
            var fieldScale = Array.ConvertAll (Constant.BattleFieldScale.Split (','), int.Parse);
            var fieldElement = await ResourcesLoadHelper.LoadResourcesAsync<LandElement> (
                ResourceRoleType.Bundles, ResourcesType.Element, nameof (LandElement));

            _lineElements.ForEach ((element, i) =>
            {
                var count = fieldScale[i];
                while (count > 0)
                {
                    var obj = fieldElement.InstantiateObject<LandElement> (element.transform);
                    obj.transform.SetLocalReset ();
                    element.AddLandElement (obj);
                    count--;
                }
            });
        }


        private void EndBattle (bool isWin)
        {
            _endBattleDisposable.DisposeSafe ();
        }


        /// <summary>
        /// 플레이어 캐릭터 소환.
        /// </summary>
        public void SummonPlayerCharacter ()
        {
            if (_atAfterSummon)
            {
                _playerBattleCharacterElements.ForEach (element =>
                {
                    var landElement = GetLandElement (element.ElementData.PositionModel);
                    element.transform.position = landElement.transform.position;
                });
                return;
            }

            _atAfterSummon = true;
            _characterViewmodel.BattleCharacterModels.Where (x => x.IsAssigned).ForEach ((battlePlayer, index) =>
            {
                var landElement = GetLandElement (battlePlayer.PositionModel);
                var characterElement = ObjectPoolingHelper.GetResources<BattleCharacterElement> (
                    ResourceRoleType.Bundles,
                    ResourcesType.Element, nameof (BattleCharacterElement), landElement.transform);

                characterElement.SetInfoElement (_battleCharacterListArea.GetElement(index));
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
            _battleViewmodel.BattleAiCharacterModels.ForEach (battleMonster =>
            {
                var landElement = _lineElements[battleMonster.PositionModel.Column]
                    .GetLandElement (battleMonster.PositionModel.Row);
                var characterElement = ObjectPoolingHelper.GetResources<BattleCharacterElement> (
                    ResourceRoleType.Bundles,
                    ResourcesType.Element, nameof (BattleCharacterElement), landElement.transform);

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

            _playerBattleCharacterElements.ForEach (element =>
            {
                element.transform.SetParent (_characterParents);
                element.StartBattle ();
            });
            _aiBattleCharacterElements.ForEach (element =>
            {
                element.transform.SetParent (_characterParents);
                element.StartBattle ();
            });
        }

        #endregion


        public LandElement GetLandElement (PositionModel positionModel)
        {
            return _lineElements[positionModel.Column].GetLandElement (positionModel.Row);
        }
    }
}