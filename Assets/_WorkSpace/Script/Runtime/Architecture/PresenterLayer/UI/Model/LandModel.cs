namespace AutoChess
{
    public class LandModel
    {
        private PositionModel _landPosition;

        public PositionModel LandPosition => _landPosition;

        public LandModel (PositionModel landPosition)
        {
            _landPosition = landPosition;
        }
        
        public void SetPosition (PositionModel positionModel)
        {
            _landPosition = positionModel;
        }
    }
}