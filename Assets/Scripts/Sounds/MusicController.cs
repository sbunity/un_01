using UnityEngine;

namespace Sounds
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;

        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }

        public void TryPlayMusic(AudioClip clip)
        {
            if (!SoundsStates.CanPlayMusic)
            {
                StopMusic();
                return;
            }

            if (_musicSource.isPlaying && _musicSource.clip == clip)
            {
                return;
            }
            
            StopMusic();

            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }
    }
}
