

namespace Service
{
    public class AudioPlayer : IAudioPlayer
    {
        
    }
}
// using System;
// using UnityEngine;
// using Object = UnityEngine.Object;
//
// namespace Service
// {
//     public class AudioPlayer : IInitializable, IDisposable, IAudioPlayer
//     {
//         [Inject] private IResourcesService _resourcesService;
//         [Inject] private IGameSettingsSystem _settingsSystem;
//         
//         private Dictionary<string, AudioInstance> _instances;
//         private GameObject _defaultGameObject;
//         private class AudioInstance
//         {
//             public Event Event;
//             public GameObject GameObject;
//             public bool IsPlayed;
//
//             public AudioInstance(Event ev, GameObject gameObject)
//             {
//                 Event = ev;
//                 GameObject = gameObject;
//                 IsPlayed = false;
//             }
//         }
//
//         public void Initialize()
//         {
//             _settingsSystem.OnSettingsInitialize += ApplySettings;
//             _settingsSystem.OnSettingsChange += ApplySettings;
//             _instances = new Dictionary<string, AudioInstance>();
//             _defaultGameObject = new GameObject("Default Wwise Audio Source");
//             _defaultGameObject.AddComponent(typeof(DontDestroyOnLoad));
//         }
//
//         public void ApplySettings()
//         {
//             SetVolume(AUDIOVOLUME.MASTER, _settingsSystem.CurrentData.MasterVolume);
//             SetVolume(AUDIOVOLUME.MUSIC, _settingsSystem.CurrentData.MusicVolume);
//             SetVolume(AUDIOVOLUME.SFX, _settingsSystem.CurrentData.SFXVolume);
//             SetVolume(AUDIOVOLUME.AMBIENT, _settingsSystem.CurrentData.AmbientVolume);
//         }
//
//         public void Dispose()
//         {
//             _settingsSystem.OnSettingsInitialize -= ApplySettings;
//             _settingsSystem.OnSettingsChange -= ApplySettings;
//         }
//         
//         public void PlayLoop(string name)
//         {
//             if (!_instances.ContainsKey(name))
//             {
//                 RegisterInstance(name);
//             }
//
//             if (_instances[name].IsPlayed)
//             {
//                 return;
//             }
//             
//             _instances[name].Event.Post(_instances[name].GameObject);
//             _instances[name].IsPlayed = true;
//         }
//
//         public void PlayLoopOnAttachedObject(string name, GameObject gameObject)
//         {
//             if (!_instances.ContainsKey(name))
//             {
//                 var data = _resourcesService.GetAudioData(name);
//                 var instance = data.GetAudio();
//                 _instances.Add(name, new AudioInstance(instance, gameObject));
//             }
//
//             if (_instances[name].IsPlayed)
//             {
//                 return;
//             }
//             
//             _instances[name].Event.Post(_instances[name].GameObject);
//             _instances[name].IsPlayed = true;
//         }
//
//         public uint PlayUniqueLoop(string name, GameObject gameObject)
//         {
//             var instance = _resourcesService.GetAudioData(name).GetAudio();
//             var id = instance.Post(gameObject);
//             return id;
//         }
//
//         public void StopLoopOnAttachedObject(string name, STOPMODE stopMode)
//         {
//             if (!_instances.ContainsKey(name))
//             {
//                 Debug.Log("Это звуковое событие не запущено!");
//                 return;
//             }
//             
//             var eventInstance = _instances[name].Event;
//             var gameobject = _instances[name].GameObject;
//                 
//             eventInstance.Stop(gameobject, GetStopDuration(stopMode));
//             _instances.Remove(name);
//         }
//
//         public void StopUniqueLoop(uint id, STOPMODE stopMode = STOPMODE.IMMEDIATELY)
//         {
//             AkSoundEngine.StopPlayingID(id, GetStopDuration(stopMode));
//         }
//
//         public void PlayOnce(string name, Vector3 point = default)
//         {
//             var instance = _resourcesService.GetAudioData(name).GetAudio();
//             if (point != default)
//             {
//                 _defaultGameObject.transform.position = point;
//             }
//
//             instance.Post(_defaultGameObject);
//         }
//
//         public void PlayOnceOnAttachedObject(string name, GameObject gameObject)
//         {
//             var instance = _resourcesService.GetAudioData(name).GetAudio();
//             instance.Post(gameObject);
//         }
//
//         public void Stop(string name, STOPMODE stopMode)
//         {
//             if (!_instances.ContainsKey(name))
//             {
//                 Debug.Log("Это звуковое событие не запущено!");
//                 return;
//             }
//             
//             var eventInstance = _instances[name].Event;
//             var gameobject = _instances[name].GameObject;
//                 
//             eventInstance.Stop(gameobject, GetStopDuration(stopMode));
//             UnRegisterInstance(name);
//         }
//
//         public void Pause(string name, bool value)
//         {
//         }
//
//         public void PauseAllInstances(bool value)
//         {
//         }
//         
//         public void Switch(string switchGroup, string state, GameObject audioSource = null)
//         {
//             if (audioSource == null)
//             {
//                 AkSoundEngine.SetSwitch(switchGroup, state, _defaultGameObject);
//             }
//             else
//             {
//                 AkSoundEngine.SetSwitch(switchGroup, state, audioSource);
//             }
//         }
//
//         private void SetVolume(string name, float value)
//         {
//             AkSoundEngine.SetRTPCValue(name, value * 100);
//         }
//
//         private void RegisterInstance(string name)
//         {
//             var data = _resourcesService.GetAudioData(name);
//             var instance = data.GetAudio();
//             var gameObject = new GameObject($"Audio source {name}");
//             gameObject.AddComponent(typeof(DontDestroyOnLoad));
//             
//             _instances.Add(name, new AudioInstance(instance, gameObject));
//         }
//         
//         private void UnRegisterInstance(string name)
//         {
//             Object.Destroy(_instances[name].GameObject);
//             _instances.Remove(name);
//         }
//
//         private int GetStopDuration(STOPMODE stopMode)
//         {
//             return stopMode switch
//             {
//                 STOPMODE.IMMEDIATELY => 0,
//                 STOPMODE.WAIT_HALFSECOND => 500,
//                 STOPMODE.WAIT_SECOND => 1000,
//                 STOPMODE.WAIT_SECOND_AND_HALF => 1500,
//                 STOPMODE.WAIT_2_SECOND => 2000,
//                 STOPMODE.WAIT_3_SECOND => 3000,
//                 STOPMODE.WAIT_4_SECOND => 4000,
//             };
//         }
//     }
// }