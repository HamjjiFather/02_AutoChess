using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Helper;
using UnityEngine;

namespace KKSFramework.TableData
{
    /// <summary>
    /// CSV데이터를 받아와 리턴.
    /// </summary>
    public class ReadCSVData : Singleton<ReadCSVData>
    {
        /// <summary>
        /// CSV 데이터 리턴.
        /// 데이터 클래스를 List<object> 형식으로 받아올 수 있게 구현해주어야 함.
        /// </summary>
        /// <returns></returns>
        public async UniTask<List<T>> LoadCSVData<T> (string sccondsPath, string path) where T : TableDataBase, new ()
        {
            var csvDataBases = new List<T> ();
            var res = await ResourcesLoadHelper.LoadResourcesAsync<TextAsset> ($"_Data/Tsv/{sccondsPath}/{path}");
            var csvEnum = res.text.Split ('\n').GetEnumerator ();

            csvEnum.MoveNext ();

            while (csvEnum.MoveNext ())
            {
                var datas = csvEnum.Current?.ToString ();
                if (string.IsNullOrEmpty (datas)) continue;
                var tempListValues = datas.Split ('\t').Select (x => x.Replace ("\r", "")).ToList ();
                var dataBase = new T ();
                dataBase.SetData (tempListValues);
                csvDataBases.Add (dataBase);
            }

            await UniTask.WaitWhile (() => csvEnum.MoveNext ());
            return csvDataBases;
        }
    }
}