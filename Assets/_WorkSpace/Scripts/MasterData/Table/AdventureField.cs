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
    /// 대상 파일  13_AdventureField.xlsx
    /// </summary>
    public class AdventureField : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 출현 장비 그룹 인덱스
        /// 출현 장비 그룹 인덱스
        /// </summary>
        public int AppearedEquipmentGroupIndex { get; internal set; }
        
        /// <summary>
        /// 필드 포인트 수량
        /// 필드 포인트 수량
        /// </summary>
        public int[] FieldPointCount { get; internal set; }
        
        /// <summary>
        /// 필드 포인트에 배치되는 필드 수량
        /// 필드 포인트에 배치되는 필드 수량
        /// </summary>
        public int[] FieldCount { get; internal set; }
        
        /// <summary>
        /// 숲 수량
        /// 숲 수량
        /// </summary>
        public int[] ForestCount { get; internal set; }
        
        /// <summary>
        /// 숲에 배치되는 나무타일의 수량
        /// 숲에 배치되는 나무타일의 수량
        /// </summary>
        public int[] TreeCount { get; internal set; }
        
        /// <summary>
        /// 보물상자의 수량
        /// 보물상자의 수량
        /// </summary>
        public int[] RewardCount { get; internal set; }
        
        /// <summary>
        /// 전투 수량
        /// 전투 수량
        /// </summary>
        public int[] BattleCount { get; internal set; }
        
        /// <summary>
        /// 보스 전투 수량
        /// 보스 전투 수량
        /// </summary>
        public int[] BossBattleCount { get; internal set; }
        
        /// <summary>
        /// 이벤트 수량
        /// 이벤트 수량
        /// </summary>
        public int[] EventCount { get; internal set; }
        
        /// <summary>
        /// 기타 수량
        /// 기타 수량
        /// </summary>
        public int[] OtherCount { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly AdventureFieldManager Manager = new AdventureFieldManager ();
    }


    /// <summary>
    /// AdventureField 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class AdventureFieldManager : BaseManager<AdventureField>
    {
        public AdventureField GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(AdventureFieldManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/AdventureField";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 11)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 11");
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

            CompletedTableLoad (nameof(AdventureField));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/AdventureField";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 11)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 11");
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

            CompletedTableLoad (nameof(AdventureField));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new AdventureField
            {
                Index = Parsing (dataList[0], int.Parse),
                AppearedEquipmentGroupIndex = Parsing (dataList[1], int.Parse),
                FieldPointCount = ParsingList (dataList[2], int.Parse),
                FieldCount = ParsingList (dataList[3], int.Parse),
                ForestCount = ParsingList (dataList[4], int.Parse),
                TreeCount = ParsingList (dataList[5], int.Parse),
                RewardCount = ParsingList (dataList[6], int.Parse),
                BattleCount = ParsingList (dataList[7], int.Parse),
                BossBattleCount = ParsingList (dataList[8], int.Parse),
                EventCount = ParsingList (dataList[9], int.Parse),
                OtherCount = ParsingList (dataList[10], int.Parse),
            };
            p.PostProcess (dataList);
            AdventureField.Manager.Add (p.Index, p);
        }
    }
}