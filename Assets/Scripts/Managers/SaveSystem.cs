using System.IO;
using System;
using UnityEngine;

public static class SaveFileSystem
{
    public const string FileNameSaveData = "/savedata.json";

    public static SaveData FindSaveData()
    {
        string filePath = Application.persistentDataPath + FileNameSaveData;
        if (!File.Exists(filePath))
        {
            return CreateSaveFile(filePath);
        }
        else
        {
            string fileContent = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(fileContent);
            return saveData;
        }

    }
    static SaveData CreateSaveFile(string filePath)
    {
        ProgressionData progressionData = new ProgressionData(0);
        SaveData saveData = new SaveData(progressionData);
        string txt = JsonUtility.ToJson(saveData);

        File.WriteAllText(filePath, txt);
        return saveData;
    }
    public static void Save(SaveData saveData)
    {
        string filePath = Application.persistentDataPath + FileNameSaveData;
        string txt = JsonUtility.ToJson(saveData);


        FileStream fileStream = File.Open(filePath, FileMode.Open);
        fileStream.SetLength(0);
        fileStream.Close();

        File.WriteAllText(filePath, txt);
    }
}
[Serializable]
public class SaveData
{
    [SerializeField] ProgressionData progressionData;
    public ProgressionData GetProgression() { return progressionData; }

    public SaveData(ProgressionData progData)
    {
        progressionData = progData;
    }
}

[Serializable]
public class ProgressionData
{
    [SerializeField] public int levelProgressed;

    public ProgressionData(int levelBeat)
    {
        levelProgressed = levelBeat;
    }
}
