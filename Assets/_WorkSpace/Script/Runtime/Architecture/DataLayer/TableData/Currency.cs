// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Currency : Item
{
	/// <summary>
	/// 아이콘 이름.
	/// </summary>
	public string IconName;

		

    public Currency ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		GlobalIndex = myData[rowIndex++];
		StackAmount = int.Parse(myData[rowIndex++]);
		SellingPrice = int.Parse(myData[rowIndex++]);
		IconName = myData[rowIndex++];
		
    }
}