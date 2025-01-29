using UnityEngine;

namespace Sounds
{
    public static class SoundsStates
    {
        private const string CanPlaySoundKey = "SoundsStates.CanPlaySound";
        private const string CanPlayMusicKey = "SoundsStates.CanPlayMusic";
        
        public static bool CanPlaySound
        {
            get => !PlayerPrefs.HasKey(CanPlaySoundKey) || PlayerPrefs.GetInt(CanPlaySoundKey) == 0;
            private set => PlayerPrefs.SetInt(CanPlaySoundKey, value ? 0 : 1);
        }
        
        public static bool CanPlayMusic
        {
            get => !PlayerPrefs.HasKey(CanPlayMusicKey) || PlayerPrefs.GetInt(CanPlayMusicKey) == 0;
            private set => PlayerPrefs.SetInt(CanPlayMusicKey, value ? 0 : 1);
        }

        public static void ChangeSoundsState()
        {
            CanPlaySound = !CanPlaySound;
        }
        
        public static void ChangeMusicState()
        {
            CanPlayMusic = !CanPlayMusic;
        }
    }
}