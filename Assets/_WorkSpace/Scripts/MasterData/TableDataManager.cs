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
	 public Dictionary<int, Character> CharacterDict = new Dictionary<int, Character> ();
	 public Dictionary<int, CharacterLevel> CharacterLevelDict = new Dictionary<int, CharacterLevel> ();
	 public Dictionary<int, CharacterSkill> CharacterSkillDict = new Dictionary<int, CharacterSkill> ();
	 public Dictionary<int, Ability> AbilityDict = new Dictionary<int, Ability> ();
	 public Dictionary<int, AbilityGradeRangeProb> AbilityGradeRangeProbDict = new Dictionary<int, AbilityGradeRangeProb> ();
	 public Dictionary<int, Combination> CombinationDict = new Dictionary<int, Combination> ();
	 public Dictionary<int, Equipment> EquipmentDict = new Dictionary<int, Equipment> ();
	 public Dictionary<int, EquipmentAbility> EquipmentAbilityDict = new Dictionary<int, EquipmentAbility> ();
	 public Dictionary<int, EquipmentGradeProb> EquipmentGradeProbDict = new Dictionary<int, EquipmentGradeProb> ();
	 public Dictionary<int, BattleStage> BattleStageDict = new Dictionary<int, BattleStage> ();
	 public Dictionary<int, Particle> ParticleDict = new Dictionary<int, Particle> ();
	 public Dictionary<int, BattleState> BattleStateDict = new Dictionary<int, BattleState> ();
	 public Dictionary<int, Currency> CurrencyDict = new Dictionary<int, Currency> ();
	 public Dictionary<int, AdventureField> AdventureFieldDict = new Dictionary<int, AdventureField> ();
	 public Dictionary<int, AdventureEquipment> AdventureEquipmentDict = new Dictionary<int, AdventureEquipment> ();
	 public Dictionary<int, AdventureEquipmentProb> AdventureEquipmentProbDict = new Dictionary<int, AdventureEquipmentProb> ();
	 

    public async UniTask LoadTableDatas ()
    {
        PlayerLevelDict = (await ReadCSVData.Instance.LoadCSVData<PlayerLevel> ("TableData", nameof (PlayerLevel))).ToDictionary (x => x.Id, x => x);
		CharacterDict = (await ReadCSVData.Instance.LoadCSVData<Character> ("TableData", nameof (Character))).ToDictionary (x => x.Id, x => x);
		CharacterLevelDict = (await ReadCSVData.Instance.LoadCSVData<CharacterLevel> ("TableData", nameof (CharacterLevel))).ToDictionary (x => x.Id, x => x);
		CharacterSkillDict = (await ReadCSVData.Instance.LoadCSVData<CharacterSkill> ("TableData", nameof (CharacterSkill))).ToDictionary (x => x.Id, x => x);
		AbilityDict = (await ReadCSVData.Instance.LoadCSVData<Ability> ("TableData", nameof (Ability))).ToDictionary (x => x.Id, x => x);
		AbilityGradeRangeProbDict = (await ReadCSVData.Instance.LoadCSVData<AbilityGradeRangeProb> ("TableData", nameof (AbilityGradeRangeProb))).ToDictionary (x => x.Id, x => x);
		CombinationDict = (await ReadCSVData.Instance.LoadCSVData<Combination> ("TableData", nameof (Combination))).ToDictionary (x => x.Id, x => x);
		EquipmentDict = (await ReadCSVData.Instance.LoadCSVData<Equipment> ("TableData", nameof (Equipment))).ToDictionary (x => x.Id, x => x);
		EquipmentAbilityDict = (await ReadCSVData.Instance.LoadCSVData<EquipmentAbility> ("TableData", nameof (EquipmentAbility))).ToDictionary (x => x.Id, x => x);
		EquipmentGradeProbDict = (await ReadCSVData.Instance.LoadCSVData<EquipmentGradeProb> ("TableData", nameof (EquipmentGradeProb))).ToDictionary (x => x.Id, x => x);
		BattleStageDict = (await ReadCSVData.Instance.LoadCSVData<BattleStage> ("TableData", nameof (BattleStage))).ToDictionary (x => x.Id, x => x);
		ParticleDict = (await ReadCSVData.Instance.LoadCSVData<Particle> ("TableData", nameof (Particle))).ToDictionary (x => x.Id, x => x);
		BattleStateDict = (await ReadCSVData.Instance.LoadCSVData<BattleState> ("TableData", nameof (BattleState))).ToDictionary (x => x.Id, x => x);
		CurrencyDict = (await ReadCSVData.Instance.LoadCSVData<Currency> ("TableData", nameof (Currency))).ToDictionary (x => x.Id, x => x);
		AdventureFieldDict = (await ReadCSVData.Instance.LoadCSVData<AdventureField> ("TableData", nameof (AdventureField))).ToDictionary (x => x.Id, x => x);
		AdventureEquipmentDict = (await ReadCSVData.Instance.LoadCSVData<AdventureEquipment> ("TableData", nameof (AdventureEquipment))).ToDictionary (x => x.Id, x => x);
		AdventureEquipmentProbDict = (await ReadCSVData.Instance.LoadCSVData<AdventureEquipmentProb> ("TableData", nameof (AdventureEquipmentProb))).ToDictionary (x => x.Id, x => x);
		

        TotalDataDict.AddRange (PlayerLevelDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CharacterDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CharacterLevelDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CharacterSkillDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AbilityDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AbilityGradeRangeProbDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CombinationDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentAbilityDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (EquipmentGradeProbDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (BattleStageDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (ParticleDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (BattleStateDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (CurrencyDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AdventureFieldDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AdventureEquipmentDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		TotalDataDict.AddRange (AdventureEquipmentProbDict.ToDictionary (x => x.Key, k => (TableDataBase) k.Value));
		
    }
}