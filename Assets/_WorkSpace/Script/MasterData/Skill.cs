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
    /// 스킬 범위
    /// </summary>
    public SkillBound SkillBound;

    /// <summary>
    /// 스킬 대상
    /// </summary>
    public SkillTarget SkillTarget;

    /// <summary>
    /// 스킬 범위
    /// </summary>
    public int SkillTargetBound;

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



    public Skill ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		SkillActiveCondition = (SkillActiveCondition)Enum.Parse (typeof(SkillActiveCondition), myData[rowIndex++]);
		SkillStatusType = (StatusType)Enum.Parse (typeof(StatusType), myData[rowIndex++]);
		StatusChangeType = (StatusChangeType)Enum.Parse (typeof(StatusChangeType), myData[rowIndex++]);
		InvokeCount = int.Parse(myData[rowIndex++]);
		InvokeTime = float.Parse(myData[rowIndex++]);
		SkillBound = (SkillBound)Enum.Parse (typeof(SkillBound), myData[rowIndex++]);
		SkillTarget = (SkillTarget)Enum.Parse (typeof(SkillTarget), myData[rowIndex++]);
		SkillTargetBound = int.Parse(myData[rowIndex++]);
		RefSkillValueTarget = (RefSkillValueTarget)Enum.Parse (typeof(RefSkillValueTarget), myData[rowIndex++]);
		RefSkillStatusType = (StatusType)Enum.Parse (typeof(StatusType), myData[rowIndex++]);
		RefSkillValueAmount = float.Parse(myData[rowIndex++]);

    }
}
