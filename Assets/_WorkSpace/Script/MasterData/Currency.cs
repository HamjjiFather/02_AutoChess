// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Currency : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 재화 타입
    /// </summary>
    public CurrencyType CurrencyType;



    public Currency ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		CurrencyType = (CurrencyType)Enum.Parse (typeof(CurrencyType), myData[rowIndex++]);

    }
}
