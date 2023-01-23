using System.Collections.Generic;


namespace AutoChess
{
    public class FieldTargetResultModel
    {
        #region Fields & Property

        public bool IsConnected;
        
        public List<PositionModel> FoundPositions = new List<PositionModel> ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
    }
}