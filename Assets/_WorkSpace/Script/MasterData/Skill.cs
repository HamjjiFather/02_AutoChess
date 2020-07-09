// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Skill : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 동시에 발동되는 스킬 인덱스
    /// </summary>
    public int InvokeSkillIndex;

    /// <summary>
    /// 발동 이후에 발동되는 스킬 인덱스
    /// </summary>
    public int AfterSkillIndex;

    /// <summary>
    /// 스킬 이름 로컬 키값
    /// </summary>
    public string Name;

    /// <summary>
    /// 스킬 설명 로컬 키값
    /// </summary>
    public string Desc;

    /// <summary>
    /// 스킬 발동 타이밍
    /// </summary>
    public SkillActiveCondition SkillActiveCondition;

    /// <summary>
    /// 변동 능력치 타입
    /// </summary>
    public StatusType SkillStatusType;

    /// <summary>
    /// 변동 타입
    /// </summary>
    public StatusChangeType StatusChangeType;

    /// <summary>
    /// 횟수
    /// </summary>
    public int InvokeCount;

    /// <summary>
    /// 시간
    /// </summary>
    public float InvokeTime;

    /// <summary>
    /// 스킬 범위 타입
    /// </summary>
    public SkillBound SkillBound;

    /// <summary>
    /// 스킬 범위
    /// </summary>
    public int SkillTargetBound;

    /// <summary>
    /// 스킬 대상
    /// </summary>
    public SkillTarget SkillTarget;

    /// <summary>
    /// 스킬 값
    /// </summary>
    public float SkillValue;

    /// <summary>
    /// 스킬 값 타입
    /// </summary>
    public SkillValueType SkillValueType;

    /// <summary>
    /// 능력치 참조 대상
    /// </summary>
    public RefSkillValueTarget RefSkillValueTarget;

    /// <summary>
    /// 참조 능력치 타입
    /// </summary>
    public StatusType RefSkillStatusType;

    /// <summary>
    /// 참조 능력치 계수
    /// </summary>
    public float RefSkillValueAmount;

    /// <summary>
    /// 참조 체력 타입
    /// </summary>
    public RefHealthType RefHealthType;

    /// <summary>
    /// 부여할 상태
    /// </summary>
    public BattleStateType BattleStateType;

    /// <summary>
    /// 상태 부여 시간(초)
    /// </summary>
    public float BattleStateSeconds;

    /// <summary>
    /// 발동시 출력할 파티클 인덱스
    /// </summary>
    public int InvokeParticleIndex;

    /// <summary>
    /// 적용시 출력할 파티클 인덱스
    /// </summary>
    public int ApplyParticleIndex;

    /// <summary>
    /// 적용후 출력할 파티클 인덱스
    /// </summary>
    public int AfterParticleIndex;



    public Skill ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		InvokeSkillIndex = int.Parse(myData[rowIndex++]);
		AfterSkillIndex = int.Parse(myData[rowIndex++]);
		Name = myData[rowIndex++];
		Desc = myData[rowIndex++];
		SkillActiveCondition = (SkillActiveCondition)Enum.Parse (typeof(SkillActiveCondition), myData[rowIndex++]);
		SkillStatusType = (StatusType)Enum.Parse (typeof(StatusType), myData[rowIndex++]);
		StatusChangeType = (StatusChangeType)Enum.Parse (typeof(StatusChangeType), myData[rowIndex++]);
		InvokeCount = int.Parse(myData[rowIndex++]);
		InvokeTime = float.Parse(myData[rowIndex++]);
		SkillBound = (SkillBound)Enum.Parse (typeof(SkillBound), myData[rowIndex++]);
		SkillTargetBound = int.Parse(myData[rowIndex++]);
		SkillTarget = (SkillTarget)Enum.Parse (typeof(SkillTarget), myData[rowIndex++]);
		SkillValue = float.Parse(myData[rowIndex++]);
		SkillValueType = (SkillValueType)Enum.Parse (typeof(SkillValueType), myData[rowIndex++]);
		RefSkillValueTarget = (RefSkillValueTarget)Enum.Parse (typeof(RefSkillValueTarget), myData[rowIndex++]);
		RefSkillStatusType = (StatusType)Enum.Parse (typeof(StatusType), myData[rowIndex++]);
		RefSkillValueAmount = float.Parse(myData[rowIndex++]);
		RefHealthType = (RefHealthType)Enum.Parse (typeof(RefHealthType), myData[rowIndex++]);
		BattleStateType = (BattleStateType)Enum.Parse (typeof(BattleStateType), myData[rowIndex++]);
		BattleStateSeconds = float.Parse(myData[rowIndex++]);
		InvokeParticleIndex = int.Parse(myData[rowIndex++]);
		ApplyParticleIndex = int.Parse(myData[rowIndex++]);
		AfterParticleIndex = int.Parse(myData[rowIndex++]);

    }
}
