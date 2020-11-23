using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using MasterData;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AutoChess
{
    public class EquipmentViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private GameSetting _gameSetting;

#pragma warning restore CS0649

        private int _lastUniqueId;

        /// <summary>
        /// 장비.
        /// </summary>
        private readonly Dictionary<int, EquipmentModel> _equipmentModels = new Dictionary<int, EquipmentModel> ();

        public Dictionary<int, EquipmentModel> EquipmentModels => _equipmentModels;


        private static EquipmentModel _emptyEquipmentModel = new EquipmentModel ();
        public static EquipmentModel EmptyEquipmentModel => _emptyEquipmentModel;

        private readonly int[] _startEquipmentIndexes =
        {
            0,
            1,
            2,
            3,
            4
        };

        public bool IsDataChanged { get; set; }

        #endregion


        public override void Initialize ()
        {
        }


        public override void InitAfterLoadTableData ()
        {
            base.InitAfterLoadTableData ();
        }


        public override void InitAfterLoadLocalData ()
        {
            _lastUniqueId = LocalDataHelper.GetGameBundle ().LastEquipmentUniqueId;

            var equipmentBundle = LocalDataHelper.GetEquipmentBundle ();

            // 처음 실행.
            if (!equipmentBundle.EquipmentUniqueIds.Any ())
            {
                _startEquipmentIndexes.ForEach (index =>
                {
                    var newEquipment = NewEquipment (index);
                    _equipmentModels.Add (newEquipment.UniqueEquipmentId, newEquipment);
                });

                SaveEquipmentData ();
                IsDataChanged = true;
                return;
            }

            equipmentBundle.EquipmentUniqueIds.ForEach ((uid, index) =>
            {
                var equipmentModel = new EquipmentModel ();
                var starGrade = equipmentBundle.EquipmentGrades[index];
                var equipmentBundleData = equipmentBundle.EquipmentDatas[index];
                var equipmentData = Equipment.Manager.GetItemByIndex (equipmentBundle.EquipmentIds[index]);
                var statusModel = SetBaseStatusList (equipmentBundleData.EquipmentStatusIndexes,
                    equipmentBundleData.EquipmentStatusGrades);

                equipmentModel.SetStatusGrade (equipmentBundleData.EquipmentStatusIndexes,
                    equipmentBundleData.EquipmentStatusGrades);
                equipmentModel.SetUniqueData (uid);
                equipmentModel.SetEquipmentData (equipmentData);
                equipmentModel.SetEquipmentGrade (starGrade);
                equipmentModel.SetStatus (statusModel);

                _equipmentModels.Add (equipmentModel.UniqueEquipmentId, equipmentModel);
            });

            SaveEquipmentData ();
            IsDataChanged = true;
        }


        #region Methods

        public EquipmentModel GetEquipmentModel (int uniqueIndex)
        {
            return _equipmentModels.ContainsKey (uniqueIndex) ? _equipmentModels[uniqueIndex] : _emptyEquipmentModel;
        }


        /// <summary>
        /// 장비 획득.
        /// </summary>
        public EquipmentModel NewEquipment (int equipmentIndex)
        {
            var equipmentModel = new EquipmentModel ();
            var equipmentData = Equipment.Manager.GetItemByIndex (equipmentIndex);
            equipmentModel.SetUniqueData (NewUniqueId ());
            equipmentModel.SetEquipmentData (equipmentData);
            equipmentModel.SetEquipmentGrade (EquipmentGrade.Grade1);

            SetEquipmentStatus (equipmentModel);

            return equipmentModel;
        }


        /// <summary>
        /// 새로 획득한 장비의 등급을 설정함.
        /// </summary>
        public EquipmentGrade SetEquipmentGrade ()
        {
            var equipmentGradeProb = TableDataHelper.Instance.GetEquipmentGradeProb (0);
            
            var rand = Random.Range (0, 10000);
            var gradeArray = Enum.GetValues (typeof(EquipmentGrade));
            var gradeEnumerator = gradeArray.GetEnumerator ();
            var stackedProbValue = 0;

            while (gradeEnumerator.MoveNext ())
            {
                if (!(gradeEnumerator.Current is EquipmentGrade grade)) continue;
                stackedProbValue += equipmentGradeProb.ProbGrades[(int) grade];
                if (stackedProbValue < rand)
                {
                    return grade;
                }
            }

            return EquipmentGrade.Grade1;
        }
        


        public void SetEquipmentStatus (EquipmentModel equipmentModel)
        {
            equipmentModel.EquipmentData.BaseEquipmentStatusIndexes.Where (x => !x.Equals (Constants.INVALID_INDEX))
                .ForEach (index =>
                {
                    var baseRand = Random.Range (0, 1f);
                    equipmentModel.SetStatusGrade (index, baseRand);
                });

            var rand = Random.Range (0, 1f);
            var statusGradeIndex = equipmentModel.EquipmentData.AvailEquipmentStatusIndexes
                .Where (x => !x.Equals (Constants.INVALID_INDEX)).Choice ();
            equipmentModel.SetStatusGrade (statusGradeIndex, rand);

            var statusModel = SetBaseStatusList (equipmentModel.StatusIndexes,
                equipmentModel.StatusGrades);
            equipmentModel.SetStatus (statusModel);
        }


        public void SaveEquipmentData ()
        {
            LocalDataHelper.SaveEquipmentStatusData (
                _equipmentModels.Values.Select (x => x.UniqueEquipmentId).ToList (),
                _equipmentModels.Values.Select (x => x.EquipmentData.Index).ToList (),
                _equipmentModels.Values.Select (x => x.EquipmentGrade).ToList (),
                _equipmentModels.Values.Select (x => x.StatusIndexes).ToList (),
                _equipmentModels.Values.Select (x => x.StatusGrades).ToList ());
        }


        public IEnumerable<BaseStatusModel> SetBaseStatusList (IReadOnlyList<int> indexes,
            IReadOnlyList<float> gradeValues)
        {
            var list = new List<BaseStatusModel> ();
            for (var i = 0; i < indexes.Count; i++)
            {
                var equipmentStatus = EquipmentStatusGroup.Manager.GetItemByIndex (indexes[i]);
                var statusData = TableDataHelper.Instance.GetStatus (equipmentStatus.StatusType);
                var statusValue = Mathf.Lerp (equipmentStatus.Min, equipmentStatus.Max, gradeValues[i]);
                var statusModel = new BaseStatusModel (statusData);
                statusModel.SetStatusValue (statusValue);
                statusModel.SetGradeValue (gradeValues[i]);

                list.Add (statusModel);
            }

            return list;
        }


        public int CombineUniqueId (int uniqueIndex)
        {
            return _lastUniqueId + uniqueIndex;
        }

        public int NewUniqueId ()
        {
            _lastUniqueId++;
            LocalDataHelper.SaveEquipmentUniqueIdData (_lastUniqueId);
            return _gameSetting.baseEquipmentUniqueId + _lastUniqueId;
        }

        #endregion


        #region Get Equipment Info

        #endregion


        #region EventMethods

        #endregion
    }
}