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

public enum CharacterGroundType
{
	/// <summary>
	/// 지면에 위치함.
	/// </summary>
	Ground = 0,

	/// <summary>
	/// 공중에 위치함.
	/// </summary>
	Air = 1,


}

public enum EquipmentType
{
	/// <summary>
	/// 랜덤으로 능력치가 부여됨.
	/// </summary>
	Random = 0,

	/// <summary>
	/// 고정된 능력치가 있음.
	/// </summary>
	Artifact = 1,


}

public enum EquipmentGrade
{
	/// <summary>
	/// 1등급.
	/// </summary>
	Grade1 = 0,

	/// <summary>
	/// 2등급.
	/// </summary>
	Grade2 = 1,

	/// <summary>
	/// 3등급.
	/// </summary>
	Grade3 = 2,

	/// <summary>
	/// 4등급.
	/// </summary>
	Grade4 = 3,

	/// <summary>
	/// 5등급.
	/// </summary>
	Grade5 = 4,


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

public enum SkillBound
{
	/// <summary>
	/// 자신.
	/// </summary>
	Self = 0,

	/// <summary>
	/// 공격 대상.
	/// </summary>
	Target = 1,

	/// <summary>
	/// 자신과 주변.
	/// </summary>
	SelfArea = 2,

	/// <summary>
	/// 대상과 주변.
	/// </summary>
	TargetArea = 3,

	/// <summary>
	/// 자신 제외 주변.
	/// </summary>
	SelfAreaOnly = 4,

	/// <summary>
	/// 대상 제외 주변.
	/// </summary>
	TargetAreaOnly = 5,

	/// <summary>
	/// 대상 방향.
	/// </summary>
	TargetDirection = 6,

	/// <summary>
	/// 부채꼴.
	/// </summary>
	FanShape = 7,

	/// <summary>
	/// 가장 가까운.
	/// </summary>
	Nearest = 8,

	/// <summary>
	/// 가장 먼.
	/// </summary>
	Furthest = 9,

	/// <summary>
	/// 전체.
	/// </summary>
	All = 10,


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

public enum StatusGrade
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

public enum AbilityType
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
	/// 체력 재생.
	/// </summary>
	HealthRecovery = 1,

	/// <summary>
	/// 마력 수치.
	/// </summary>
	ManaPoint = 2,

	/// <summary>
	/// 마력 재생.
	/// </summary>
	ManaRecovery = 3,

	/// <summary>
	/// 물리 피해량.
	/// </summary>
	PhysicalDamage = 4,

	/// <summary>
	/// 마법 피해량.
	/// </summary>
	MagicalDamage = 5,

	/// <summary>
	/// 물리 저항력.
	/// </summary>
	PhysicalRegistance = 6,

	/// <summary>
	/// 마법 저항력.
	/// </summary>
	MagicalRegistance = 7,

	/// <summary>
	/// 행동 속도.
	/// </summary>
	BehaveSpeed = 8,

	/// <summary>
	/// 치명타 확률.
	/// </summary>
	CriticalProbability = 9,

	/// <summary>
	/// 치명타 데미지 배율.
	/// </summary>
	CriticalDamagePercent = 10,

	/// <summary>
	/// 회피 확률.
	/// </summary>
	EvadeProbability = 11,

	/// <summary>
	/// 화염 저항력.
	/// </summary>
	FlameRegistance = 12,

	/// <summary>
	/// 전격 저항력.
	/// </summary>
	ShockRegistance = 13,

	/// <summary>
	/// 빙결 저항력.
	/// </summary>
	FrozenRegistance = 14,


}

public enum AbilityProcType
{
	/// <summary>
	/// None.
	/// </summary>
	Number = 0,

	/// <summary>
	/// None.
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
	/// 영혼석.
	/// </summary>
	SoulStone = 1,

	/// <summary>
	/// 계승의 돌.
	/// </summary>
	StoneOfInheritance = 2,


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