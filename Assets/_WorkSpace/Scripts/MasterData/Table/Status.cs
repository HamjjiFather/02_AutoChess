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
    /// 대상 파일  06_Status.xlsx
    /// </summary>
    public class Status : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 능력치 이름 키값
        /// 능력치 이름 키값
        /// </summary>
        public string NameKey { get; internal set; }
        
        /// <summary>
        /// 능력치 타입
        /// 능력치 타입
        /// </summary>
        public StatusType StatusType { get; internal set; }
        
        /// <summary>
        /// 아이콘 이미지 이름
        /// 아이콘 이미지 이름
        /// </summary>
        public string IconImage { get; internal set; }
        
        /// <summary>
        /// 자리수 표시 방식
        /// 자리수 표시 방식
        /// </summary>
        public string Format { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly StatusManager Manager = new StatusManager ();
    }


    /// <summary>
    /// Status 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class StatusManager : BaseManager<Status>
    {
        public Status GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(StatusManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/Status";
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

            CompletedTableLoad (nameof(Status));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/Status";
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

            CompletedTableLoad (nameof(Status));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new Status
            {
                Index = Parsing (dataList[0], int.Parse),
                NameKey = dataList[1],
                StatusType = ParsingEnum<StatusType>(dataList[2]),
                IconImage = dataList[3],
                Format = dataList[4],
            };
            p.PostProcess (dataList);
            Status.Manager.Add (p.Index, p);
        }
    }
}