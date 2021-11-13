// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class AdventureField : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 출현 장비 데이터.
	/// </summary>
	public int AppearedEquipmentGroupIndex;

	/// <summary>
	/// 필드 특정 지역 수량.
	/// </summary>
	public int[] FieldPointCount;

	/// <summary>
	/// 필드 수량.
	/// </summary>
	public int[] FieldCount;

	/// <summary>
	/// 숲 수량.
	/// </summary>
	public int[] ForestCount;

	/// <summary>
	/// 숲에 배치되는 나무타일의 수량.
	/// </summary>
	public int[] TreeCount;

	/// <summary>
	/// 보물상자의 수량.
	/// </summary>
	public int[] RewardCount;

	/// <summary>
	/// 전투 수량.
	/// </summary>
	public int[] BattleCount;

	/// <summary>
	/// 보스 전투 수량.
	/// </summary>
	public int[] BossBattleCount;

	/// <summary>
	/// 이벤트 수량.
	/// </summary>
	public int[] EventCount;

	/// <summary>
	/// 기타 수량.
	/// </summary>
	public int[] OtherCount;



    public AdventureField ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		AppearedEquipmentGroupIndex = int.Parse(myData[rowIndex++]);
		FieldPointCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		FieldCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		ForestCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		TreeCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		RewardCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		BattleCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		BossBattleCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		EventCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		OtherCount = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		
    }
}
