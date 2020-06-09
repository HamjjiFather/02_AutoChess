// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class CharacterProbability : TableDataBase
{
	// 인덱스.
	public int Id;
	// 확률.
	public int[] Probs;


    public CharacterProbability ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		Probs = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);

    }
}
