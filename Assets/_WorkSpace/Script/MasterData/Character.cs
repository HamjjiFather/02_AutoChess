// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Character : TableDataBase
{
	// 인덱스.
	public int Id;
	// 이름.
	public string Name;
	// 설명.
	public string Desc;
	// 시작등급.
	public Grade StartGrade;
	// 체력.
	public int Hp;
	// 체력 증가값.
	public int HpInc;
	// 공격력.
	public int At;
	// 공격력 증가값.
	public int AtInc;
	// 방어력.
	public int Df;
	// 방어력 증가값.
	public int DfInc;


    public Character ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		Name = myData[rowIndex++];
		Desc = myData[rowIndex++];
		StartGrade = (Grade)Enum.Parse (typeof(Grade), myData[rowIndex++]);
		Hp = int.Parse(myData[rowIndex++]);
		HpInc = int.Parse(myData[rowIndex++]);
		At = int.Parse(myData[rowIndex++]);
		AtInc = int.Parse(myData[rowIndex++]);
		Df = int.Parse(myData[rowIndex++]);
		DfInc = int.Parse(myData[rowIndex++]);

    }
}
