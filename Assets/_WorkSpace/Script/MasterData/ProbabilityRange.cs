// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class ProbabilityRange : TableDataBase
{
	// 인덱스.
	public int Id;
	// CharacterProbability 테이블 인덱스.
	public int CharProbIdx;
	// 최소.
	public float Min;
	// 최대.
	public float Max;


    public ProbabilityRange ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		CharProbIdx = int.Parse(myData[rowIndex++]);
		Min = float.Parse(myData[rowIndex++]);
		Max = float.Parse(myData[rowIndex++]);

    }
}
