using UnityEngine;
using UnityEngine.UI;


public class PauseMenuController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    SettingsManager settingsManager;
    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsManager = FindAnyObjectByType<SettingsManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        if (!settingsManager || !audioManager)
        {
            enabled = false;
            return;
        }
        volumeSlider.value = settingsManager.Settings.MasterVolume;
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    public void Mute()
    {
        settingsManager.Settings.Muted = !settingsManager.Settings.Muted;
        volumeSlider.value = settingsManager.Settings.Muted ? 0 : 1;
        audioManager.UpdateSettings(settingsManager.Settings);
    }
    public void ValueChangeCheck()
    {
        settingsManager.Settings.Muted = false;
        settingsManager.Settings.MasterVolume = volumeSlider.value;
        audioManager.UpdateSettings(settingsManager.Settings);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
