// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Status : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 능력치 이름 키값
    /// </summary>
    public string NameKey;

    /// <summary>
    /// 능력치 타입
    /// </summary>
    public StatusType StatusType;

    /// <summary>
    /// 아이콘 이미지 이름
    /// </summary>
    public string IconImage;

    /// <summary>
    /// 자리수 표시 방식
    /// </summary>
    public string Format;



    public Status ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		NameKey = myData[rowIndex++];
		StatusType = (StatusType)Enum.Parse (typeof(StatusType), myData[rowIndex++]);
		IconImage = myData[rowIndex++];
		Format = myData[rowIndex++];

    }
}
