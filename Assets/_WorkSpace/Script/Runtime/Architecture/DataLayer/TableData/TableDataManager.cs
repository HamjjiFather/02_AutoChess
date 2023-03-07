// ExcelExporter 로 자동 생성된 파일.

using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.TableData;
using Cysharp.Threading.Tasks;

public class TableDataManager : Singleton<TableDataManager>
{
    public readonly Dictionary<int, TableDataBase> TotalDataDict = new Dictionary<int, TableDataBase> ();

    public Dictionary<int, PlayerLevel> PlayerLevelDict = new Dictionary<int, PlayerLevel> ();
	 public Dictionary<int, PlayableCharacter> PlayableCharacterDict = new Dictionary<int, PlayableCharacter> ();
	 public Dictionary<int, EnemyGrade> EnemyGradeDict = new Dictionary<int, EnemyGrade> ();
	 public Dictionary<int, CharacterLevel> CharacterLevelDict = new Dictionary<int, CharacterLevel> ();
	 public Dictionary<int, CharacterSkill> CharacterSkillDict = new Dictionary<int, CharacterSkill> ();
	 public Dictionary<int, Ability> AbilityDict = new Dictionary<int, Ability> ();
	 public Dictionary<int, Combination> CombinationDict = new Dictionary<int, Combination> ();
	 public Dictionary<int, Trophy> TrophyDict = new Dictionary<int, Trophy> ();
	 public Dictionary<int, Currency> CurrencyDict = new Dictionary<int, Currency> ();
	 public Dictionary<int, Material> MaterialDict = new Dictionary<int, Material> ();
	 public Dictionary<int, Equipment> EquipmentDict = new Dictionary<int, Equipment> ();
	 public Dictionary<int, EquipmentGrade> EquipmentGradeDict = new Dictionary<int, EquipmentGrade> ();
	 public Dictionary<int, EquipmentAbility> EquipmentAbilityDict = new Dictionary<int, EquipmentAbility> ();
	 public Dictionary<int, EquipmentAbilityGrade> EquipmentAbilityGradeDict = new Dictionary<int, EquipmentAbilityGrade> ();
	 public Dictionary<int, BattleStage> BattleStageDict = new Dictionary<int, BattleStage> ();
	 public Dictionary<int, Particle> ParticleDict = new Dictionary<int, Particle> ();
	 public Dictionary<int, BattleState> BattleStateDict = new Dictionary<int, BattleState> ();
	 public Dictionary<int, AdventureField> AdventureFieldDict = new Dictionary<int, AdventureField> ();
	 public Dictionary<int, AdventureEquipment> AdventureEquipmentDict = new Dictionary<int, AdventureEquipment> ();
	 public Dictionary<int, AdventureEquipmentProb> AdventureEquipmentProbDict = new Dictionary<int, AdventureEquipmentProb> ();
	 public Dictionary<int, Base> BaseDict = new Dictionary<int, Base> ();
	 public Dictionary<int, Outpost> OutpostDict = new Dictionary<int, Outpost> ();
	 public Dictionary<int, OutpostExtend> OutpostExtendDict = new Dictionary<int, OutpostExtend> ();
	 public Dictionary<int, OfficeSkill> OfficeSkillDict = new Dictionary<int, OfficeSkill> ();
	 public Dictionary<int, Region> RegionDict = new Dictionary<int, Region> ();
	 

