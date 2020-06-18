// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Stage : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 출현 몬스터
    /// </summary>
    public int[] MonsterIndexes;

    /// <summary>
    /// 출현 몬스터 레벨
    /// </summary>
    public int[] MonsterLevels;

    /// <summary>
    /// 출현 몬스터 위치
    /// </summary>
    public string[] MonsterPosition;



    public Stage ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		MonsterIndexes = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		MonsterLevels = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		MonsterPosition = myData[rowIndex++].Split ('/');

    }
}
