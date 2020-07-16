using System.Collections.Generic;

namespace AutoChess
{
    public class FindTargetResultModel
    {
        public PositionModel TargetResultPosition;

        public List<PositionModel> AroundPositionModels = new List<PositionModel> ();

        public FindTargetResultModel (PositionModel targetResultPosition)
        {
            TargetResultPosition = targetResultPosition;
            AroundPositionModels.Add (targetResultPosition);
        }

        public void AddRangeAroundPositions (IEnumerable<PositionModel> positionModels)
        {
            AroundPositionModels.AddRange (positionModels);
        }
    }
}