    public async UniTask LoadTableDatas ()
    {
        PlayerLevelDict = (await ReadCSVData.Instance.LoadCSVData<PlayerLevel> ("TableData", nameof (PlayerLevel))).ToDictionary (x => x.Id, x => x);
		PlayableCharacterDict = (await ReadCSVData.Instance.LoadCSVData<PlayableCharacter> ("TableData", nameof (PlayableCharacter))).ToDictionary (x => x.Id, x => x);
		EnemyGradeDict = (await ReadCSVData.Instance.LoadCSVData<EnemyGrade> ("TableData", nameof (EnemyGrade))).ToDictionary (x => x.Id, x => x);
		CharacterLevelDict = (await ReadCSVData.Instance.LoadCSVData<CharacterLevel> ("TableData", nameof (CharacterLevel))).ToDictionary (x => x.Id, x => x);
		CharacterSkillDict = (await ReadCSVData.Instance.LoadCSVData<CharacterSkill> ("TableData", nameof (CharacterSkill))).ToDictionary (x => x.Id, x => x);
		AbilityDict = (await ReadCSVData.Instance.LoadCSVData<Ability> ("TableData", nameof (Ability))).ToDictionary (x => x.Id, x => x);
		CombinationDict = (await ReadCSVData.Instance.LoadCSVData<Combination> ("TableData", nameof (Combination))).ToDictionary (x => x.Id, x => x);
		TrophyDict = (await ReadCSVData.Instance.LoadCSVData<Trophy> ("TableData", nameof (Trophy))).ToDictionary (x => x.Id, x => x);
		CurrencyDict = (await ReadCSVData.Instance.LoadCSVData<Currency> ("TableData", nameof (Currency))).ToDictionary (x => x.Id, x => x);
		MaterialDict = (await ReadCSVData.Instance.LoadCSVData<Material> ("TableData", nameof (Material))).ToDictionary (x => x.Id, x => x);
		EquipmentDict = (await ReadCSVData.Instance.LoadCSVData<Equipment> ("TableData", nameof (Equipment))).ToDictionary (x => x.Id, x => x);
		EquipmentGradeDict = (await ReadCSVData.Instance.LoadCSVData<EquipmentGrade> ("TableData", nameof (EquipmentGrade))).ToDictionary (x => x.Id, x => x);
		EquipmentAbilityDict = (await ReadCSVData.Instance.LoadCSVData<EquipmentAbility> ("TableData", nameof (EquipmentAbility))).ToDictionary (x => x.Id, x => x);
		EquipmentAbilityGradeDict = (await ReadCSVData.Instance.LoadCSVData<EquipmentAbilityGrade> ("TableData", nameof (EquipmentAbilityGrade))).ToDictionary (x => x.Id, x => x);
		BattleStageDict = (await ReadCSVData.Instance.LoadCSVData<BattleStage> ("TableData", nameof (BattleStage))).ToDictionary (x => x.Id, x => x);
		ParticleDict = (await ReadCSVData.Instance.LoadCSVData<Particle> ("TableData", nameof (Particle))).ToDictionary (x => x.Id, x => x);
		BattleStateDict = (await ReadCSVData.Instance.LoadCSVData<BattleState> ("TableData", nameof (BattleState))).ToDictionary (x => x.Id, x => x);
		AdventureFieldDict = (await ReadCSVData.Instance.LoadCSVData<AdventureField> ("TableData", nameof (AdventureField))).ToDictionary (x => x.Id, x => x);
		AdventureEquipmentDict = (await ReadCSVData.Instance.LoadCSVData<AdventureEquipment> ("TableData", nameof (AdventureEquipment))).ToDictionary (x => x.Id, x => x);
		AdventureEquipmentProbDict = (await ReadCSVData.Instance.LoadCSVData<AdventureEquipmentProb> ("TableData", nameof (AdventureEquipmentProb))).ToDictionary (x => x.Id, x => x);
		BaseDict = (await ReadCSVData.Instance.LoadCSVData<Base> ("TableData", nameof (Base))).ToDictionary (x => x.Id, x => x);
		OutpostDict = (await ReadCSVData.Instance.LoadCSVData<Outpost> ("TableData", nameof (Outpost))).ToDictionary (x => x.Id, x => x);
		OutpostExtendDict = (await ReadCSVData.Instance.LoadCSVData<OutpostExtend> ("TableData", nameof (OutpostExtend))).ToDictionary (x => x.Id, x => x);
		OfficeSkillDict = (await ReadCSVData.Instance.LoadCSVData<OfficeSkill> ("TableData", nameof (OfficeSkill))).ToDictionary (x => x.Id, x => x);
		RegionDict = (await ReadCSVData.Instance.LoadCSVData<Region> ("TableData", nameof (Region))).ToDictionary (x => x.Id, x => x);
		

        TotalDataDict.AddRange (PlayerLevelDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (PlayableCharacterDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EnemyGradeDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CharacterLevelDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CharacterSkillDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AbilityDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CombinationDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (TrophyDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CurrencyDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (MaterialDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentGradeDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentAbilityDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentAbilityGradeDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (BattleStageDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (ParticleDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (BattleStateDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AdventureFieldDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AdventureEquipmentDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AdventureEquipmentProbDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (BaseDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (OutpostDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (OutpostExtendDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (OfficeSkillDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (RegionDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		
    }
}