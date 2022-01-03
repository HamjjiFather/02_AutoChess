// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Ability : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 능력치 이름 키값.
	/// </summary>
	public string NameKey;

	/// <summary>
	/// 능력치 타입.
	/// </summary>
	public AbilityType AbilityType;

	/// <summary>
	/// 아이콘 이미지 이름.
	/// </summary>
	public string IconImage;

	/// <summary>
	/// 능력치 기본 값.
	/// </summary>
	public int BaseValue;

	/// <summary>
	/// 능력치 최대 기준값.
	/// </summary>
	public int MaxValue;

	/// <summary>
	/// 자리수 표시 방식.
	/// </summary>
	public string Format;



    public Ability ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		NameKey = myData[rowIndex++];
		AbilityType = (AbilityType)Enum.Parse (typeof(AbilityType), myData[rowIndex++]);
		IconImage = myData[rowIndex++];
		BaseValue = int.Parse(myData[rowIndex++]);
		MaxValue = int.Parse(myData[rowIndex++]);
		Format = myData[rowIndex++];
		
    }
}
