using System.Collections.Generic;
using System.Threading;
using KKSFramework.ResourcesLoad;
using Cysharp.Threading.Tasks;
using MasterData;
using UnityEngine;

namespace AutoChess
{
    public class BattleViewParticleManagingModule : MonoBehaviour
    {
        #region Fields & Property
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly List<ParticlePrefab> _particlePrefabs = new List<ParticlePrefab> ();
        
        private CancellationTokenSource _cancellationTokenSource;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _cancellationTokenSource = new CancellationTokenSource ();
        }

        #endregion


        #region Methods
        
        private void ResetModule ()
        {
            _cancellationTokenSource.Cancel();
            _particlePrefabs.ForEach (particle =>
            {
                particle.Despawn ();
            });
        }
        

        public async UniTask GenerateParticlePrefab (int particleDataIndex, Transform parents)
        {
            if (!Particle.Manager.TryGetValue (particleDataIndex, out var particleData)) return;
            
            Debug.Log ($"Generate Particle : {particleDataIndex}");
            var particlePrefab = ObjectPoolingHelper.GetResources<ParticlePrefab> (ResourceRoleType.Bundles, ResourcesType.Particle,
                particleData.PrefabName, parents);
            await particlePrefab.PresetParticlePrefab (particleData, _cancellationTokenSource.Token);
            _particlePrefabs.Remove (particlePrefab);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}