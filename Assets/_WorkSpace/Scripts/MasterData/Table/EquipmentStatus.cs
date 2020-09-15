// 이 파일은 자동생성 되었습니다.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace MasterData
{
    /// <summary>
    /// 대상 파일  05_EquipmentStatus.xlsx
    /// </summary>
    public class EquipmentStatus : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 부여되는 능력치 타입
        /// 부여되는 능력치 타입
        /// </summary>
        public StatusType StatusType { get; internal set; }
        
        /// <summary>
        /// 최소값
        /// 최소값
        /// </summary>
        public float Min { get; internal set; }
        
        /// <summary>
        /// 최대값
        /// 최대값
        /// </summary>
        public float Max { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly EquipmentStatusManager Manager = new EquipmentStatusManager ();
    }


    /// <summary>
    /// EquipmentStatus 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class EquipmentStatusManager : BaseManager<EquipmentStatus>
    {
        public EquipmentStatus GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(EquipmentStatusManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/EquipmentStatus";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 4)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 4");
                    continue;
                }

                try
                {
                    LoadRow (dataList);
                }
                catch (FormatException)
                {
                    Debug.LogWarning ($"{i} = {row}");
                    throw;
                }
            }
            await UniTask.Yield ();

            CompletedTableLoad (nameof(EquipmentStatus));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/EquipmentStatus";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 4)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 4");
                    continue;
                }

                try
                {
                    LoadRow (dataList);
                }
                catch (FormatException)
                {
                    Debug.LogWarning ($"{i} = {row}");
                    throw;
                }
            }

            CompletedTableLoad (nameof(EquipmentStatus));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new EquipmentStatus
            {
                Index = Parsing (dataList[0], int.Parse),
                StatusType = ParsingEnum<StatusType>(dataList[1]),
                Min = Parsing (dataList[2], float.Parse),
                Max = Parsing (dataList[3], float.Parse),
            };
            p.PostProcess (dataList);
            EquipmentStatus.Manager.Add (p.Index, p);
        }
    }
}