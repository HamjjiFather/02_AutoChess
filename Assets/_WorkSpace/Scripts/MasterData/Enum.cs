// ExcelExporter 로 자동 생성된 파일.

public enum DataType
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = 0,

	/// <summary>
	/// 캐릭터 데이터.
	/// </summary>
	Character = 1000,

	/// <summary>
	/// 캐릭터 레벨 데이터.
	/// </summary>
	CharacterLevel = 2000,

	/// <summary>
	/// 스킬 데이터.
	/// </summary>
	Skill = 3000,

	/// <summary>
	/// 장비 데이터.
	/// </summary>
	Equipment = 4000,

	/// <summary>
	/// 장비 부여 능력치 데이터.
	/// </summary>
	EquipmentAbility = 5000,

	/// <summary>
	/// 능력치 데이터.
	/// </summary>
	Ability = 6000,

	/// <summary>
	/// 능력치 단계 데이터.
	/// </summary>
	AbilityGrade = 7000,

	/// <summary>
	/// 스테이지 데이터.
	/// </summary>
	BattleStage = 8000,

	/// <summary>
	/// 파티클 데이터.
	/// </summary>
	Particle = 9000,

	/// <summary>
	/// 전투상태 데이터.
	/// </summary>
	BattleState = 10000,

	/// <summary>
	/// 조합 데이터.
	/// </summary>
	Combination = 11000,

	/// <summary>
	/// 재화 데이터.
	/// </summary>
	Currency = 12000,

	/// <summary>
	/// 모험 필드 데이터.
	/// </summary>
	AdventureField = 13000,

	/// <summary>
	/// 모험 필드 출현 장비 데이터.
	/// </summary>
	AdventureEquipment = 14000,

	/// <summary>
	/// 모험 필드 출현 장비 확률 데이터.
	/// </summary>
	AdventureEquipmentProb = 15000,

	/// <summary>
	/// 장비 등급 부여 확률.
	/// </summary>
	EquipmentGradeProb = 16000,

	/// <summary>
	/// 플레이어 레벨.
	/// </summary>
	PlayerLevel = 17000,


}

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

public enum StarGrade
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = -1,

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
	/// F.
	/// </summary>
	Grade1 = 0,

	/// <summary>
	/// D.
	/// </summary>
	Grade2 = 1,

	/// <summary>
	/// C.
	/// </summary>
	Grade3 = 2,

	/// <summary>
	/// B.
	/// </summary>
	Grade4 = 3,

	/// <summary>
	/// A.
	/// </summary>
	Grade5 = 4,

	/// <summary>
	/// S.
	/// </summary>
	Grade6 = 5,

	/// <summary>
	/// SS.
	/// </summary>
	Grade7 = 6,

	/// <summary>
	/// SSS.
	/// </summary>
	Grade8 = 7,


}

public enum AbilityType
{
	/// <summary>
	/// 없음.
	/// </summary>
	None = 0,

	/// <summary>
	/// 체력.
	/// </summary>
	Health = 1,

	/// <summary>
	/// 체력 재생.
	/// </summary>
	HealthRecovery = 2,

	/// <summary>
	/// 스킬 게이지.
	/// </summary>
	SkillGage = 3,

	/// <summary>
	/// 스킬 게이지 재생.
	/// </summary>
	SkillGageRecovery = 4,

	/// <summary>
	/// 공격.
	/// </summary>
	Attack = 5,

	/// <summary>
	/// 주문력.
	/// </summary>
	AbilityPoint = 6,

	/// <summary>
	/// 방어.
	/// </summary>
	Defense = 7,

	/// <summary>
	/// 공속.
	/// </summary>
	AttackSpeed = 8,

	/// <summary>
	/// 치명타 확률.
	/// </summary>
	CriticalProbability = 9,

	/// <summary>
	/// 치명타 데미지 배율.
	/// </summary>
	CriticalDamage = 10,

	/// <summary>
	/// 회피 확률.
	/// </summary>
	EvadeProbability = 11,

	/// <summary>
	/// 가하는 피해량.
	/// </summary>
	HitDamage = 12,

	/// <summary>
	/// 받는 피해량.
	/// </summary>
	ShotDamage = 13,

	/// <summary>
	/// 가하는 회복량.
	/// </summary>
	HitHeal = 14,

	/// <summary>
	/// 받는 회복량.
	/// </summary>
	ShotHeal = 15,

	/// <summary>
	/// 이동속도.
	/// </summary>
	MoveSpeed = 16,


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