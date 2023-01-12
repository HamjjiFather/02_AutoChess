// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class EquipmentAbilityGrade : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 기본 가격.
	/// </summary>
	public int BasePrice;



    public EquipmentAbilityGrade ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		BasePrice = int.Parse(myData[rowIndex++]);
		
    }
}
