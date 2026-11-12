using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

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
    static void ValidateSaveFile()
    {
        //TODO: Check if save file contains all the right values
    }
    static SaveData CreateSaveFile(string filePath)
    {
        SaveData saveData = new SaveData();
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
    [SerializeField] int levelProgressed;
    [SerializeField] public int[] levelCheckpoints;
    // TODO: Find better way to represent which points have been picked up
    [SerializeField] public int[,] levelPoints;
    public int LevelProgressed { get { return levelProgressed; } set { levelProgressed = value; } }

    public SaveData(int startLevel = 1)
    {
        levelProgressed = startLevel;
        levelCheckpoints = new int[30];
        levelPoints = new int[30, 2];
    }
}


