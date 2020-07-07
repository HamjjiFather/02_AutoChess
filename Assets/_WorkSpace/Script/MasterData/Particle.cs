// ExcelExporter로 자동 생성된 파일.

using System;
using System.Collections.Generic;
using KKSFramework.TableData;

public class Particle : TableDataBase
{
    /// <summary>
    /// 인덱스
    /// </summary>
    public int Id;

    /// <summary>
    /// 프리팹 이름
    /// </summary>
    public string PrefabName;

    /// <summary>
    /// 파티클 영역
    /// </summary>
    public int BoundArea;

    /// <summary>
    /// 파티클 크기
    /// </summary>
    public float ParticleSize;



    public Particle ()
    {
    }


    public override void SetData (List<string> myData)
    {
        var rowIndex = 0;
		Id = int.Parse(myData[rowIndex++]);
		PrefabName = myData[rowIndex++];
		BoundArea = int.Parse(myData[rowIndex++]);
		ParticleSize = float.Parse(myData[rowIndex++]);

    }
}
