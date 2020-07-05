using UnityEngine;

namespace AutoChess
{
    public enum CharacterParticleType
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

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void PlayParticle (CharacterParticleType particleType)
        {
            Debug.Log ($"Play Particle : {particleType}");
            characterParticles[(int)particleType].Play();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}