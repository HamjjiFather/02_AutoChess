using System.Collections.Generic;

namespace AutoChess
{
    public class BattleTargetResultModel
    {
        public PositionModel TargetResultPosition;

        public List<PositionModel> AroundPositionModels = new List<PositionModel> ();

        public BattleTargetResultModel (PositionModel targetResultPosition)
        {
            TargetResultPosition = targetResultPosition;
            AroundPositionModels.Add (targetResultPosition);
        }

        public void AddRangeAroundPositions (IEnumerable<PositionModel> positionModels)
        {
            AroundPositionModels.AddRange (positionModels);
        }
    }

    public class FindTargetLinkResultModel
    {
        public List<PositionModel> LinkPositionModels = new List<PositionModel> ();
    }
}