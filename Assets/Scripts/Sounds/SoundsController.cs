using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundsController : MonoBehaviour
    {
        [SerializeField] 
        private List<AudioSource> _audioSources;

        public void TryPlaySound(AudioClip clip)
        {
            if (!SoundsStates.CanPlaySound)
            {
                return;
            }

            int index = 0;

            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (!_audioSources[i].isPlaying)
                {
                    index = i;
                    break;
                }
            }

            _audioSources[index].clip = clip;
            _audioSources[index].Play();
        }
    }
}