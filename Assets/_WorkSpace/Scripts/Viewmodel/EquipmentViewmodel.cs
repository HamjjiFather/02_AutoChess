using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using KKSFramework;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;
using MasterData;
using UnityEditor;
using UnityEngine;
using Zenject;

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
            4000,
            4001,
            4002,
            4003,
            4004
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
                var statusModel = SetBaseStatusDict (equipmentBundleData.EquipmentStatusIndexes,
                    equipmentBundleData.EquipmentStatusGrades);

                equipmentModel.SetStatusGrade (equipmentBundleData.EquipmentStatusIndexes,
                    equipmentBundleData.EquipmentStatusGrades);
                equipmentModel.SetUniqueData (uid);
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
            var equipmentData = Equipment.Manager.GetItemByIndex (equipmentIndex);
            equipmentModel.SetUniqueData (NewUniqueId ());
            equipmentModel.SetEquipmentData (equipmentData);
            equipmentModel.SetStarGrade (StarGrade.Grade1);

            SetStatusGradeValue ();

            return equipmentModel;

            void SetStatusGradeValue ()
            {
                var rand = Random.Range (0, 1f);
                var statusGradeIndex = equipmentData.AvailEquipmentTypeIndex.Choice();
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
                _equipmentModels.Values.Select (x => x.EquipmentData.Index).ToList (),
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
                var equipmentStatus = EquipmentStatus.Manager.GetItemByIndex (indexes[i]);
                var statusData = TableDataHelper.Instance.GetStatus (equipmentStatus.StatusType);
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