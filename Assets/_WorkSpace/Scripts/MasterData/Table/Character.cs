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
    /// 대상 파일  01_Character.xlsx
    /// </summary>
    public class Character : BaseTable
    {
        /// <summary>
        /// 인덱스
        /// 인덱스
        /// </summary>
        public int Index { get; internal set; }
        
        /// <summary>
        /// 캐릭터 이름 로컬 키값
        /// 캐릭터 이름 로컬 키값
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// 캐릭터 설명 로컬 키값
        /// 캐릭터 설명 로컬 키값
        /// </summary>
        public string Desc { get; internal set; }
        
        /// <summary>
        /// 공격 유형
        /// 공격 유형
        /// </summary>
        public BattleCharacterType BattleCharacterType { get; internal set; }
        
        /// <summary>
        /// 체력 증가값
        /// 체력 증가값
        /// </summary>
        public float HpInc { get; internal set; }
        
        /// <summary>
        /// 공격력 증가값
        /// 공격력 증가값
        /// </summary>
        public float AtInc { get; internal set; }
        
        /// <summary>
        /// 주문력 증가값
        /// 주문력 증가값
        /// </summary>
        public float ApInc { get; internal set; }
        
        /// <summary>
        /// 방어력 증가값
        /// 방어력 증가값
        /// </summary>
        public float DfInc { get; internal set; }
        
        /// <summary>
        /// 공격 속도
        /// 공격 속도
        /// </summary>
        public float AttackSpeed { get; internal set; }
        
        /// <summary>
        /// 기본 공격 인덱스
        /// 기본 공격 인덱스
        /// </summary>
        public int AttackIndex { get; internal set; }
        
        /// <summary>
        /// 스킬 인덱스
        /// 스킬 인덱스
        /// </summary>
        public int SkillIndex { get; internal set; }
        
        /// <summary>
        /// 스프라이트 이름
        /// 스프라이트 이름
        /// </summary>
        public string SpriteResName { get; internal set; }
        
        /// <summary>
        /// 애니메이터 이름
        /// 애니메이터 이름
        /// </summary>
        public string AnimatorResName { get; internal set; }
        
        /// <summary>
        /// 체력
        /// 체력 최소값
        /// </summary>
        public float[] Hp { get; internal set; }
        
        /// <summary>
        /// 공격력
        /// 공격력
        /// </summary>
        public float[] At { get; internal set; }
        
        /// <summary>
        /// 주문력
        /// 주문력
        /// </summary>
        public float[] Ap { get; internal set; }
        
        /// <summary>
        /// 방어력
        /// 방어력
        /// </summary>
        public float[] Df { get; internal set; }

        /// <summary>
        /// 매니저
        /// </summary>
        public static readonly CharacterManager Manager = new CharacterManager ();
    }


    /// <summary>
    /// Character 모델의 매니저
    /// partial 로 선언되어 있기 때문에 확장 시킬수 있습니다.
    /// </summary>
    public partial class CharacterManager : BaseManager<Character>
    {
        public Character GetItemByIndex (int index)
        {
            return ContainsKey(index) ? this[index] : throw new KeyNotFoundException($"{nameof(CharacterManager)}에 {index}가 존재하지 않는다.");
        }

        
        public async UniTask LoadAsync (string basePath)
        {
            var filePath = $"{basePath}/Character";
            var rowData = await LoadDataAsync (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 17)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 17");
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

            CompletedTableLoad (nameof(Character));
        }
        

        public void Load (string basePath)
        {
            var filePath = $"{basePath}/Character";
            var rowData = LoadData (filePath);

            // 첫번째 row 는 컬럼의 이름이기 떄문에 제외.
            for (var i = 1; i < rowData.Length; i++)
            {
                var row = rowData[i];
                var dataList = row.Split ('\t');
                if (dataList.Length != 17)
                {
                    Debug.LogWarning ($"[Path: {filePath}, Row: {i}] 필드 수량 불일치 {dataList.Length} != 17");
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

            CompletedTableLoad (nameof(Character));
        }


        private void LoadRow (string[] dataList)
        {
            var p = new Character
            {
                Index = Parsing (dataList[0], int.Parse),
                Name = dataList[1],
                Desc = dataList[2],
                BattleCharacterType = ParsingEnum<BattleCharacterType>(dataList[3]),
                HpInc = Parsing (dataList[4], float.Parse),
                AtInc = Parsing (dataList[5], float.Parse),
                ApInc = Parsing (dataList[6], float.Parse),
                DfInc = Parsing (dataList[7], float.Parse),
                AttackSpeed = Parsing (dataList[8], float.Parse),
                AttackIndex = Parsing (dataList[9], int.Parse),
                SkillIndex = Parsing (dataList[10], int.Parse),
                SpriteResName = dataList[11],
                AnimatorResName = dataList[12],
                Hp = ParsingList (dataList[13], float.Parse),
                At = ParsingList (dataList[14], float.Parse),
                Ap = ParsingList (dataList[15], float.Parse),
                Df = ParsingList (dataList[16], float.Parse),
            };
            p.PostProcess (dataList);
            Character.Manager.Add (p.Index, p);
        }
    }
}