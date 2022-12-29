// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class EquipmentGrade : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 장비 타입.
	/// </summary>
	public EquipmentGradeType EquipmentGradeType;

	/// <summary>
	/// 장비의 슬롯 수량.
	/// </summary>
	public int SlotAmount;



    public EquipmentGrade ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		EquipmentGradeType = (EquipmentGradeType)Enum.Parse (typeof(EquipmentGradeType), myData[rowIndex++]);
		SlotAmount = int.Parse(myData[rowIndex++]);
		
    }
}
