// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class CharacterLevel : TableDataBase
{
	// 인덱스.
	public int Id;
	// 레벨.
	public string LevelString;
	// 누적 EXP.
	public float AccReqExp;
	// 요구 EXP.
	public float ReqExp;
	// 보정 EXP.
	public float CoExp;


    public CharacterLevel ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		LevelString = myData[rowIndex++];
		AccReqExp = float.Parse(myData[rowIndex++]);
		ReqExp = float.Parse(myData[rowIndex++]);
		CoExp = float.Parse(myData[rowIndex++]);

    }
}
