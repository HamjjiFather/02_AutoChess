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

	/// <summary>
	/// 기본 내구도.
	/// </summary>
	public int BaseDurability;

	/// <summary>
	/// 강화에 필요한 기본 재화량.
	/// </summary>
	public int BaseReqCurrencyAmountForEnhance;

	/// <summary>
	/// 레벨당 강화에 필요한 추가 재화량.
	/// </summary>
	public int AddReqCurrencyAmountForEnhance;

	/// <summary>
	/// 기본 가격.
	/// </summary>
	public int BasePrice;



    public EquipmentGrade ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		EquipmentGradeType = (EquipmentGradeType)Enum.Parse (typeof(EquipmentGradeType), myData[rowIndex++]);
		SlotAmount = int.Parse(myData[rowIndex++]);
		BaseDurability = int.Parse(myData[rowIndex++]);
		BaseReqCurrencyAmountForEnhance = int.Parse(myData[rowIndex++]);
		AddReqCurrencyAmountForEnhance = int.Parse(myData[rowIndex++]);
		BasePrice = int.Parse(myData[rowIndex++]);
		
    }
}
