// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class OutpostExtend : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 건물 이름.
	/// </summary>
	public string BuildingName;

	/// <summary>
	/// 증축 건물 타입.
	/// </summary>
	public OutpostExtendType OutpostExtendType;



    public OutpostExtend ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		BuildingName = myData[rowIndex++];
		OutpostExtendType = (OutpostExtendType)Enum.Parse (typeof(OutpostExtendType), myData[rowIndex++]);
		
    }
}
