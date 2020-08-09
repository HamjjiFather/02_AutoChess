using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public enum CharacterBuiltInParticleType
    {
        Death = 0,
        Hit,
        CriticalHit
    }
    
    public class CharacterParticleModule : MonoBehaviour
    {
        #region Fields & Property

        public ParticleSystem[] characterParticles;

#pragma warning disable CS0649

        [Inject]
        private BattleViewParticleManagingModule _battleViewParticleManagingModule;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void PlayParticle (CharacterBuiltInParticleType builtInParticleType)
        {
            Debug.Log ($"Play Particle : {builtInParticleType}");
            characterParticles[(int)builtInParticleType].Play();
        }

        public void GenerateParticle (int particleDataIndex)
        {
            _battleViewParticleManagingModule.GenerateParticlePrefab (particleDataIndex, transform).Forget();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}