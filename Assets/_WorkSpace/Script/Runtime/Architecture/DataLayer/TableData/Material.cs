// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Material : Item
{


    public Material ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		GlobalIndex = myData[rowIndex++];
		StackAmount = int.Parse(myData[rowIndex++]);
		SellingPrice = int.Parse(myData[rowIndex++]);
		
    }
}