using System;
using UnityEngine;

namespace Data
{
    // Stub class. Wwise Event class should be used instead
    [Serializable]
    public class AudioEvent
    {
        public uint Post(GameObject gameObject)
        {
            // this method play sound
            return 0;
        }
        
        public void Stop(GameObject gameObject, float fadeout)
        {
            // this method stop sound, if it's possible
        }
    }
}