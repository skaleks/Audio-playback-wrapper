using System.Collections.Generic;
using Data;
using UnityEngine;
using Util;
using Zenject;
using Object = UnityEngine.Object;

namespace Service
{
    public class AudioDirector : IInitializable, IAudioDirector
    {
        [Inject] private AudioDataBase _audioDataBase;

        private Dictionary<string, AudioInstance> _instances;
        private GameObject _defaultAudioSource;
        private class AudioInstance
        {
            public AudioEvent AudioEvent;
            public GameObject GameObject;
            public bool IsPlayed;

            public AudioInstance(AudioEvent ev, GameObject gameObject)
            {
                AudioEvent = ev;
                GameObject = gameObject;
                IsPlayed = false;
            }
        }

        public void Initialize()
        {
            ApplySettings();
            _instances = new Dictionary<string, AudioInstance>();
            _defaultAudioSource = new GameObject("Default Audio Source");
            _defaultAudioSource.AddComponent(typeof(DontDestroyOnLoad));
        }

        private void ApplySettings()
        {
            // SetVolume("id", value);
        }

        public void PlayLoop(string name)
        {
            if (!_instances.ContainsKey(name))
            {
                RegisterInstance(name);
            }

            if (_instances[name].IsPlayed)
            {
                return;
            }
            
            _instances[name].AudioEvent.Post(_instances[name].GameObject);
            _instances[name].IsPlayed = true;
        }

        public void PlayLoopOnAttachedObject(string name, GameObject gameObject)
        {
            if (!_instances.ContainsKey(name))
            {
                var data = _audioDataBase.GetAudioData(name);
                var instance = data.GetAudio();
                _instances.Add(name, new AudioInstance(instance, gameObject));
            }

            if (_instances[name].IsPlayed)
            {
                return;
            }
            
            _instances[name].AudioEvent.Post(_instances[name].GameObject);
            _instances[name].IsPlayed = true;
        }

        public uint PlayUniqueLoop(string name, GameObject gameObject)
        {
            var instance = _audioDataBase.GetAudioData(name).GetAudio();
            var id = instance.Post(gameObject);
            return id;
        }

        public void StopLoopOnAttachedObject(string name, STOPMODE stopMode)
        {
            if (!_instances.ContainsKey(name))
            {
                Debug.Log("Это звуковое событие не запущено!");
                return;
            }
            
            var eventInstance = _instances[name].AudioEvent;
            var gameobject = _instances[name].GameObject;
                
            eventInstance.Stop(gameobject, GetStopDuration(stopMode));
            _instances.Remove(name);
        }

        public void StopUniqueLoop(uint id, STOPMODE stopMode = STOPMODE.IMMEDIATELY)
        {
            // Stop unique sound instance 
        }

        public void PlayOnce(string name, Vector3 point = default)
        {
            var instance = _audioDataBase.GetAudioData(name).GetAudio();
            if (point != default)
            {
                _defaultAudioSource.transform.position = point;
            }

            instance.Post(_defaultAudioSource);
        }

        public void PlayOnceOnAttachedObject(string name, GameObject gameObject)
        {
            var instance = _audioDataBase.GetAudioData(name).GetAudio();
            instance.Post(gameObject);
        }

        public void Stop(string name, STOPMODE stopMode)
        {
            if (!_instances.ContainsKey(name))
            {
                Debug.Log("Это звуковое событие не запущено!");
                return;
            }
            
            var eventInstance = _instances[name].AudioEvent;
            var gameobject = _instances[name].GameObject;
                
            eventInstance.Stop(gameobject, GetStopDuration(stopMode));
            UnRegisterInstance(name);
        }

        public void Pause(string name, bool value)
        {
            // pause one audio
        }

        public void PauseAllInstances(bool value)
        {
            // Pause all audio
        }
        
        public void Switch(string switchGroup, string state, GameObject audioSource = null)
        {
            // Switch sound group depending on conditions
        }

        private void SetVolume(string id, float value)
        {
            // Change volume params
        }

        private void RegisterInstance(string name)
        {
            var data = _audioDataBase.GetAudioData(name);
            var instance = data.GetAudio();
            var gameObject = new GameObject($"Audio source {name}");
            gameObject.AddComponent(typeof(DontDestroyOnLoad));
            
            _instances.Add(name, new AudioInstance(instance, gameObject));
        }
        
        private void UnRegisterInstance(string name)
        {
            Object.Destroy(_instances[name].GameObject);
            _instances.Remove(name);
        }

        private int GetStopDuration(STOPMODE stopMode)
        {
            return stopMode switch
            {
                STOPMODE.IMMEDIATELY => 0,
                STOPMODE.WAIT_SECOND => 1000,
            };
        }
    }
}