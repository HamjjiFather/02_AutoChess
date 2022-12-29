// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Equipment : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 글로벌 인덱스.
	/// </summary>
	public string GlobalIndex;

	/// <summary>
	/// 장비 이름 로컬 키값.
	/// </summary>
	public string Name;

	/// <summary>
	/// 장비 타입.
	/// </summary>
	public EquipmentGradeType EquipmentGradeType;

	/// <summary>
	/// 고정 능력치 인덱스 - EquipmentAbility참조.
	/// </summary>
	public int[] BaseEquipmentStatusIndexes;

	/// <summary>
	/// 랜덤 부여 가능 능력치 인덱스 - EquipmentAbility참조.
	/// </summary>
	public int[] AvailEquipmentTypeIndex;

	/// <summary>
	/// 스프라이트 이름.
	/// </summary>
	public string SpriteResName;



    public Equipment ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		GlobalIndex = myData[rowIndex++];
		Name = myData[rowIndex++];
		EquipmentGradeType = (EquipmentGradeType)Enum.Parse (typeof(EquipmentGradeType), myData[rowIndex++]);
		BaseEquipmentStatusIndexes = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		AvailEquipmentTypeIndex = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		SpriteResName = myData[rowIndex++];
		
    }
}
