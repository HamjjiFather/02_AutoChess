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
	All = 6,

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
	Health = 0,
	HealthRegen = 1,
	SkillGageRegen = 2,
	Attack = 3,
	AbilityPoint = 4,
	Defense = 5,
	AtSpd = 6,
	CriticalProb = 7,
	CriticalDmg = 8,
	EvadeProb = 9,
	AmountOfAttackDamage = 10,
	AmountOfShotDamage = 11,

}
