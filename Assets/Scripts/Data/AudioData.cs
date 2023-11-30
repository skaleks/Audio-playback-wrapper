using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "AudioData")]
    public class AudioData : ScriptableObject, IAudioData<AudioEvent>
    {
        [SerializeField] private AudioEvent _audioEvent;
        
        public AudioEvent GetAudio()
        {
            return _audioEvent;
        }
    }
}