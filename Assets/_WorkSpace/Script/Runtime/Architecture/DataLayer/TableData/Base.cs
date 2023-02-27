// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Base : Building
{
	/// <summary>
	/// 건물 타입.
	/// </summary>
	public BuildingType BuildingType;

		

    public Base ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		BuildingName = myData[rowIndex++];
		BuildingType = (BuildingType)Enum.Parse (typeof(BuildingType), myData[rowIndex++]);
		
    }
}