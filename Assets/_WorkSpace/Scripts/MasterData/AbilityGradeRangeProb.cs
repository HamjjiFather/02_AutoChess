// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class AbilityGradeRangeProb : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 등급 타입.
	/// </summary>
	public StatusGrade StatusGrade;

	/// <summary>
	/// 확률.
	/// </summary>
	public float Probability;



    public AbilityGradeRangeProb ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		StatusGrade = (StatusGrade)Enum.Parse (typeof(StatusGrade), myData[rowIndex++]);
		Probability = float.Parse(myData[rowIndex++]);
		
    }
}
