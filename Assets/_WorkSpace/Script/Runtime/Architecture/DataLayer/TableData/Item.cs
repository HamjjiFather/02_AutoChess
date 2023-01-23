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
	/// 중첩 수량.
	/// </summary>
	public int StackAmount;

	/// <summary>
	/// 판매 가격.
	/// </summary>
	public int SellingPrice;



    public Item ()
    {
    }


    public override void SetData (List<string> myData)
    {
        return;
    }
}