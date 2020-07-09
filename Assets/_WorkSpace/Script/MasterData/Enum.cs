// ExcelExporter로 자동 생성된 파일.

public enum DataType
{
	GlobalText = 1000,
	Character = 2000,
	CharacterLevel = 3000,
	Skill = 4000,
	Equipment = 5000,
	EquipmentStatus = 6000,
	Status = 7000,
	StatusGrade = 8000,
	Stage = 9000,
	Particle = 10000,
	Combination = 11000,

}

public enum GlobalLanguageType
{
	Korean = 0,
	English = 1,

}

public enum CharacterGrade
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
	All = 8,

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
	AttackDamage = 12,
	ShotDamage = 13,

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
