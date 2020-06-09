// ExcelExporter로 자동 생성된 파일.

using System.Collections.Generic;
using System.Linq;
using KKSFramework.TableData;
using UniRx.Async;

public class TableDataManager : Singleton<TableDataManager>
{
	public Dictionary<string, GlobalText> GlobalTextDict = new Dictionary<string, GlobalText> ();
	public Dictionary<int, SpecialPuzzle> SpecialPuzzleDict = new Dictionary<int, SpecialPuzzle> ();
	public Dictionary<int, PuzzleLevel> PuzzleLevelDict = new Dictionary<int, PuzzleLevel> ();
	public Dictionary<int, CharacterProbability> CharacterProbabilityDict = new Dictionary<int, CharacterProbability> ();
	public Dictionary<int, ProbabilityRange> ProbabilityRangeDict = new Dictionary<int, ProbabilityRange> ();
	public Dictionary<int, Character> CharacterDict = new Dictionary<int, Character> ();
	public Dictionary<int, CharacterLevel> CharacterLevelDict = new Dictionary<int, CharacterLevel> ();


    public async UniTask LoadTableDatas ()
    {
		GlobalTextDict = (await ReadCSVData.Instance.LoadCSVData<GlobalText> (nameof (GlobalText))).ToDictionary (x => x.Id, x => x);
		SpecialPuzzleDict = (await ReadCSVData.Instance.LoadCSVData<SpecialPuzzle> (nameof (SpecialPuzzle))).ToDictionary (x => x.Id, x => x);
		PuzzleLevelDict = (await ReadCSVData.Instance.LoadCSVData<PuzzleLevel> (nameof (PuzzleLevel))).ToDictionary (x => x.Id, x => x);
		CharacterProbabilityDict = (await ReadCSVData.Instance.LoadCSVData<CharacterProbability> (nameof (CharacterProbability))).ToDictionary (x => x.Id, x => x);
		ProbabilityRangeDict = (await ReadCSVData.Instance.LoadCSVData<ProbabilityRange> (nameof (ProbabilityRange))).ToDictionary (x => x.Id, x => x);
		CharacterDict = (await ReadCSVData.Instance.LoadCSVData<Character> (nameof (Character))).ToDictionary (x => x.Id, x => x);
		CharacterLevelDict = (await ReadCSVData.Instance.LoadCSVData<CharacterLevel> (nameof (CharacterLevel))).ToDictionary (x => x.Id, x => x);

    }
}
