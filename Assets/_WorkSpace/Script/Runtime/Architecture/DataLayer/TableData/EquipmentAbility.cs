// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class EquipmentAbility : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 부여되는 능력치 타입.
	/// </summary>
	public SubAbilityType AbilityType;

	/// <summary>
	/// 최소값(포함).
	/// </summary>
	public float Min;

	/// <summary>
	/// 최대값(포함).
	/// </summary>
	public float Max;



    public EquipmentAbility ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		AbilityType = (SubAbilityType)Enum.Parse (typeof(SubAbilityType), myData[rowIndex++]);
		Min = float.Parse(myData[rowIndex++]);
		Max = float.Parse(myData[rowIndex++]);
		
    }
}
