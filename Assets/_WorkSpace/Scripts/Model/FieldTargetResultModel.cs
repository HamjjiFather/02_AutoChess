using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class FieldTargetResultModel : ModelBase
    {
        #region Fields & Property

        public bool IsConnected;
        
        public List<PositionModel> FoundPositions = new List<PositionModel> ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
    }
}