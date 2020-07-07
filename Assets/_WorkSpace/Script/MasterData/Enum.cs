// ExcelExporter로 자동 생성된 파일.

public enum DataType
{
	GlobalText = 1000,
	Character = 2000,
	CharacterLevel = 3000,
	Skill = 4000,
	Equipment = 5000,
	Status = 6000,
	StatusGrade = 7000,
	Stage = 8000,
	Particle = 9000,
	Combination = 10000,

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
	LostHealth = 2,
	MaxHealth = 3,
	HealthRegen = 4,
	SkillGageRegen = 5,
	Attack = 6,
	AbilityPoint = 7,
	Defense = 8,
	AtSpd = 9,
	CriticalProb = 10,
	CriticalDmg = 11,
	EvadeProb = 12,
	AmountOfAttackDamage = 13,
	AmountOfShotDamage = 14,

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
