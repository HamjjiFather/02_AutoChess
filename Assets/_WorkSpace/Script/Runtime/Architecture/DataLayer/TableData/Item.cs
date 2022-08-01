// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Item : TableDataBase
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
	/// 이름.
	/// </summary>
	public string Name;

	/// <summary>
	/// 중첩 가능 여부.
	/// </summary>
	public bool Stackable;



    public Item ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		GlobalIndex = myData[rowIndex++];
		Name = myData[rowIndex++];
		Stackable = bool.Parse(myData[rowIndex++]);
		
    }
}
