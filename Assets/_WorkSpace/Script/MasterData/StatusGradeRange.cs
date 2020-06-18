// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class StatusGradeRange : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 등급 타입
    /// </summary>
    public StatusGrade StatusGrade;

    /// <summary>
    /// 최소값(이상)
    /// </summary>
    public float Min;

    /// <summary>
    /// 최대값(미만)
    /// </summary>
    public float Max;

    /// <summary>
    /// 등급
    /// </summary>
    public string GradeString;



    public StatusGradeRange ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		StatusGrade = (StatusGrade)Enum.Parse (typeof(StatusGrade), myData[rowIndex++]);
		Min = float.Parse(myData[rowIndex++]);
		Max = float.Parse(myData[rowIndex++]);
		GradeString = myData[rowIndex++];

    }
}
