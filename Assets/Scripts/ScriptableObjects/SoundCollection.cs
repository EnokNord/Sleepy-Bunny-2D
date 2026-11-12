using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundCollection", menuName = "Scriptable Objects/SoundCollection")]
public class SoundCollection : ScriptableObject
{
    [SerializeField]  List<AudioClip> sounds = new List<AudioClip>();
    [SerializeField] List<string> soundNames = new List<string>();

    public List<AudioClip> Sounds {  get { return sounds; } }
    public List<string> SoundNames { get { return soundNames; } }

}




