using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveData : Singleton<SaveData>
{
    private void Update()
    {
        
    }

    public void SaveDataVal(int index, int score)
    {
        Save newSave = new Save();

        newSave.levelIndex = index;
        newSave.score = score;

        string saveDataValue = JsonUtility.ToJson(newSave);
        string filePath = Application.persistentDataPath + "/SaveData.json";

        System.IO.File.WriteAllText(filePath, saveDataValue);
    }

    public Save LoadData()
    {
        Save save = new Save();
        string filePath = Application.persistentDataPath + "/SaveData.json";
        
        if(!File.Exists(filePath))
        {
            SaveDataVal(0, 0);
        }

        string saveDataValue = System.IO.File.ReadAllText(filePath);

        return save = JsonUtility.FromJson<Save>(saveDataValue);
    }

}

[System.Serializable]
public class Save
{
    public int levelIndex;
    public int score;

    public Save()
    {
        this.levelIndex = LevelManager.Ins.levelIndex;
        this.score = LevelManager.Ins.score;
    }
}
