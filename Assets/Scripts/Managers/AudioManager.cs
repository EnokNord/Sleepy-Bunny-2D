using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundCollection soundEffects;
    [SerializeField] SoundCollection music;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundEffectSource;

    SettingsData settings;

    public SoundCollection SoundEffects {  get { return soundEffects; } }
    public SoundCollection Music {  get { return music; } }

    private void Start()
    {
        SettingsManager settingsManager = GetComponent<SettingsManager>();
        UpdateSettings(settingsManager.Settings);
        PlayMusic(music.SoundNames[0]);
        musicSource.ignoreListenerPause = true;
        
    }
    public void UpdateSettings(SettingsData newSettings)
    {
        settings = newSettings;
        float masterVolume = settings.Muted ? 0 : settings.MasterVolume;
        musicSource.volume = masterVolume * settings.MusicVolume;
        soundEffectSource.volume = masterVolume * settings.EffectsVolume;
    }
    private void OnLevelWasLoaded(int level)
    {
        PlayMusic(music.SoundNames[level]);
    }
    public void PlayMusic(string musicName)
    {
        if (!music.SoundNames.Contains(musicName)) return;

        int index = music.SoundNames.IndexOf(musicName);
        if (index >= 0 && music.Sounds.Count > 0 && music.Sounds.Count >= index && music.Sounds[index])
        {
            musicSource.resource = music.Sounds[index];
            musicSource.Play();
        }
    }
    public void PlaySoundEffect(string effectName)
    {
        if (!soundEffects.SoundNames.Contains(effectName)) return;
     
        int index = soundEffects.SoundNames.IndexOf(effectName);
        if (index >= 0 && soundEffects.Sounds.Count >= index && soundEffects.Sounds[index])
        {
            soundEffectSource.resource = soundEffects.Sounds[index];
            soundEffectSource.Play();
        }
    }
}
