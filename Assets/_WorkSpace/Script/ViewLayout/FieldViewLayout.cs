using System;
using System.Linq;
using KKSFramework.Navigation;
using UniRx.Async;
using Zenject;
using EnumActionToDict = System.Collections.Generic.Dictionary<System.Enum, System.Action>;

namespace AutoChess
{
    public class FieldViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public LineElement[] lineElements;
        
        public FieldCharacterElement fieldCharacterElement;
        
#pragma warning disable CS0649

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private BattleCharacterListArea _battleCharacterListArea;

        private readonly EnumActionToDict _fieldTypeAction = new EnumActionToDict();

        private bool _isMoving;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _fieldTypeAction.Add (FieldType.Battle, () => { _battleViewmodel.StartBattle (); });
            _fieldTypeAction.Add (FieldType.BossBattle, () => { _battleViewmodel.StartBattle (); });
        }

        #endregion


        #region Methods

        public override void Initialize ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            _battleCharacterListArea = ProjectContext.Instance.Container.Resolve<BattleCharacterListArea> ();
            
            var fieldModels =
                _adventureViewmodel.SetFieldSizes (lineElements.Select (x => x.landElements.Length).ToArray ());
            
            fieldModels.SelectMany (x => x.Value)
                .ZipForEach (lineElements.SelectMany (x => x.landElements),
                (model, landElement) =>
                {
                    ((FieldLandElement)landElement).SetElement ((FieldModel)model);
                });


            var characterModel = _characterViewmodel.BattleCharacterModels.First ();
            fieldCharacterElement.SetElement (characterModel);
            var startElement = GetLandElement (_adventureViewmodel.StartPosition);
            fieldCharacterElement.transform.position = startElement.characterPositionTransform.position;

            _adventureViewmodel.StartAdventure ();
            base.Initialize ();
        }


        public void EndBattle (bool isWin)
        {
            
        }


        public void SetFieldType (FieldModel fieldModel)
        {
            var fieldType = fieldModel.FieldType.Value;
            if (fieldType == FieldType.None)
                return;
            
            _adventureViewmodel.SetFieldType (fieldModel);
            _fieldTypeAction[fieldType]();
        }
        
        
        public LandElement GetLandElement (PositionModel positionModel)
        {
            return lineElements[positionModel.Column].landElements[positionModel.Row];
        }


        public async UniTask ChangePosition (PositionModel targetPosition)
        {
            if (_isMoving)
                return;
            
            _isMoving = true;
            var result = _adventureViewmodel.FindMovingPositions (targetPosition);
            await fieldCharacterElement.MoveTo (result);
            var lastPositionModel = result.FoundPositions.Last ();
            var fieldModel = _adventureViewmodel.GetFieldModel (lastPositionModel);
            SetFieldType (fieldModel);
            
            _isMoving = false;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}