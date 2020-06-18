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
    /// 스킬 발동 조건
    /// </summary>
    public SkillActiveCondition SkillActiveCondition;



    public Skill ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		SkillActiveCondition = (SkillActiveCondition)Enum.Parse (typeof(SkillActiveCondition), myData[rowIndex++]);

    }
}
