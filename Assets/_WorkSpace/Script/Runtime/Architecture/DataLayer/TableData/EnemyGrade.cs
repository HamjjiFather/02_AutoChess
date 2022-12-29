// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class EnemyGrade : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 인덱스.
	/// </summary>
	public EnemyGradeType EnemyGradeType;

	/// <summary>
	/// 장비를 가질 확률.
	/// </summary>
	public int[] EquipmentProb;



    public EnemyGrade ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		EnemyGradeType = (EnemyGradeType)Enum.Parse (typeof(EnemyGradeType), myData[rowIndex++]);
		EquipmentProb = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		
    }
}
