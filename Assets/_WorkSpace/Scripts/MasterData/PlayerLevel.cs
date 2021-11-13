// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class PlayerLevel : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 레벨.
	/// </summary>
	public int Level;

	/// <summary>
	/// 레벨 스트링.
	/// </summary>
	public string LevelString;

	/// <summary>
	/// 누적경험치.
	/// </summary>
	public int AccReqExp;

	/// <summary>
	/// 필요 경험치.
	/// </summary>
	public int ReqExp;

	/// <summary>
	/// 이전 누적 경험치.
	/// </summary>
	public int CoExp;



    public PlayerLevel ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		Level = int.Parse(myData[rowIndex++]);
		LevelString = myData[rowIndex++];
		AccReqExp = int.Parse(myData[rowIndex++]);
		ReqExp = int.Parse(myData[rowIndex++]);
		CoExp = int.Parse(myData[rowIndex++]);
		
    }
}
