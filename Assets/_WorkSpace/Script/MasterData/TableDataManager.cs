// ExcelExporter로 자동 생성된 파일.

using System.Collections.Generic;
using System.Linq;
using KKSFramework.TableData;
using UniRx.Async;

public class TableDataManager : Singleton<TableDataManager>
{
	public Dictionary<string, GlobalText> GlobalTextDict = new Dictionary<string, GlobalText> ();
	public Dictionary<int, SpecialPuzzle> SpecialPuzzleDict = new Dictionary<int, SpecialPuzzle> ();


    public async UniTask LoadTableDatas ()
    {
		GlobalTextDict = (await ReadCSVData.Instance.LoadCSVData<GlobalText> (nameof (GlobalText))).ToDictionary (x => x.Id, x => x);
		SpecialPuzzleDict = (await ReadCSVData.Instance.LoadCSVData<SpecialPuzzle> (nameof (SpecialPuzzle))).ToDictionary (x => x.Id, x => x);

    }
}
