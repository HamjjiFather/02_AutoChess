// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Enemy : Character
{
	/// <summary>
	/// 부여되는 기본 주요 능력치.
	/// </summary>
	public int[] PrimeAbilities;

			/// <summary>
	/// 드랍 아이템 글로벌 인덱스.
	/// </summary>
	public string[] DropItemGlobalIndexes;

			/// <summary>
	/// 드랍 아이템 확률.
	/// </summary>
	public int[] DropItemProbabilities;

			/// <summary>
	/// 드랍 아이템 수량.
	/// </summary>
	public int[] DropItemAmounts;

		

    public Enemy ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
        Id = int.Parse(myData[rowIndex++]);
		Name = myData[rowIndex++];
		Desc = myData[rowIndex++];
		AttackRangeType = int.Parse(myData[rowIndex++]);
		CharacterRoleType = (CharacterRoleType)Enum.Parse (typeof(CharacterRoleType), myData[rowIndex++]);
		AttackIndex = int.Parse(myData[rowIndex++]);
		SkillIndex = int.Parse(myData[rowIndex++]);
		BulletResName = myData[rowIndex++];
		SpriteResName = myData[rowIndex++];
		AnimatorResName = myData[rowIndex++];
		PrimeAbilities = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		DropItemGlobalIndexes = myData[rowIndex++].Split ('/');
		DropItemProbabilities = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		DropItemAmounts = Array.ConvertAll (myData[rowIndex++].Split ('/'), int.Parse);
		
    }
}