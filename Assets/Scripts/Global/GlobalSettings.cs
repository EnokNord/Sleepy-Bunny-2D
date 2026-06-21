using UnityEditor;
using UnityEditor.Profiling;
using UnityEngine;
namespace Global 
{ 
    public static class GlobalSettings
    {
        public static int SoundVolume 
        { 
            get { return SoundVolume; } 
            set 
            { 
                SoundVolume = Mathf.Clamp(value, 0, 100);
            } 
        }
    }   
}
