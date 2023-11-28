namespace Service
{
    public interface IAudioPlayer
    {
        
    }
}
//     {
//         public void PlayLoop(string name);
//         public void PlayLoopOnAttachedObject(string name, GameObject gameObject);
//         public uint PlayUniqueLoop(string name, GameObject gameObject);
//         public void PlayOnce(string name, Vector3 point = default);
//         public void PlayOnceOnAttachedObject(string name, GameObject gameObject);
//         public void Stop(string name, STOPMODE stopMode = STOPMODE.IMMEDIATELY);
//         public void StopLoopOnAttachedObject(string name, STOPMODE stopMode = STOPMODE.IMMEDIATELY);
//         public void StopUniqueLoop(uint id, STOPMODE stopMode = STOPMODE.IMMEDIATELY);
//         public void Pause(string name, bool value);
//         public void PauseAllInstances(bool value);
//         void Switch(string switchGroup, string state, GameObject audioSource = null);
//     }
// }