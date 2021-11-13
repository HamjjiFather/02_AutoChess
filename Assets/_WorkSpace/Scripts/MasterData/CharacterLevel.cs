// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class CharacterLevel : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 실제 레벨.
	/// </summary>
	public int Level;

	/// <summary>
	/// 레벨.
	/// </summary>
	public string LevelString;

	/// <summary>
	/// 누적 EXP.
	/// </summary>
	public float AccReqExp;

	/// <summary>
	/// 요구 EXP.
	/// </summary>
	public float ReqExp;

	/// <summary>
	/// 보정 EXP.
	/// </summary>
	public float CoExp;



    public CharacterLevel ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		Level = int.Parse(myData[rowIndex++]);
		LevelString = myData[rowIndex++];
		AccReqExp = float.Parse(myData[rowIndex++]);
		ReqExp = float.Parse(myData[rowIndex++]);
		CoExp = float.Parse(myData[rowIndex++]);
		
    }
}
