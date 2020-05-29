// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class SpecialPuzzle : TableDataBase
{
	// 인덱스.
	public int Id;
	// 퍼즐 타입.
	public PuzzleMatchingType PuzzleMatchingType;
	// 결과 값.
	public int CheckValue;


    public SpecialPuzzle ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		PuzzleMatchingType = (PuzzleMatchingType)Enum.Parse (typeof(PuzzleMatchingType), myData[rowIndex++]);
		CheckValue = int.Parse(myData[rowIndex++]);

    }
}
