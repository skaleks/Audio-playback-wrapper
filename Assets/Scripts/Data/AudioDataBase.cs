using System.Collections.Generic;
using UnityEngine;

namespace Data
{
     
    [CreateAssetMenu(menuName = "AudioDataBase", fileName = nameof(AudioDataBase))]
    public class AudioDataBase : ScriptableObject // Odin and its SerializedScriptableObject class were used to serialize dictionaries.
    {
        [SerializeField] private Dictionary<string, IAudioData<AudioEvent>> _audioData;

        public IAudioData<AudioEvent> GetAudioData(string name)
        {
            return _audioData.ContainsKey(name) ? _audioData[name] : null;
        }

        public Dictionary<string, IAudioData<AudioEvent>> GetAllAudio()
        {
            return _audioData;
        }
    }
}