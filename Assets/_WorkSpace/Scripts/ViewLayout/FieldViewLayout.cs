using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using BaseFrame.Navigation;
using KKSFramework;
using KKSFramework.Navigation;
using UniRx;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.ResourcesLoad;
using KKSFramework.UI;
using MasterData;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using EnumActionToDict = System.Collections.Generic.Dictionary<System.Enum, System.Action<AutoChess.FieldLandElement>>;

namespace AutoChess
{
    public class FieldViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private FieldViewLayoutRewardArea _fieldViewLayoutRewardArea;
        
        [Resolver]
        private LineElement[] _lineElements;

        public LineElement[] LineElements => _lineElements;

        [Resolver]
        private Text _adventureCountText;

        [Resolver]
        private ButtonExtension _inspectButton;

        [Resolver]
        private ButtonExtension _formationButton;

        [Resolver]
        private ScrollSnap _scrollSnap;

        [Resolver]
        private ScrollRect _fieldScroll;

        [Resolver]
        private FieldCharacterElement _fieldCharacterElement;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private BattleCharacterListArea _battleCharacterListArea;

        private FieldLandElement _nowLandElement;

        private readonly EnumActionToDict _fieldTypeActionBefore = new EnumActionToDict ();

        private readonly EnumActionToDict _fieldTypeActionAfter = new EnumActionToDict ();

        private bool _isMoving;


        private List<IDisposable> _disposables = new List<IDisposable> ();

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _inspectButton.AddListener (ClickInspectButton);
            _formationButton.AddListener (ClickFormationButton);
            
