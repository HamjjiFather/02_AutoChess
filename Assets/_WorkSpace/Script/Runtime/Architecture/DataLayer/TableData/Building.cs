// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Building : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 건물 타입.
	/// </summary>
	public BuildingType BuildingType;

	/// <summary>
	/// 건물 이름.
	/// </summary>
	public string BuildingName;



    public Building ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		BuildingType = (BuildingType)Enum.Parse (typeof(BuildingType), myData[rowIndex++]);
		BuildingName = myData[rowIndex++];
		
    }
}
