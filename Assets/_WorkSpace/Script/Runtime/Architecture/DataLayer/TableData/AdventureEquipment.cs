// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class AdventureEquipment : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 아이템 확률 테이블 인덱스.
	/// </summary>
	public int EquipmentProbIndex;

	/// <summary>
	/// 장비 아이템 인덱스.
	/// </summary>
	public int[] EquipmentIndexes;



    public AdventureEquipment ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		EquipmentProbIndex = int.Parse(myData[rowIndex++]);
		EquipmentIndexes = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		
    }
}