            _fieldTypeActionAfter.Add (FieldSpecialType.Battle, _ => { Debug.Log ("전투가 시작되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.Reward, _ =>
            {
                Debug.Log ("보상을 얻었습니다.");
            });
            _fieldTypeActionAfter.Add (FieldSpecialType.BossBattle, _ => { Debug.Log ("전투가 시작되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverSmall, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverMedium, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverLarge, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.Insightful, fieldLandElement =>
            {
                var insightFulPosition = _adventureViewmodel.GetInsightfulPosition ();
                var insightFulLand = GetLandElement (insightFulPosition.First ());
                _scrollSnap.SnapToAsync (insightFulLand.transform).Forget ();
            });
            
            _fieldTypeActionAfter.Add (FieldSpecialType.Store, _ =>
            {
                Debug.Log ("상점을 이용함.");
            });
            
            _fieldTypeActionAfter.Add (FieldSpecialType.Smith, _ =>
            {
                Debug.Log ("대장간을 이용함.");
            });
            
            _fieldTypeActionAfter.Add (FieldSpecialType.Bar, _ =>
            {
                Debug.Log ("새 파티를 영입하면 됨.");
            });
            
            _fieldTypeActionAfter.Add (FieldSpecialType.Exit, _ =>
            {
                var parameter = new Parameters
                {
                    {
                        "Model",
                        new NextFloorConfirmPopup.Model
                        {
                            confirmAction = ConfirmNextFloor
                        }
                    }
                };
                
                TreeNavigationHelper.PushPopup (nameof(NextFloorConfirmPopup), parameter);
            });
        }

        #endregion


        #region Methods

        protected override void OnInitialized ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            _battleCharacterListArea = ProjectContext.Instance.Container.Resolve<BattleCharacterListArea> ();
            base.OnInitialized ();
        }

        
        public async UniTask StartAdventure ()
        {
            var fieldScale = Array.ConvertAll (Constants.FIELD_SCALE.Split (','), int.Parse);
            var adventureModel = await _adventureViewmodel.StartAdventure (fieldScale);
            Subscribe ();

            await CreateField ();
            adventureModel.AllFieldModel
                .SelectMany (x => x.Value)
                .ZipForEach (_lineElements.SelectMany (x => x.LandElements),
                    (model, landElement) => { ((FieldLandElement) landElement).Element (model); });

            // 레이아웃 배치를 위해 대기.
            await UniTask.WaitForEndOfFrame ();
            
            _fieldViewLayoutRewardArea.SetArea (_adventureViewmodel.AdventureRewardModel);

            var characterModel = _characterViewmodel.BattleCharacterModels.First ();
            _fieldCharacterElement.SetElement (characterModel);
            var startElement = GetLandElement (adventureModel.StartField.LandPosition);
            _fieldCharacterElement.transform.position = startElement.transform.position;
            _nowLandElement = startElement as FieldLandElement;
            _scrollSnap.SnapTo (startElement.GetComponent<RectTransform> ());

            void Subscribe ()
            {
                _disposables.Add(_adventureViewmodel.EndAdventureCommand.Subscribe(_ =>
                {
                    EndAdventure ();
                }));
                _disposables.Add (_adventureViewmodel.AdventureModel.AdventureCount.Subscribe (count =>
                {
                    _adventureCountText.text = count.ToString ();
                }));
            }


            async UniTask CreateField ()
            {
                var fieldElement = await ResourcesLoadHelper.LoadResourcesAsync <FieldLandElement> (
                    ResourceRoleType.Bundles, ResourcesType.Element, nameof (FieldLandElement));
                
                _lineElements.ForEach ((element, i) =>
                {
                    var count = fieldScale[i];
                    while (count > 0)
                    {
                        var obj = fieldElement.InstantiateObject<FieldLandElement> (element.transform);
                        obj.transform.SetLocalReset ();
                        element.AddLandElement (obj);
                        count--;
                    }
                });
            }
        }


        /// <summary>
        /// 탐험 종료.
        /// </summary>
        public void EndAdventure ()
        {
            _disposables.ForEach (x => x.DisposeSafe ());
            _disposables.Clear ();
            Debug.Log ("end adventure");
        }

        
        /// <summary>
        /// 다음 층.
        /// </summary>
        private void ConfirmNextFloor ()
        {
            NextFloor ().Forget ();
        }


        /// <summary>
        /// 다음층 처리.
        /// </summary>
        /// <returns></returns>
        private async UniTask NextFloor ()
        {
            await TreeNavigationHelper.TransitionActionAsync (TransitionType.Fade, OnBeween);

            void OnBeween ()
            {
                _adventureViewmodel.NewField ();
                _fieldViewLayoutRewardArea.SetArea (_adventureViewmodel.AdventureRewardModel);

                var startElement = GetLandElement (_adventureViewmodel.AdventureModel.StartField.LandPosition);
                _fieldCharacterElement.transform.position = startElement.transform.position;
                _scrollSnap.SnapTo (startElement.GetComponent<RectTransform> ());
            }
        }
        

        /// <summary>
        /// 보상 확인.
        /// </summary>
        private void ConfirmRewardAdventure ()
        {
            _adventureViewmodel.EndAdventure ();
            var param = new Parameters 
            {
                {
                    "action",
                    (Action)ExitAdventure
                }
            };
            TreeNavigationHelper.PushPopup (nameof(AdventureResultPopup), param);
            Debug.Log ("confirm reward adventure");
        }


        /// <summary>
        /// 탐험에서 빠져나옴.
        /// </summary>
        private void ExitAdventure ()
        {
            Debug.Log ("exit adventure");
            TreeNavigationHelper.PushPage (nameof(GamePage));
        }


        public async UniTask DisposeViewLayout ()
        {
            var tasks = _lineElements.Select (element => element.Clear ());
            await UniTask.WhenAll (tasks);
        }


        public void EndBattle (bool isWin)
        {
        }


        /// <summary>
        /// 이동 전 클릭 시 이벤트.
        /// </summary>
        public void EventFieldTypeBeforeMoving (FieldLandElement fieldLandElement)
        {
            var fieldType = fieldLandElement.FieldModel.FieldSpecialType.Value;
            if (fieldType == FieldSpecialType.None)
                return;

            _adventureViewmodel.FieldType (fieldLandElement.FieldModel);

            if (_fieldTypeActionBefore.ContainsKey (fieldType))
                _fieldTypeActionBefore[fieldType] (fieldLandElement);
        }


        /// <summary>
        /// 이동 후 클릭 시 이벤트.
        /// </summary>
        public void EventByFieldTypeAfterMoving (FieldLandElement fieldLandElement)
        {
            var fieldType = fieldLandElement.FieldModel.FieldSpecialType.Value;
            if (fieldType == FieldSpecialType.None)
                return;

            _adventureViewmodel.FieldType (fieldLandElement.FieldModel);

            if (_fieldTypeActionAfter.ContainsKey (fieldType))
                _fieldTypeActionAfter[fieldType] (fieldLandElement);
        }


        public LandElement GetLandElement (PositionModel positionModel)
        {
            return _lineElements[positionModel.Column].GetLandElement (positionModel.Row);
        }


        public async UniTask ChangePosition (FieldLandElement fieldLandElement)
        {
            if (_isMoving)
                return;

            if (_adventureViewmodel.IsConnectByMovable (_nowLandElement.FieldModel, fieldLandElement.FieldModel))
            {
                _isMoving = true;
                var result = _adventureViewmodel.FindMovingPositions (fieldLandElement.FieldModel);
                await _fieldCharacterElement.MoveTo (result);
                _nowLandElement = fieldLandElement;
                Debug.Log ("Complete Movement");

                EventByFieldTypeAfterMoving (fieldLandElement);

                _isMoving = false;
                return;
            }

            Debug.Log ("경로가 막힘.");
        }

        #endregion


        #region EventMethods

        private void ClickInspectButton ()
        {
            var aroundPosition =
                PathFindingHelper.Instance.GetAroundPositionModel (_adventureViewmodel.AdventureModel.AllFieldModel,
                    _adventureViewmodel.NowPosition);
        }

        private void ClickFormationButton ()
        {
            TreeNavigationHelper.PushPopup (nameof(FormationPopup));
        }
        
        #endregion
    }
}