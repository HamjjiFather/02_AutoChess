// ExcelExporter 로 자동 생성된 파일.

public enum GlobalLanguageType
{
	/// <summary>
	/// 한국어.
	/// </summary>
	Korean = 0,

	/// <summary>
	/// 영어.
	/// </summary>
	English = 1,


}

public enum EnemyGradeType
{
	/// <summary>
	/// 신입.
	/// </summary>
	Rookie = 0,

	/// <summary>
	/// 숙련.
	/// </summary>
	Elite = 1,

	/// <summary>
	/// 이름있는.
	/// </summary>
	Named = 2,

	/// <summary>
	/// 악명높은.
	/// </summary>
	Notorious = 3,

	/// <summary>
	/// 전설적인.
	/// </summary>
	Legendary = 4,


}

public enum EquipmentDropType
{
	/// <summary>
	/// 장비 대분류 타입 - 공용.
	/// </summary>
	Common = 0,

	/// <summary>
	/// 장비 대분류 타입 - 고유.
	/// </summary>
	Unique = 1,


}

public enum EquipmentGradeType
{
	/// <summary>
	/// 장비 소분류 - 잘 못 만든.
	/// </summary>
	BadlyMade = 0,

	/// <summary>
	/// 공용 - 흔한.
	/// </summary>
	Common = 1,

	/// <summary>
	/// 공용 - 잘 만든.
	/// </summary>
	WellMade = 2,

	/// <summary>
	/// 공용 - 걸작의.
	/// </summary>
	MasterPiece = 3,

	/// <summary>
	/// 고유 - 일반.
	/// </summary>
	Unique = 4,

	/// <summary>
	/// 고유 - 전용의.
	/// </summary>
	Exclusive = 5,

	/// <summary>
	/// 고유 - 전설적인.
	/// </summary>
	Legendary = 6,

	/// <summary>
	/// 고유 - 유물.
	/// </summary>
	Relic = 7,


}

public enum SkillActiveCondition
{
	/// <summary>
	/// 기본 스킬.
	/// </summary>
	OnActive = 0,

	/// <summary>
	/// 실시간.
	/// </summary>
	OnAlways = 1,

	/// <summary>
	/// 공격시.
	/// </summary>
	OnAttack = 2,

	/// <summary>
	/// 공격 성공시.
	/// </summary>
	OnHit = 3,

	/// <summary>
	/// 피격시.
	/// </summary>
	OnShot = 4,

	/// <summary>
	/// 처치시.
	/// </summary>
	OnKill = 5,

	/// <summary>
	/// 사망시.
	/// </summary>
	OnDeath = 6,

	/// <summary>
	/// 아군 사망.
	/// </summary>
	OnAllyDeath = 7,

	/// <summary>
	/// 적군 사망.
	/// </summary>
	OnEnemyDeath = 8,

	/// <summary>
	/// 스킬 이후 발동.
	/// </summary>
	OnAfter = 9,


}

public enum StatusChangeType
{
	/// <summary>
	/// 증가.
	/// </summary>
	Increase = 0,

	/// <summary>
	/// 감소.
	/// </summary>
	Decrease = 1,


}

public enum SkillTarget
{
	/// <summary>
	/// 아군.
	/// </summary>
	Ally = 0,

	/// <summary>
	/// 적군.
	/// </summary>
	Enemy = 1,


}

public enum RefSkillValueTarget
{
	/// <summary>
	/// 시전자의 능력치 참조.
	/// </summary>
	Self = 0,

	/// <summary>
	/// 대상의 능력치 참조.
	/// </summary>
	Target = 1,

	/// <summary>
	/// 스킬 피해량.
	/// </summary>
	Damage = 2,


}

public enum ItemGrade
{
	/// <summary>
	/// D.
	/// </summary>
	D = 0,

	/// <summary>
	/// C.
	/// </summary>
	C = 1,

	/// <summary>
	/// B.
	/// </summary>
	B = 2,

	/// <summary>
	/// A.
	/// </summary>
	A = 3,

	/// <summary>
	/// S.
	/// </summary>
	S = 4,


}

public enum PrimeAbilityType
{
	/// <summary>
	/// 신체.
	/// </summary>
	Body = 0,

	/// <summary>
	/// 정신.
	/// </summary>
	Mentality = 1,

	/// <summary>
	/// 기술.
	/// </summary>
	Skill = 2,

	/// <summary>
	/// 속도.
	/// </summary>
	Speed = 3,

	/// <summary>
	/// 지혜.
	/// </summary>
	Wisdom = 4,


}

public enum SubAbilityType
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = -1,

	/// <summary>
	/// 체력 수치.
	/// </summary>
	HealthPoint = 0,

	/// <summary>
	/// 마력 수치.
	/// </summary>
	ManaPoint = 1,

	/// <summary>
	/// 물리 공격력.
	/// </summary>
	PhysicalDamage = 2,

	/// <summary>
	/// 마법 공격력.
	/// </summary>
	MagicalDamage = 3,

	/// <summary>
	/// 물리 저항력.
	/// </summary>
	PhysicalRegistance = 4,

	/// <summary>
	/// 마법 저항력.
	/// </summary>
	MagicalRegistance = 5,

	/// <summary>
	/// 행동 속도.
	/// </summary>
	BehaveSpeed = 6,

	/// <summary>
	/// 치명타 확률.
	/// </summary>
	CriticalProbability = 7,

	/// <summary>
	/// 치명타 피해량.
	/// </summary>
	CriticalDamagePercent = 8,

	/// <summary>
	/// 명중율.
	/// </summary>
	HitProbability = 9,

	/// <summary>
	/// 회피율.
	/// </summary>
	EvadeProbability = 10,

	/// <summary>
	/// 가하는 피해량.
	/// </summary>
	InflictedDamage = 10,

	/// <summary>
	/// 받는 피해량.
	/// </summary>
	IncomeDamage = 11,

	/// <summary>
	/// 가하는 회복량.
	/// </summary>
	InflictedHeal = 12,

	/// <summary>
	/// 받는 회복량.
	/// </summary>
	IncomeHeal = 13,

	/// <summary>
	/// 흡혈.
	/// </summary>
	Bloodthirst = 14,


}

