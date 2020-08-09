using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AutoChess
{
    public class ParticlePrefab : PooingComponent
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public async UniTask PresetParticlePrefab (Particle particleData, CancellationToken cancellationToken)
        {
            var xRange = Random.Range (-particleData.BoundArea, particleData.BoundArea);
            var yRange = Random.Range (-particleData.BoundArea, particleData.BoundArea);
            transform.localPosition = new Vector3 (xRange, yRange, 0);
            transform.localScale = new Vector3 (particleData.ParticleSize, particleData.ParticleSize, 1);

            var particleSys = GetComponent<ParticleSystem> ();
            await UniTask.Delay (TimeSpan.FromSeconds (particleSys.main.startLifetime.constantMax),
                cancellationToken: cancellationToken);
            
            PoolingObject ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}