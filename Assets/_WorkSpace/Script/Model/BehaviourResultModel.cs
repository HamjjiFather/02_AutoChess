using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class BehaviourResultModel : ModelBase
    {
        #region Fields & Property

        public BattleState ResultState;

        public PositionModel TargetPosition;

        public List<BattleCharacterElement> TargetBattleCharacterElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods


        public void SetResultState (BattleState result)
        {
            ResultState = result;
        }

        
        public void SetTargetPosition (PositionModel target)
        {
            TargetPosition = target;
        }

        
        public void AddTargetCharacterElements (List<BattleCharacterElement> targets)
        {
            TargetBattleCharacterElements = targets;
        }

        #endregion
    }
}