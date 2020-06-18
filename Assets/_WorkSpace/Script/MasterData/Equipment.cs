// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Equipment : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 장비 이름 로컬 키값
    /// </summary>
    public string Name;

    /// <summary>
    /// 장비 타입
    /// </summary>
    public EquipmentType EquipmentType;

    /// <summary>
    /// 장비 등급
    /// </summary>
    public EquipmentGrade EquipmentGrade;



    public Equipment ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		Name = myData[rowIndex++];
		EquipmentType = (EquipmentType)Enum.Parse (typeof(EquipmentType), myData[rowIndex++]);
		EquipmentGrade = (EquipmentGrade)Enum.Parse (typeof(EquipmentGrade), myData[rowIndex++]);

    }
}
