// ExcelExporter 로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Character : TableDataBase
{
	/// <summary>
	/// 인덱스.
	/// </summary>
	public int Id;

	/// <summary>
	/// 캐릭터 이름 로컬 키값.
	/// </summary>
	public string Name;

	/// <summary>
	/// 캐릭터 설명 로컬 키값.
	/// </summary>
	public string Desc;

	/// <summary>
	/// 공격 범위 타입.
	/// </summary>
	public int AttackRangeType;

	/// <summary>
	/// 캐릭터 유형.
	/// </summary>
	public CharacterRoleType CharacterRoleType;

	/// <summary>
	/// 기본 공격 인덱스.
	/// </summary>
	public int AttackIndex;

	/// <summary>
	/// 기본 스킬 인덱스.
	/// </summary>
	public int SkillIndex;

	/// <summary>
	/// 투사체 프리팹 이름.
	/// </summary>
	public string BulletResName;

	/// <summary>
	/// 스프라이트 이름.
	/// </summary>
	public string SpriteResName;

	/// <summary>
	/// 애니메이터 이름.
	/// </summary>
	public string AnimatorResName;



    public Character ()
    {
    }


    public override void SetData (List<string> myData)
    {
        return;
    }
}