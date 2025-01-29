using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace SO
{
    [CreateAssetMenu]
    public class SceneSounds : ScriptableObject
    {
        [SerializeField] private List<AudioClip> _audioClips;

        private Dictionary<string, AudioClip> AudioClips;

        public AudioClip GetAudioClipByName(string clipName)
        {
            return AudioClips.TryGetValue(clipName,out var clip) ? clip : null;
        }

        public void SetAudioClip()
        {
            AudioClips = new Dictionary<string, AudioClip>();
            
            for (int i = 0; i < _audioClips.Count; i++)
            {
                AudioClips.Add(Enum.GetValues(typeof(AudioNames)).GetValue(i).ToString(), _audioClips[(int)Enum.GetValues(typeof(AudioNames)).GetValue(i)]);
            }
        }
    }
}
