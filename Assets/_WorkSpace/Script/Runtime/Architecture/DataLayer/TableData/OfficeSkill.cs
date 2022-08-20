// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class OfficeSkill : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 분야.
	/// </summary>
	public OfficeSkillBranchType BranchType;

	/// <summary>
	/// 투자 타입.
	/// </summary>
	public OfficeSkillSpentType SpentType;

	/// <summary>
	/// 요구 레벨.
	/// </summary>
	public int[] RequireLevels;

	/// <summary>
	/// 해금되는 게임 기능.
	/// </summary>
	public GameSystemType UnlockGameSystem;



    public OfficeSkill ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		BranchType = (OfficeSkillBranchType)Enum.Parse (typeof(OfficeSkillBranchType), myData[rowIndex++]);
		SpentType = (OfficeSkillSpentType)Enum.Parse (typeof(OfficeSkillSpentType), myData[rowIndex++]);
		RequireLevels = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		UnlockGameSystem = (GameSystemType)Enum.Parse (typeof(GameSystemType), myData[rowIndex++]);
		
    }
}
