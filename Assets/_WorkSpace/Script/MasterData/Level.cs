// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Level : TableDataBase
{
	// 인덱스.
	public int Id;
	// 레벨.
	public string LevelString;
	// 누적 EXP.
	public int AccReqExp;
	// 요구 EXP.
	public int ReqExp;
	// 보정 EXP.
	public int CoExp;


    public Level ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		LevelString = myData[rowIndex++];
		AccReqExp = int.Parse(myData[rowIndex++]);
		ReqExp = int.Parse(myData[rowIndex++]);
		CoExp = int.Parse(myData[rowIndex++]);

    }
}
