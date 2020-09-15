using System;
 using System.Collections.Generic;
 using System.Linq;
 using UnityEngine;
 using Cysharp.Threading.Tasks;
 using Helper;

    public class BaseManager<TC> : Dictionary<int, TC>
    {
        protected string[] LoadData (string fullPath)
        {
            var textAsset = Resources.Load (fullPath) as TextAsset;

            if (textAsset == null)
            {
                Debug.LogWarning ($"[{nameof(BaseManager<TC>)}] MasterData의 Data 파일이 존재하지 않는다. : {fullPath}");
                return new string[0];
            }

            // 윈도우에서 로드시 행의 마지막에 \r이 자동으로 붙는다. 제거해야 한다.
            return textAsset.text.Replace ("\r", "").Split ('\n');    
        }
        
        
        protected async UniTask<string[]> LoadDataAsync (string fullPath)
        {
            var textAsset = await ResourcesLoadHelper.LoadResourcesAsync<TextAsset> (fullPath);
            
            if (textAsset == null)
            {
                Debug.LogWarning ($"[{nameof(BaseManager<TC>)}] MasterData의 Data 파일이 존재하지 않는다. : {fullPath}");
                return new string[0];
            }

            // 윈도우에서 로드시 행의 마지막에 \r이 자동으로 붙는다. 제거해야 한다.
            return textAsset.text.Replace ("\r", "").Split ('\n');    
        }


        protected string[] ParsingStringList (string value)
        {
            return value.Split ('|').ToArray ();
        }


        protected T[] ParsingList<T> (string value, Func<string, T> parser)
        {
            var values = value.Split ('|');
            var result = new T[values.Length];
            
            if (typeof (bool) == typeof (T))
            {
                for (var i = 0; i < values.Length; i++)
                {
                    result[i] = parser (values[i].Equals ("1") ? "true" : "false");
                }
            }
            else
            {
                for (var i = 0; i < values.Length; i++)
                {
                    result[i] = parser (values[i]);
                }
            }

            return result;
        }


        protected T Parsing<T> (string value, Func<string, T> parser)
        {
            if (typeof(bool) == typeof(T))
            {
                value = value.Equals ("1") ? "true" : "false";
            }

            return parser (value);
        }


        protected T ParsingEnum<T> (string value)
        {
            return (T) Enum.Parse (typeof(T), value);
        }


        protected T[] ParsingEnumList<T> (string value)
        {
            return value.Split ('|').Select (x => (T) Enum.Parse (typeof(T), x)).ToArray ();
        }


        protected DateTime? ParsingDateTime (string value)
        {
            if (value.ToLower() == "none" || value.ToLower() == "null" || value.ToLower().Equals("0"))
                return null;

            return DateTime.Parse (value);
        }


        protected void CompletedTableLoad (string msg)
        {
//            BaseFrame.Log.Verbose (nameof(BaseManager<TC>), msg);
        }
    }
