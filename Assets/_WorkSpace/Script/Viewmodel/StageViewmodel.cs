using AutoChess.Helper;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using UnityEngine;

namespace AutoChess
{
    public class StageViewmodel : ViewModelBase<StageModel>
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private int _lastStageIndex = 0;
        public int LastStageIndex => _lastStageIndex;

        protected override StageModel ModelBase { get; set; }

        public int Exp => ModelBase.StageData.RewardExp;

        public bool IsClearStage { get; set; }

        #endregion


        public override void Initialize ()
        {
            IsClearStage = true;
        }


        public override void InitAfterLoadLocalData ()
        {
            base.InitAfterLoadLocalData ();
            _lastStageIndex = LocalDataHelper.GetGameBundle ().LastStageIndex;
            var stageData = TableDataHelper.Instance.GetTableData<Stage> (DataType.Stage, _lastStageIndex);
            ModelBase = new StageModel (stageData);
        }


        #region Methods


        public void RetryStage ()
        {
            ModelChangedCommand.Execute (ModelBase);
        }
        

        public void SetNextStage ()
        {
            _lastStageIndex++;
            IsClearStage = true;
            LocalDataHelper.SaveStageIndex (_lastStageIndex);
            Debug.Log ($"Next Stage :{_lastStageIndex}");
            var stageData = TableDataHelper.Instance.GetTableData<Stage> (DataType.Stage, _lastStageIndex);
            ModelBase = new StageModel (stageData);
            ModelChangedCommand.Execute (ModelBase);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}