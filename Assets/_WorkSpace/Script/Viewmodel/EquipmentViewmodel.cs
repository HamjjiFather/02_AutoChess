using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using UniRx.Async;
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

        private readonly Dictionary<int, EquipmentModel> _equipmentModels = new Dictionary<int, EquipmentModel> ();
        public Dictionary<int, EquipmentModel> EquipmentModels => _equipmentModels;

        private static EquipmentModel _emptyEquipmentModel = new EquipmentModel ();
        public static EquipmentModel EmptyEquipmentModel => _emptyEquipmentModel;

        private readonly int[] _startEquipmentIndexes =
        {
            5000,
            5001,
            5002,
            5003,
            5004
        };

        public bool IsDataChanged { get; set; }

        #endregion


        public override void Initialize ()
        {
        }


        public override void InitTableData ()
        {
            base.InitTableData ();
        }


        public override void InitLocalData ()
        {
            _lastUniqueId = LocalDataHelper.GetGameBundle ().LastEquipmentUniqueId;

            var equipmentBundle = LocalDataHelper.GetEquipmentBundle ();

            if (!equipmentBundle.EquipmentUniqueIds.Any ())
            {
                _startEquipmentIndexes.Foreach (index =>
                {
                    var newEquipment = NewEquipment (index);
                    _equipmentModels.Add (newEquipment.UniqueEquipmentId, newEquipment);
                });

                SaveEquipmentData ();
                IsDataChanged = true;
                return;
            }

            equipmentBundle.EquipmentUniqueIds.Foreach ((uid, index) =>
            {
                var equipmentModel = new EquipmentModel ();
                var starGrade = equipmentBundle.EquipmentGrades[index];
                var equipmentBundleData = equipmentBundle.EquipmentDatas[index];
                var equipmentData = TableDataManager.Instance.EquipmentDict[equipmentBundle.EquipmentIds[index]];
                var statusModel = SetBaseStatusDict (equipmentBundleData.EquipmentStatusIndexes,
                    equipmentBundleData.EquipmentStatusGrades);

                equipmentModel.SetStatusGrade (equipmentBundleData.EquipmentStatusIndexes,
                    equipmentBundleData.EquipmentStatusGrades);
                equipmentModel.SetUniqueData (CombineUniqueId (uid));
                equipmentModel.SetEquipmentData (equipmentData);
                equipmentModel.SetStarGrade (starGrade);
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
        /// 캐릭터 획득.
        /// </summary>
        public EquipmentModel NewEquipment (int equipmentIndex)
        {
            var equipmentModel = new EquipmentModel ();
            var equipmentData = TableDataManager.Instance.EquipmentDict[equipmentIndex];
            equipmentModel.SetUniqueData (NewUniqueId ());
            equipmentModel.SetEquipmentData (equipmentData);
            equipmentModel.SetStarGrade (StarGrade.Grade1);

            SetStatusGradeValue ();

            return equipmentModel;

            void SetStatusGradeValue ()
            {
                var rand = Random.Range (0, 1f);
                var statusGradeIndex = equipmentData.AvailEquipmentTypeIndex.RandomSource ();
                equipmentModel.SetStatusGrade (statusGradeIndex, rand);

                var statusModel = SetBaseStatusDict (equipmentModel.StatusIndexes,
                    equipmentModel.StatusGrades);
                equipmentModel.SetStatus (statusModel);
            }
        }


        public void SaveEquipmentData ()
        {
            LocalDataHelper.SaveEquipmentStatusData (
                _equipmentModels.Values.Select (x => x.UniqueEquipmentId).ToList (),
                _equipmentModels.Values.Select (x => x.EquipmentData.Id).ToList (),
                _equipmentModels.Values.Select (x => x.StarGrade).ToList (),
                _equipmentModels.Values.Select (x => x.StatusIndexes).ToList (),
                _equipmentModels.Values.Select (x => x.StatusGrades).ToList ());
        }


        private Dictionary<StatusType, BaseStatusModel> SetBaseStatusDict (IReadOnlyList<int> indexes,
            IReadOnlyList<float> gradeValues)
        {
            var dict = new Dictionary<StatusType, BaseStatusModel> ();
            for (var i = 0; i < indexes.Count; i++)
            {
                var equipmentStatus = TableDataManager.Instance.EquipmentStatusDict[indexes[i]];
                var statusData =
                    TableDataManager.Instance.StatusDict[(int) DataType.Status + (int) equipmentStatus.StatusType];
                var statusValue = Mathf.Lerp (equipmentStatus.Min, equipmentStatus.Max, gradeValues[i]);
                var statusModel = new BaseStatusModel (statusData);
                statusModel.SetStatusValue (statusValue);
                statusModel.SetGradeValue (gradeValues[i]);

                dict.Add (equipmentStatus.StatusType, statusModel);
            }

            return dict;
        }


        public int CombineUniqueId (int uniqueIndex)
        {
            return _gameSetting.baseEquipmentUniqueId + uniqueIndex;
        }

        public int NewUniqueId ()
        {
            _lastUniqueId++;
            LocalDataHelper.SaveEquipmentUniqueIdData (_lastUniqueId);
            return _gameSetting.baseEquipmentUniqueId + _lastUniqueId;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}