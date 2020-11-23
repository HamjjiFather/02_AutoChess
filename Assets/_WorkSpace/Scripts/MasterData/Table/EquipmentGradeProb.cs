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
    public class EquipmentGradeProb : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 장비 출현 확률
        /// 등급1 출현 확률
        /// </summary>
        public int[] ProbGrades { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly EquipmentGradeProbManager Manager = new EquipmentGradeProbManager ();
    }


    /// <summary>
    /// EquipmentGradeProb 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class EquipmentGradeProbManager : BaseManager<EquipmentGradeProb>
    {
        public EquipmentGradeProb GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(EquipmentGradeProbManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/EquipmentGradeProb";
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

            CompletedTableLoad (nameof(EquipmentGradeProb));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/EquipmentGradeProb";
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

            CompletedTableLoad (nameof(EquipmentGradeProb));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new EquipmentGradeProb
            {
                Index = Parsing (dataList[0], int.Parse),
                ProbGrades = ParsingList (dataList[1], int.Parse),
            };
            p.PostProcess (dataList);
            EquipmentGradeProb.Manager.Add (p.Index, p);
        }
    }
}