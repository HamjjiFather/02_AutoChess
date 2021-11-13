// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class EquipmentGradeProb : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 등급 부여 확률.
	/// </summary>
	public int[] ProbGrades;



    public EquipmentGradeProb ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		ProbGrades = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		
    }
}
