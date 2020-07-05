// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Character : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 캐릭터 이름 로컬 키값
    /// </summary>
    public string Name;

    /// <summary>
    /// 캐릭터 설명 로컬 키값
    /// </summary>
    public string Desc;

    /// <summary>
    /// 사정거리
    /// </summary>
    public int AttackDistance;

    /// <summary>
    /// 몬스터 위치
    /// </summary>
    public CharacterGroundType GroundType;

    /// <summary>
    /// 체력
    /// </summary>
    public float[] Hp;

    /// <summary>
    /// 체력 증가값
    /// </summary>
    public float HpInc;

    /// <summary>
    /// 공격력
    /// </summary>
    public float[] At;

    /// <summary>
    /// 공격력 증가값
    /// </summary>
    public float AtInc;

    /// <summary>
    /// 주문력
    /// </summary>
    public float[] Ap;

    /// <summary>
    /// 공격력 증가값
    /// </summary>
    public float ApInc;

    /// <summary>
    /// 방어력
    /// </summary>
    public float[] Df;

    /// <summary>
    /// 방어력 증가값
    /// </summary>
    public float DfInc;

    /// <summary>
    /// 공격 속도
    /// </summary>
    public float AtSpd;

    /// <summary>
    /// 기본 공격 인덱스
    /// </summary>
    public int AttackIndex;

    /// <summary>
    /// 스킬 인덱스
    /// </summary>
    public int SkillIndex;

    /// <summary>
    /// 스프라이트 이름
    /// </summary>
    public string SpriteResName;

    /// <summary>
    /// 애니메이터 이름
    /// </summary>
    public string AnimatorResName;



    public Character ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		Name = myData[rowIndex++];
		Desc = myData[rowIndex++];
		AttackDistance = int.Parse(myData[rowIndex++]);
		GroundType = (CharacterGroundType)Enum.Parse (typeof(CharacterGroundType), myData[rowIndex++]);
		Hp = Array.ConvertAll (myData[rowIndex++].Split ('/'), float.Parse);
		HpInc = float.Parse(myData[rowIndex++]);
		At = Array.ConvertAll (myData[rowIndex++].Split ('/'), float.Parse);
		AtInc = float.Parse(myData[rowIndex++]);
		Ap = Array.ConvertAll (myData[rowIndex++].Split ('/'), float.Parse);
		ApInc = float.Parse(myData[rowIndex++]);
		Df = Array.ConvertAll (myData[rowIndex++].Split ('/'), float.Parse);
		DfInc = float.Parse(myData[rowIndex++]);
		AtSpd = float.Parse(myData[rowIndex++]);
		AttackIndex = int.Parse(myData[rowIndex++]);
		SkillIndex = int.Parse(myData[rowIndex++]);
		SpriteResName = myData[rowIndex++];
		AnimatorResName = myData[rowIndex++];

    }
}
