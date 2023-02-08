// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Trophy : Item
{
	/// <summary>
	/// 아이템 등급.
	/// </summary>
	public ItemGrade ItemGrade;

		

    public Trophy ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		GlobalIndex = myData[rowIndex++];
		StackAmount = int.Parse(myData[rowIndex++]);
		SellingPrice = int.Parse(myData[rowIndex++]);
		ItemGrade = (ItemGrade)Enum.Parse (typeof(ItemGrade), myData[rowIndex++]);
		
    }
}