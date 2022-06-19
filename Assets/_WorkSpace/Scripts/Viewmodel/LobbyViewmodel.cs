using System.Linq;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using MasterData;
using UniRx;
using UnityEngine;

namespace AutoChess
{
    public struct PlayerExpModel
    {
        public int Level;

        public int Exp;
        
        public PlayerLevel PlayerLevelTable;

        public bool IsMaxLevel;

        public float ExpProportion => IsMaxLevel ? 1 : Exp / PlayerLevelTable.ReqExp;
    }

    public class LobbyViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649


#pragma warning restore CS0649

        /// <summary>
        /// 플레이어 레벨.
        /// </summary>
        public PlayerExpModel PlayerExpModel;

        /// <summary>
        /// 플레이어 경험치 변동 커맨드.
        /// </summary>
        public readonly ReactiveCommand<PlayerExpModel> ChangePlayerExpModel = new ReactiveCommand<PlayerExpModel> ();

        #endregion


        public override void Initialize ()
        {
            var level = LocalDataHelper.GetPlayerBundle ().Level;
            PlayerExpModel.Level = level;
            PlayerExpModel.Exp = LocalDataHelper.GetPlayerBundle ().Exp;
            PlayerExpModel.PlayerLevelTable = TableDataManager.Instance.PlayerLevelDict.Values.SingleOrDefault (x => x.Level.Equals (level));
            PlayerExpModel.IsMaxLevel = TableDataManager.Instance.PlayerLevelDict.Values.Last ().Equals (PlayerExpModel.PlayerLevelTable);
        }


        public override void InitFinally()
        {
            base.InitFinally();
        }


        #region Subscribe

        #endregion


        #region Methods


        public void ChangePlayerExp (int value)
        {
            if (PlayerExpModel.IsMaxLevel)
                return;
            
            while (true)
            {
                var preCalcedValue = PlayerExpModel.Exp + value;
                var reqExp = PlayerExpModel.PlayerLevelTable.ReqExp;
                
                // 레벨업 요구 경험치보다 많음.
                if (preCalcedValue >= reqExp)
                {
                    var nextLevel = PlayerExpModel.Level += 1;
                    var nextLevelTable = TableDataManager.Instance.PlayerLevelDict.Values.SingleOrDefault (x => x.Level.Equals (nextLevel));

                    // 최대 레벨.
                    if (nextLevelTable is (PlayerLevel) default)
                    {
                        PlayerExpModel.IsMaxLevel = true;
                        PlayerExpModel.Exp = 0;
                        return;
                    }

                    var remainExp = preCalcedValue - reqExp;
                    PlayerExpModel.Exp = (int)remainExp;
                    PlayerExpModel.Level = nextLevel;
                    PlayerExpModel.PlayerLevelTable = nextLevelTable;
                    continue;
                }

                break;
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}