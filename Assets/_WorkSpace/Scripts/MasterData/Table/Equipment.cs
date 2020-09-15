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
    public class Equipment : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 장비 이름 로컬 키값
        /// 장비 이름 로컬 키값
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// 장비 타입
        /// 장비 타입
        /// </summary>
        public EquipmentType EquipmentType { get; internal set; }
        
        /// <summary>
        /// 스프라이트 이름
        /// 스프라이트 이름
        /// </summary>
        public string SpriteResName { get; internal set; }
        
        /// <summary>
        /// 부여 가능 능력치
        /// 부여 가능 능력치
        /// </summary>
        public int[] AvailEquipmentTypeIndex { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly EquipmentManager Manager = new EquipmentManager ();
    }


    /// <summary>
    /// Equipment 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class EquipmentManager : BaseManager<Equipment>
    {
        public Equipment GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(EquipmentManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/Equipment";
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

            CompletedTableLoad (nameof(Equipment));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/Equipment";
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

            CompletedTableLoad (nameof(Equipment));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new Equipment
            {
                Index = Parsing (dataList[0], int.Parse),
                Name = dataList[1],
                EquipmentType = ParsingEnum<EquipmentType>(dataList[2]),
                SpriteResName = dataList[3],
                AvailEquipmentTypeIndex = ParsingList (dataList[4], int.Parse),
            };
            p.PostProcess (dataList);
            Equipment.Manager.Add (p.Index, p);
        }
    }
}