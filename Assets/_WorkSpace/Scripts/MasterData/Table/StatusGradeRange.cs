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
    /// 대상 파일  07_StatusGradeRange.xlsx
    /// </summary>
    public class StatusGradeRange : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 등급 타입
        /// 등급 타입
        /// </summary>
        public StatusGrade StatusGrade { get; internal set; }
        
        /// <summary>
        /// 최소값(이상)
        /// 최소값(이상)
        /// </summary>
        public float Min { get; internal set; }
        
        /// <summary>
        /// 최대값(미만)
        /// 최대값(미만)
        /// </summary>
        public float Max { get; internal set; }
        
        /// <summary>
        /// 등급
        /// 등급
        /// </summary>
        public string GradeString { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly StatusGradeRangeManager Manager = new StatusGradeRangeManager ();
    }


    /// <summary>
    /// StatusGradeRange 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class StatusGradeRangeManager : BaseManager<StatusGradeRange>
    {
        public StatusGradeRange GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(StatusGradeRangeManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/StatusGradeRange";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 5)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 5");
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

            CompletedTableLoad (nameof(StatusGradeRange));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/StatusGradeRange";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 5)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 5");
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

            CompletedTableLoad (nameof(StatusGradeRange));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new StatusGradeRange
            {
                Index = Parsing (dataList[0], int.Parse),
                StatusGrade = ParsingEnum<StatusGrade>(dataList[1]),
                Min = Parsing (dataList[2], float.Parse),
                Max = Parsing (dataList[3], float.Parse),
                GradeString = dataList[4],
            };
            p.PostProcess (dataList);
            StatusGradeRange.Manager.Add (p.Index, p);
        }
    }
}