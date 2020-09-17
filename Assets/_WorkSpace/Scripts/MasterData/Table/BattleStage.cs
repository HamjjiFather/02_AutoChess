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
    /// 대상 파일  08_BattleStage.xlsx
    /// </summary>
    public class BattleStage : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 보상 경험치
        /// 보상 경험치
        /// </summary>
        public int RewardExp { get; internal set; }
        
        /// <summary>
        /// 출현 몬스터
        /// 출현 몬스터
        /// </summary>
        public int[] MonsterIndexes { get; internal set; }
        
        /// <summary>
        /// 출현 몬스터 레벨
        /// 출현 몬스터 레벨
        /// </summary>
        public int[] MonsterLevels { get; internal set; }
        
        /// <summary>
        /// 출현 몬스터 위치
        /// 출현 몬스터 위치
        /// </summary>
        public string[] MonsterPosition { get; internal set; }

        /// <summary>
        /// 몬스터 크기.
        /// </summary>
        public int[] MonsterScale { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly BattleStageManager Manager = new BattleStageManager ();
    }


    /// <summary>
    /// BattleStage 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class BattleStageManager : BaseManager<BattleStage>
    {
        public BattleStage GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(BattleStageManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/BattleStage";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 6)
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

            CompletedTableLoad (nameof(BattleStage));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/BattleStage";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 6)
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

            CompletedTableLoad (nameof(BattleStage));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new BattleStage
            {
                Index = Parsing (dataList[0], int.Parse),
                RewardExp = Parsing (dataList[1], int.Parse),
                MonsterIndexes = ParsingList (dataList[2], int.Parse),
                MonsterLevels = ParsingList (dataList[3], int.Parse),
                MonsterPosition = ParsingStringList (dataList[4]),
                MonsterScale = ParsingList (dataList[5], int.Parse),
            };
            p.PostProcess (dataList);
            BattleStage.Manager.Add (p.Index, p);
        }
    }
}