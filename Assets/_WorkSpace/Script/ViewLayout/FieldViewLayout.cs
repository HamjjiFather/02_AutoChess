using System.Linq;
using KKSFramework.Navigation;
using UniRx.Async;
using Zenject;

namespace AutoChess
{
    public class FieldViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public LineElement[] lineElements;
        
        public FieldCharacterElement fieldCharacterElement;
        
#pragma warning disable CS0649

        [Inject]
        private FieldViewmodel _fieldViewmodel;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649


        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void Initialize ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            
            var fieldModels =
                _fieldViewmodel.SetFieldSizes (lineElements.Select (x => x.landElements.Length).ToArray ());
            
            fieldModels.SelectMany (x => x.Value)
                .ZipForEach (lineElements.SelectMany (x => x.landElements),
                (model, landElement) =>
                {
                    ((FieldLandElement)landElement).SetElement ((FieldModel)model);
                });


            var characterModel = _characterViewmodel.BattleCharacterModels.First ();
            fieldCharacterElement.SetElement (characterModel);
            var startElement = GetLandElement (_fieldViewmodel.StartPosition);
            fieldCharacterElement.transform.position = startElement.characterPositionTransform.transform.position;

            _fieldViewmodel.StartGame ();
            base.Initialize ();
        }
        
        
        public LandElement GetLandElement (PositionModel positionModel)
        {
            return lineElements[positionModel.Column].landElements[positionModel.Row];
        }


        public void ChangePosition (PositionModel targetPosition)
        {
            var result = _fieldViewmodel.FindMovingPositions (targetPosition);
            fieldCharacterElement.MoveTo (result).Forget();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}