public enum AbilityProcType
{
	/// <summary>
	/// 수량.
	/// </summary>
	Number = 0,

	/// <summary>
	/// 퍼센트.
	/// </summary>
	Percent = 1,


}

public enum RefHealthType
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = 0,

	/// <summary>
	/// 최대체력.
	/// </summary>
	MaxHealth = 1,

	/// <summary>
	/// 현재체력.
	/// </summary>
	NowHealth = 2,

	/// <summary>
	/// 잃은체력.
	/// </summary>
	LostHealth = 3,


}

public enum BattleStateType
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = 0,

	/// <summary>
	/// 저주.
	/// </summary>
	Curse = 1,

	/// <summary>
	/// 기절.
	/// </summary>
	Stun = 2,

	/// <summary>
	/// 반사.
	/// </summary>
	Reflex = 3,


}

public enum SkillValueType
{
	/// <summary>
	/// 정수.
	/// </summary>
	Number = 0,

	/// <summary>
	/// 비율.
	/// </summary>
	Percent = 1,


}

public enum CurrencyType
{
	/// <summary>
	/// 골드.
	/// </summary>
	Gold = 0,

	/// <summary>
	/// 전승지식.
	/// </summary>
	Lore = 1,

	/// <summary>
	/// 탐험물자.
	/// </summary>
	ExploreGoods = 2,


}

public enum CharacterRoleType
{
	/// <summary>
	/// 근접 공격.
	/// </summary>
	Melee = 0,

	/// <summary>
	/// 원거리 공격.
	/// </summary>
	Range = 1,

	/// <summary>
	/// 암살.
	/// </summary>
	Assassin = 2,


}

public enum OfficeSkillBranchType
{
	/// <summary>
	/// 전투 .
	/// </summary>
	Battle = 0,

	/// <summary>
	/// 탐험.
	/// </summary>
	Adventure = 1,

	/// <summary>
	/// 운영.
	/// </summary>
	Management = 2,


}

public enum OfficeSkillSpentType
{
	/// <summary>
	/// 퍽.
	/// </summary>
	Perk = 0,

	/// <summary>
	/// 스킬.
	/// </summary>
	Skill = 1,


}

public enum GameSystemType
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = 0,

	/// <summary>
	/// 우선 목표 지정 기능.
	/// </summary>
	PrimeTarget = 1,

	/// <summary>
	/// 적 규모 파악 기능.
	/// </summary>
	FigureOutEnemySize = 2,

	/// <summary>
	/// 적 상세 정보 파악 기능.
	/// </summary>
	FigureOutEnemyDetail = 3,

	/// <summary>
	/// 미식별 장비 등급 파악 기능.
	/// </summary>
	FigureOutUnIdentifiedGear = 4,

	/// <summary>
	/// 숲 지형에서 적 기습.
	/// </summary>
	AmbushInForest = 5,


}

public enum BattleInteractableAreaType
{
	/// <summary>
	/// 자신.
	/// </summary>
	Me = 0,

	/// <summary>
	/// 자신 주변 1칸.
	/// </summary>
	Around1 = 1,

	/// <summary>
	/// 자신 주변 2칸.
	/// </summary>
	Around2 = 2,

	/// <summary>
	/// 자신 주변 3칸.
	/// </summary>
	Around3 = 3,

	/// <summary>
	/// 6방향으로 2칸.
	/// </summary>
	Star1 = 4,

	/// <summary>
	/// 6방향으로 3칸.
	/// </summary>
	Star2 = 5,

	/// <summary>
	/// 6방향으로 4칸.
	/// </summary>
	Star3 = 6,

	/// <summary>
	/// 앞방향 1칸.
	/// </summary>
	Front1 = 7,

	/// <summary>
	/// 앞방향 2칸.
	/// </summary>
	Front2 = 8,

	/// <summary>
	/// 앞방향 3칸.
	/// </summary>
	Front3 = 9,

	/// <summary>
	/// 전방 3방향으로 1칸.
	/// </summary>
	Fork1 = 10,

	/// <summary>
	/// 전방 3방향으로 2칸.
	/// </summary>
	Fork2 = 11,

	/// <summary>
	/// 전방 3방향으로 3칸.
	/// </summary>
	Fork3 = 12,

	/// <summary>
	/// 자신 기준 2칸.
	/// </summary>
	Artillery1 = 13,

	/// <summary>
	/// 자신 기준 3칸.
	/// </summary>
	Artillery2 = 14,

	/// <summary>
	/// 자신 기준 4칸.
	/// </summary>
	Artillery3 = 15,


}

public enum BattleSideType
{
	/// <summary>
	/// 플레이어 유닛.
	/// </summary>
	Player = 0,

	/// <summary>
	/// 적 유닛.
	/// </summary>
	Enemy = 1,


}

public enum BuildingType
{
	/// <summary>
	/// 탐험대.
	/// </summary>
	ExploreOffice = 0,

	/// <summary>
	/// 대장간.
	/// </summary>
	BlackSmith = 1,

	/// <summary>
	/// 고용소.
	/// </summary>
	EmploymentOffice = 2,

	/// <summary>
	/// 창고.
	/// </summary>
	Warehouse = 3,

	/// <summary>
	/// 묘지.
	/// </summary>
	Graveyard = 4,


}