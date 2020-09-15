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
    /// 대상 파일  09_Particle.xlsx
    /// </summary>
    public class Particle : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 프리팹 이름
        /// 프리팹 이름
        /// </summary>
        public string PrefabName { get; internal set; }
        
        /// <summary>
        /// 파티클 영역
        /// 파티클 영역
        /// </summary>
        public int BoundArea { get; internal set; }
        
        /// <summary>
        /// 파티클 크기
        /// 파티클 크기
        /// </summary>
        public float ParticleSize { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly ParticleManager Manager = new ParticleManager ();
    }


    /// <summary>
    /// Particle 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class ParticleManager : BaseManager<Particle>
    {
        public Particle GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(ParticleManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/Particle";
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

            CompletedTableLoad (nameof(Particle));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/Particle";
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

            CompletedTableLoad (nameof(Particle));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new Particle
            {
                Index = Parsing (dataList[0], int.Parse),
                PrefabName = dataList[1],
                BoundArea = Parsing (dataList[2], int.Parse),
                ParticleSize = Parsing (dataList[3], float.Parse),
            };
            p.PostProcess (dataList);
            Particle.Manager.Add (p.Index, p);
        }
    }
}