using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helper
{
    public static class ConfigHelper
    {
        private const string SFX_VOLUME_KEY = "SfxVolume";
        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string VIBRATION_KEY = "Vibration";
        private const string LEFT_HAND_KEY = "LeftHand";
        private const string BEST_SCORE_KEY = "BestScore";
        private const string PLAY_COUNT_KEY = "PlayCount";
        
        
        public static float SfxVolume
        {
            get => PlayerPrefs.GetFloat (SFX_VOLUME_KEY, 1f);
            set => PlayerPrefs.SetFloat (SFX_VOLUME_KEY, value);
        }

        public static float MusicVolume
        {
            get => PlayerPrefs.GetFloat (MUSIC_VOLUME_KEY, 1f);
            set => PlayerPrefs.SetFloat (MUSIC_VOLUME_KEY, value);
        }

        public static bool Vibration
        {
            get => PlayerPrefs.GetInt (VIBRATION_KEY, 1) == 1;
            set => PlayerPrefs.SetInt (VIBRATION_KEY, value ? 1 : 0);
        }

        public static bool LeftHand
        {
            get => PlayerPrefs.GetInt (LEFT_HAND_KEY, 0) == 1;
            set => PlayerPrefs.SetInt (LEFT_HAND_KEY, value ? 1 : 0);
        }

        public static int BestScore
        {
            get => PlayerPrefs.GetInt (BEST_SCORE_KEY, 0);
            set => PlayerPrefs.SetInt (BEST_SCORE_KEY, value);
        }
        
        public static int PlayCount
        {
            get => PlayerPrefs.GetInt (PLAY_COUNT_KEY, 0);
            set => PlayerPrefs.SetInt (PLAY_COUNT_KEY, value);
        }

        public static void IncreasePlayCount()
        {
            PlayCount = Mathf.Min (PlayCount + 1, int.MaxValue - 1);
        }

        public static void Save ()
        {
            PlayerPrefs.Save ();
        }
    }
}