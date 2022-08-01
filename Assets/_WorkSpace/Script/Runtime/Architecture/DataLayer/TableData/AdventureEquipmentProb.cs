// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class AdventureEquipmentProb : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 장비 출현 확률.
	/// </summary>
	public int[] EquipmentProbabilities;



    public AdventureEquipmentProb ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		EquipmentProbabilities = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		
    }
}
