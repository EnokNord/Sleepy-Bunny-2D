using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    SettingsData settings;


    public SettingsData Settings { get { return settings; } }
    private void Awake()
    {
        settings = SettingsSystem.FindSettingsData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
