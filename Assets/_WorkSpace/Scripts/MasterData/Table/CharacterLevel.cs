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
    /// 대상 파일  02_CharacterLevel.xlsx
    /// </summary>
    public class CharacterLevel : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 실제 레벨
        /// 실제 레벨
        /// </summary>
        public int Level { get; internal set; }
        
        /// <summary>
        /// 레벨
        /// 레벨
        /// </summary>
        public string LevelString { get; internal set; }
        
        /// <summary>
        /// 누적 EXP
        /// 누적 EXP
        /// </summary>
        public float AccReqExp { get; internal set; }
        
        /// <summary>
        /// 요구 EXP
        /// 요구 EXP
        /// </summary>
        public float ReqExp { get; internal set; }
        
        /// <summary>
        /// 보정 EXP
        /// 보정 EXP
        /// </summary>
        public float CoExp { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly CharacterLevelManager Manager = new CharacterLevelManager ();
    }


    /// <summary>
    /// CharacterLevel 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class CharacterLevelManager : BaseManager<CharacterLevel>
    {
        public CharacterLevel GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(CharacterLevelManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/CharacterLevel";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 6)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 6");
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

            CompletedTableLoad (nameof(CharacterLevel));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/CharacterLevel";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 6)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 6");
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

            CompletedTableLoad (nameof(CharacterLevel));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new CharacterLevel
            {
                Index = Parsing (dataList[0], int.Parse),
                Level = Parsing (dataList[1], int.Parse),
                LevelString = dataList[2],
                AccReqExp = Parsing (dataList[3], float.Parse),
                ReqExp = Parsing (dataList[4], float.Parse),
                CoExp = Parsing (dataList[5], float.Parse),
            };
            p.PostProcess (dataList);
            CharacterLevel.Manager.Add (p.Index, p);
        }
    }
}