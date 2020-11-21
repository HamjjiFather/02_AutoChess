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
    /// 대상 파일  04_Equipment.xlsx
    /// </summary>
    public class AdvEquipmentProbGroup : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 출현 장비 확률
        /// 장비 확률
        /// </summary>
        public int[] EquipmentProbabilities { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly AdvEquipmentProbGroupManager Manager = new AdvEquipmentProbGroupManager ();
    }


    /// <summary>
    /// AdvEquipmentProbGroup 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class AdvEquipmentProbGroupManager : BaseManager<AdvEquipmentProbGroup>
    {
        public AdvEquipmentProbGroup GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(AdvEquipmentProbGroupManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/AdvEquipmentProbGroup";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 2)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 2");
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

            CompletedTableLoad (nameof(AdvEquipmentProbGroup));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/AdvEquipmentProbGroup";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 2)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 2");
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

            CompletedTableLoad (nameof(AdvEquipmentProbGroup));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new AdvEquipmentProbGroup
            {
                Index = Parsing (dataList[0], int.Parse),
                EquipmentProbabilities = ParsingList (dataList[1], int.Parse),
            };
            p.PostProcess (dataList);
            AdvEquipmentProbGroup.Manager.Add (p.Index, p);
        }
    }
}