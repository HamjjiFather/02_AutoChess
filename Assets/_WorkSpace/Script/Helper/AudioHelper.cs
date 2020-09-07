using DarkTonic.MasterAudio;

namespace Helper
{
    public enum Bus
    {
        bgm,
        fx
    }


    public enum Bgm
    {
        None,
        bgm_ingame,        // 인게임을 제외한 페이지에서 선택중인 MC에 따라 1~4를 재생
        bgm_menu,
        bgm_title,
    }


    public enum Fx
    {
        None,
        bounce,
        click,
        result,
        round,
        score,
        splat,
        @throw,
        impact,
        last_fork_open,
        last_fork_shining,
        double_fork_open,
        double_fork_shining,
        times_up,
        tictoc,
        new_record
    }


    public static class AudioHelper
    {
        /// <summary>
        /// 사운드 활성화 유무
        /// </summary>
        private static bool _isActive = true;

        public static bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;

                if (_isActive)
                {
                    Play (Fx.click);
                }
                else
                {
                    MasterAudio.StopEverything ();
                }
            }
        }


        /// <summary>
        /// 배경 사운드 활성화 유무
        /// </summary>
        private static bool _isBgmActive = true;

        public static bool IsBgmActive
        {
            get => _isBgmActive;
            set
            {
                _isBgmActive = value;

                if (_isBgmActive)
                {
                    Play (_lastPlayedBgm);
                }
                else
                {
                    MasterAudio.StopBus (Bus.bgm.ToString ());
                }
            }
        }


        /// <summary>
        /// 효과 사운드 활성화 유무
        /// </summary>
        private static bool _isFxActive = true;

        public static bool IsFxActive
        {
            get => _isFxActive;
            set
            {
                _isFxActive = value;

                if (_isFxActive)
                {
                    Play (Fx.click);
                }
                else
                {
                    MasterAudio.StopBus (Bus.fx.ToString ());
                }
            }
        }


        /// <summary>
        /// 마지막으로 재생된 Bgm 이름
        /// </summary>
        private static Bgm _lastPlayedBgm = Bgm.None;

        public static void SetLastPlayedBgm (Bgm bgm) { _lastPlayedBgm = bgm; }
        
        
        #region Play

        public static void Play (Bgm bgm)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsBgmActive || bgm == Bgm.None ||
                _lastPlayedBgm == bgm)
            {
                return;
            }

            if (MasterAudio.PlaySound (bgm.ToString ()) != null)
            {
                _lastPlayedBgm = bgm;
            }
        }


        public static void Play (Fx fx) { PlayToStringName (fx.ToString ()); }


        /// <summary>
        /// 사운드 리소스 이름을 조합하여 재생해야 할때 사용. bgm 재생에 사용하지 말것.
        /// </summary>
        public static void PlayToStringName (string resourceName)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsFxActive)
            {
                return;
            }
            
            MasterAudio.PlaySound (resourceName);
        }
        
        #endregion


        #region Stop

        public static void Stop (Bgm bgm)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsBgmActive)
            {
                return;
            }

            MasterAudio.StopAllOfSound (bgm.ToString ());
        }


        public static void Stop (Fx fx) { StopToStringCombine (fx.ToString ()); }

        
        /// <summary>
        /// 사운드 리소스 이름을 조합하여 정지해야 할때 사용. bgm 정지에 사용하지 말것.
        /// </summary>
        public static void StopToStringCombine (string resourceName)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsFxActive)
            {
                return;
            }
            
            MasterAudio.StopAllOfSound (resourceName);
        }
        
        #endregion


        #region Mute

        public static void MuteAll ()
        {
            if (MasterAudio.SafeInstance == null || !IsActive)
            {
                return;
            }

            if (IsBgmActive)
            {
                MuteBus (Bus.bgm);
            }

            if (IsFxActive)
            {
                MuteBus (Bus.fx);
            }
        }


        public static void MuteBus (Bus bus)
        {
            if (MasterAudio.SafeInstance == null || !IsActive)
            {
                return;
            }
            
            MasterAudio.MuteBus (bus.ToString());
        }


        public static void Mute (Bgm bgm)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsBgmActive)
            {
                return;
            }

            MasterAudio.MuteGroup (bgm.ToString ());
        }


        public static void Mute (Fx fx)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsFxActive)
            {
                return;
            }

            MasterAudio.MuteGroup (fx.ToString ());
        }
        
        #endregion


        public static bool IsPlayingSound (string sound)
        {
            return MasterAudio.IsSoundGroupPlaying (sound);
        }


        #region StopBus

        public static void StopBusAll ()
        {
            if (MasterAudio.SafeInstance == null || !IsActive)
            {
                return;
            }
            
            MasterAudio.StopBus (Bus.bgm.ToString ());
            MasterAudio.StopBus (Bus.fx.ToString ());
            _lastPlayedBgm = Bgm.None;
        }


        public static void StopBus (Bus bus)
        {
            if (MasterAudio.SafeInstance == null || !IsActive)
            {
                return;
            }

            if (bus == Bus.bgm)
            {
                _lastPlayedBgm = Bgm.None;
            }
            MasterAudio.StopBus (bus.ToString ());
        }
        
        #endregion


        #region Unmute

        public static void UnmuteAll ()
        {
            if (MasterAudio.SafeInstance == null || !IsActive)
            {
                return;
            }

            if (IsBgmActive)
            {
                UnmuteBus (Bus.bgm);
            }

            if (IsFxActive)
            {
                UnmuteBus (Bus.fx);
            }
        }

        public static void UnmuteBus (Bus bus)
        {
            if (MasterAudio.SafeInstance == null || !IsActive)
            {
                return;
            }
            
            MasterAudio.UnmuteBus (bus.ToString());
        }

        public static void Unmute (Bgm bgm)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsBgmActive)
            {
                return;
            }

            MasterAudio.UnmuteGroup (bgm.ToString ());
        }


        public static void Unmute (Fx fx)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsFxActive)
            {
                return;
            }

            MasterAudio.UnmuteGroup (fx.ToString ());
        }

        #endregion


        public static void SetBgmVolume (float volume) { MasterAudio.SetBusVolumeByName (Bus.bgm.ToString (), volume); }


        public static void SetFxVolume (float volume)
        {
            if (MasterAudio.SafeInstance == null || !IsActive || !IsFxActive)
            {
                return;
            }
            
            MasterAudio.SetBusVolumeByName (Bus.fx.ToString (), volume);
        }
    }
}