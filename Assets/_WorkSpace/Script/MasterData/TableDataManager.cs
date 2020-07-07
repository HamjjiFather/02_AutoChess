// ExcelExporter로 자동 생성된 파일.

using System.Collections.Generic;
using System.Linq;
using KKSFramework.TableData;
using UniRx.Async;

public class TableDataManager : Singleton<TableDataManager>
{
	public Dictionary<string, GlobalText> GlobalTextDict = new Dictionary<string, GlobalText> ();
	public Dictionary<int, Character> CharacterDict = new Dictionary<int, Character> ();
	public Dictionary<int, CharacterLevel> CharacterLevelDict = new Dictionary<int, CharacterLevel> ();
	public Dictionary<int, Skill> SkillDict = new Dictionary<int, Skill> ();
	public Dictionary<int, Equipment> EquipmentDict = new Dictionary<int, Equipment> ();
	public Dictionary<int, Status> StatusDict = new Dictionary<int, Status> ();
	public Dictionary<int, StatusGradeRange> StatusGradeRangeDict = new Dictionary<int, StatusGradeRange> ();
	public Dictionary<int, Stage> StageDict = new Dictionary<int, Stage> ();
	public Dictionary<int, Particle> ParticleDict = new Dictionary<int, Particle> ();
	public Dictionary<int, Combination> CombinationDict = new Dictionary<int, Combination> ();
	public Dictionary<int, BattleState> BattleStateDict = new Dictionary<int, BattleState> ();


    public async UniTask LoadTableDatas ()
    {
		GlobalTextDict = (await ReadCSVData.Instance.LoadCSVData<GlobalText> (nameof (GlobalText))).ToDictionary (x => x.Id, x => x);
		CharacterDict = (await ReadCSVData.Instance.LoadCSVData<Character> (nameof (Character))).ToDictionary (x => x.Id, x => x);
		CharacterLevelDict = (await ReadCSVData.Instance.LoadCSVData<CharacterLevel> (nameof (CharacterLevel))).ToDictionary (x => x.Id, x => x);
		SkillDict = (await ReadCSVData.Instance.LoadCSVData<Skill> (nameof (Skill))).ToDictionary (x => x.Id, x => x);
		EquipmentDict = (await ReadCSVData.Instance.LoadCSVData<Equipment> (nameof (Equipment))).ToDictionary (x => x.Id, x => x);
		StatusDict = (await ReadCSVData.Instance.LoadCSVData<Status> (nameof (Status))).ToDictionary (x => x.Id, x => x);
		StatusGradeRangeDict = (await ReadCSVData.Instance.LoadCSVData<StatusGradeRange> (nameof (StatusGradeRange))).ToDictionary (x => x.Id, x => x);
		StageDict = (await ReadCSVData.Instance.LoadCSVData<Stage> (nameof (Stage))).ToDictionary (x => x.Id, x => x);
		ParticleDict = (await ReadCSVData.Instance.LoadCSVData<Particle> (nameof (Particle))).ToDictionary (x => x.Id, x => x);
		CombinationDict = (await ReadCSVData.Instance.LoadCSVData<Combination> (nameof (Combination))).ToDictionary (x => x.Id, x => x);
		BattleStateDict = (await ReadCSVData.Instance.LoadCSVData<BattleState> (nameof (BattleState))).ToDictionary (x => x.Id, x => x);

    }
}
