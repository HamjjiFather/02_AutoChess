using KKSFramework;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public partial class AdventureViewmodel
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Inject]
        private GameSetting _gameSetting;

        [Inject]
        private EquipmentViewmodel _equipmentViewmodel;

#pragma warning restore CS0649

        private int _exp;
        

        private AdventureRewardModel _adventureRewardModel;

        public AdventureRewardModel AdventureRewardModel => _adventureRewardModel;

        #endregion


        #region Methods

        public void StartAdventure_Rewards ()
        {
            _adventureRewardModel = new AdventureRewardModel ();
        }


        public void AddExp (int value)
        {
            _exp += value;
        }


        /// <summary>
        /// 보물 상자 보상 체크.
        /// </summary>
        public void RewardCheck ()
        {
            var random = Random.Range (0, 1);
            switch (random)
            {
                // 장비 보상을 얻음.
                case 0:
                    GetEquipment ();
                    break;
                
                // 골드 보상을 얻음.
                case 1:
                    GetGold ();
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// 골드 추가.
        /// </summary>
        public void GetGold ()
        {
            var gold = Random.Range (100, 200);
            _adventureRewardModel.AddGoldCount (gold);
        }
        
        
        /// <summary>
        /// 모험 필드에서 장비 아이템을 획득함.
        /// </summary>
        public void GetEquipment ()
        {
            var curUId = AdventureRewardModel.UniqueIndex;
            var index = EquipmentIndex ();
            var equipmentModel = NewEquipment (index);
            AdventureRewardModel.InAdventureEquipmentModels.Add (curUId, equipmentModel);
            

            int EquipmentIndex ()
            {
                var equipmentGroupIndex = _adventureFieldData.AppearedEquipmentGroupIndex;
                
                var advEqGroup = TableDataManager.Instance.AdventureEquipmentDict[equipmentGroupIndex];
                var advEqProbGroup = TableDataManager.Instance.AdventureEquipmentProbDict[advEqGroup.EquipmentProbIndex];

                var equipmentProbPoint = Random.Range (0, 10000);
                var stackedProbPoint = 0;
                for (var i = 0; i < advEqGroup.EquipmentIndexes.Length; i++)
                {
                    var curIndex = advEqGroup.EquipmentIndexes[i];
                    stackedProbPoint += advEqProbGroup.EquipmentProbabilities[i];

                    if (equipmentProbPoint < stackedProbPoint)
                    {
                        return curIndex;
                    }
                }

                return Constant.InvalidIndex;
            }
        }


        /// <summary>
        /// 장비 획득.
        /// </summary>
        public EquipmentModel NewEquipment (int equipmentIndex)
        {
            var equipmentModel = new EquipmentModel ();
            var equipmentData = TableDataManager.Instance.EquipmentDict[equipmentIndex];
            equipmentModel.SetUniqueData (NewUniqueId ());
            equipmentModel.SetEquipmentData (equipmentData);
            var equipmentGrade = _equipmentViewmodel.SetEquipmentGrade ();
            equipmentModel.SetEquipmentGrade (equipmentGrade);
            _equipmentViewmodel.SetEquipmentStatus (equipmentModel);

            return equipmentModel;

        }


        public int NewUniqueId ()
        {
            return AdventureRewardModel.UniqueIndex++;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}