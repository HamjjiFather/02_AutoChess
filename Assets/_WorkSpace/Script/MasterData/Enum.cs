// ExcelExporter 로 자동 생성된 파일.

public enum DataType
{
	None = 0,
	Character = 1000,
	CharacterLevel = 2000,
	Skill = 3000,
	Equipment = 4000,
	EquipmentStatus = 5000,
	Status = 6000,
	StatusGrade = 7000,
	BattleStage = 8000,
	Particle = 9000,
	BattleState = 10000,
	Combination = 11000,
	Currency = 12000,
	AdventureField = 13000,

}

public enum GlobalLanguageType
{
	Korean = 0,
	English = 1,

}

public enum StarGrade
{
	None = -1,
	Grade1 = 0,
	Grade2 = 1,
	Grade3 = 2,
	Grade4 = 3,
	Grade5 = 4,

}

public enum CharacterGroundType
{
	Ground = 0,
	Air = 1,

}

public enum EquipmentType
{
	Random = 0,
	Artifact = 1,

}

public enum EquipmentGrade
{
	Grade1 = 0,
	Grade2 = 1,
	Grade3 = 2,
	Grade4 = 3,
	Grade5 = 4,

}

public enum SkillActiveCondition
{
	OnActive = 0,
	OnAlways = 1,
	OnAttack = 2,
	OnHit = 3,
	OnShot = 4,
	OnKill = 5,
	OnDeath = 6,
	OnAllyDeath = 7,
	OnEnemyDeath = 8,
	OnAfter = 9,

}

public enum StatusChangeType
{
	Increase = 0,
	Decrease = 1,

}

public enum SkillBound
{
	Self = 0,
	Target = 1,
	SelfArea = 2,
	TargetArea = 3,
	SelfAreaOnly = 4,
	TargetAreaOnly = 5,
	TargetDirection = 6,
	FanShape = 7,
	Nearest = 8,
	Furthest = 9,
	All = 10,

}

public enum SkillTarget
{
	Ally = 0,
	Enemy = 1,

}

public enum RefSkillValueTarget
{
	Self = 0,
	Target = 1,
	Damage = 2,

}

public enum StatusGrade
{
	Grade1 = 0,
	Grade2 = 1,
	Grade3 = 2,
	Grade4 = 3,
	Grade5 = 4,
	Grade6 = 5,
	Grade7 = 6,
	Grade8 = 7,

}

public enum StatusType
{
	None = 0,
	Health = 1,
	HealthRegen = 2,
	SkillGage = 3,
	SkillGageRegen = 4,
	Attack = 5,
	AbilityPoint = 6,
	Defense = 7,
	AttackSpeed = 8,
	CriticalProbability = 9,
	CriticalDamage = 10,
	EvadeProbability = 11,
	HitDamage = 12,
	ShotDamage = 13,
	HitHeal = 14,
	ShotHeal = 15,
	MoveSpeed = 16,

}

public enum RefHealthType
{
	None = 0,
	MaxHealth = 1,
	NowHealth = 2,
	LostHealth = 3,

}

public enum BattleStateType
{
	None = 0,
	Curse = 1,
	Stun = 2,
	Reflex = 3,

}

public enum SkillValueType
{
	Number = 0,
	Percent = 1,

}

public enum CurrencyType
{
	Gold = 0,
	SoulStone = 1,

}

public enum BattleCharacterType
{
	Melee = 0,
	Range = 1,
	Assassin = 2,

}
