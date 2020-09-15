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
    /// 대상 파일  03_Skill.xlsx
    /// </summary>
    public class Skill : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 동시에 발동되는 스킬 인덱스
        /// 동시에 발동되는 스킬 인덱스
        /// </summary>
        public int InvokeSkillIndex { get; internal set; }
        
        /// <summary>
        /// 발동 이후에 발동되는 스킬 인덱스
        /// 발동 이후에 발동되는 스킬 인덱스
        /// </summary>
        public int AfterSkillIndex { get; internal set; }
        
        /// <summary>
        /// 스킬 이름 로컬 키값
        /// 스킬 이름 로컬 키값
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// 스킬 설명 로컬 키값
        /// 스킬 설명 로컬 키값
        /// </summary>
        public string Desc { get; internal set; }
        
        /// <summary>
        /// 스킬 발동 타이밍
        /// 스킬 발동 타이밍
        /// </summary>
        public SkillActiveCondition SkillActiveCondition { get; internal set; }
        
        /// <summary>
        /// 변동 능력치 타입
        /// 변동 능력치 타입
        /// </summary>
        public StatusType SkillStatusType { get; internal set; }
        
        /// <summary>
        /// 변동 타입
        /// 변동 타입
        /// </summary>
        public StatusChangeType StatusChangeType { get; internal set; }
        
        /// <summary>
        /// 횟수
        /// 횟수
        /// </summary>
        public int InvokeCount { get; internal set; }
        
        /// <summary>
        /// 시간
        /// 시간
        /// </summary>
        public float InvokeTime { get; internal set; }
        
        /// <summary>
        /// 스킬 범위 타입
        /// 스킬 범위 타입
        /// </summary>
        public SkillBound SkillBound { get; internal set; }
        
        /// <summary>
        /// 스킬 범위
        /// 스킬 범위
        /// </summary>
        public int SkillTargetBound { get; internal set; }
        
        /// <summary>
        /// 스킬 대상
        /// 스킬 대상
        /// </summary>
        public SkillTarget SkillTarget { get; internal set; }
        
        /// <summary>
        /// 스킬 값
        /// 스킬 값
        /// </summary>
        public float SkillValue { get; internal set; }
        
        /// <summary>
        /// 스킬 값 타입
        /// 스킬 값 타입
        /// </summary>
        public SkillValueType SkillValueType { get; internal set; }
        
        /// <summary>
        /// 능력치 참조 대상
        /// 능력치 참조 대상
        /// </summary>
        public RefSkillValueTarget RefSkillValueTarget { get; internal set; }
        
        /// <summary>
        /// 참조 능력치 타입
        /// 참조 능력치 타입
        /// </summary>
        public StatusType RefSkillStatusType { get; internal set; }
        
        /// <summary>
        /// 참조 능력치 계수
        /// 참조 능력치 계수
        /// </summary>
        public float RefSkillValueAmount { get; internal set; }
        
        /// <summary>
        /// 참조 체력 타입
        /// 참조 체력 타입
        /// </summary>
        public RefHealthType RefHealthType { get; internal set; }
        
        /// <summary>
        /// 부여할 상태
        /// 부여할 상태
        /// </summary>
        public BattleStateType BattleStateType { get; internal set; }
        
        /// <summary>
        /// 상태 부여 시간(초)
        /// 상태 부여 시간(초)
        /// </summary>
        public float BattleStateSeconds { get; internal set; }
        
        /// <summary>
        /// 발동시 출력할 파티클 인덱스
        /// 발동시 출력할 파티클 인덱스
        /// </summary>
        public int InvokeParticleIndex { get; internal set; }
        
        /// <summary>
        /// 적용시 출력할 파티클 인덱스
        /// 적용시 출력할 파티클 인덱스
        /// </summary>
        public int ApplyParticleIndex { get; internal set; }
        
        /// <summary>
        /// 적용후 출력할 파티클 인덱스
        /// 적용후 출력할 파티클 인덱스
        /// </summary>
        public int AfterParticleIndex { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly SkillManager Manager = new SkillManager ();
    }


    /// <summary>
    /// Skill 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class SkillManager : BaseManager<Skill>
    {
        public Skill GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(SkillManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/Skill";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 24)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 24");
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

            CompletedTableLoad (nameof(Skill));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/Skill";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 24)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 24");
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

            CompletedTableLoad (nameof(Skill));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new Skill
            {
                Index = Parsing (dataList[0], int.Parse),
                InvokeSkillIndex = Parsing (dataList[1], int.Parse),
                AfterSkillIndex = Parsing (dataList[2], int.Parse),
                Name = dataList[3],
                Desc = dataList[4],
                SkillActiveCondition = ParsingEnum<SkillActiveCondition>(dataList[5]),
                SkillStatusType = ParsingEnum<StatusType>(dataList[6]),
                StatusChangeType = ParsingEnum<StatusChangeType>(dataList[7]),
                InvokeCount = Parsing (dataList[8], int.Parse),
                InvokeTime = Parsing (dataList[9], float.Parse),
                SkillBound = ParsingEnum<SkillBound>(dataList[10]),
                SkillTargetBound = Parsing (dataList[11], int.Parse),
                SkillTarget = ParsingEnum<SkillTarget>(dataList[12]),
                SkillValue = Parsing (dataList[13], float.Parse),
                SkillValueType = ParsingEnum<SkillValueType>(dataList[14]),
                RefSkillValueTarget = ParsingEnum<RefSkillValueTarget>(dataList[15]),
                RefSkillStatusType = ParsingEnum<StatusType>(dataList[16]),
                RefSkillValueAmount = Parsing (dataList[17], float.Parse),
                RefHealthType = ParsingEnum<RefHealthType>(dataList[18]),
                BattleStateType = ParsingEnum<BattleStateType>(dataList[19]),
                BattleStateSeconds = Parsing (dataList[20], float.Parse),
                InvokeParticleIndex = Parsing (dataList[21], int.Parse),
                ApplyParticleIndex = Parsing (dataList[22], int.Parse),
                AfterParticleIndex = Parsing (dataList[23], int.Parse),
            };
            p.PostProcess (dataList);
            Skill.Manager.Add (p.Index, p);
        }
    }
}