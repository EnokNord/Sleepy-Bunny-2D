using System;
using System.IO;
using UnityEngine;

public static class SettingsSystem
{
    public const string FileNameSettingsData = "/settings.json";
    public static SettingsData FindSettingsData()
    {
        string filePath = Application.persistentDataPath + FileNameSettingsData;
        if (!File.Exists(filePath))
        {
            return CreateSettingsFile(filePath);
        }
        else
        {
            string fileContent = File.ReadAllText(filePath);
            SettingsData settingsData = JsonUtility.FromJson<SettingsData>(fileContent);
            return settingsData;
        }

    }
    static SettingsData CreateSettingsFile(string filePath)
    {
        SettingsData settingsData = new SettingsData();
        string txt = JsonUtility.ToJson(settingsData);

        File.WriteAllText(filePath, txt);
        return settingsData;
    }
}
[Serializable]
public class SettingsData
{
    [SerializeField] float masterVolume = 1;
    [SerializeField] float musicVolume = 1;
    [SerializeField] float effectsVolume = 1;
  
}
