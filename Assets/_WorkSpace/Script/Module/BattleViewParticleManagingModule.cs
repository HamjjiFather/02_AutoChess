using System.Collections.Generic;
using System.Threading;
using KKSFramework.ResourcesLoad;
using UniRx.Async;
using UnityEngine;
using Zenject;

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
            _particlePrefabs.Foreach (particle =>
            {
                particle.PoolingObject ();
            });
        }
        

        public async UniTask GenerateParticlePrefab (int particleDataIndex, Transform parents)
        {
            if (!TableDataManager.Instance.ParticleDict.TryGetValue (particleDataIndex, out var particleData)) return;
            
            Debug.Log ($"Generate Particle : {particleDataIndex}");
            var particlePrefab = ObjectPoolingHelper.GetResources<ParticlePrefab> (ResourceRoleType._Prefab, ResourcesType.Particle,
                particleData.PrefabName, parents);
            await particlePrefab.PresetParticlePrefab (particleData, _cancellationTokenSource.Token);
            _particlePrefabs.Remove (particlePrefab);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}