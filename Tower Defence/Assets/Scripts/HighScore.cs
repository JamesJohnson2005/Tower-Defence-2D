using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScore : MonoBehaviour
{
    private PlayerData playerData = new PlayerData();
    public int highScore;

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
        highScore = loadedPlayerData._highScore;
    }

    public void SaveScore(int newScore)
    {
        playerData._highScore = newScore;

        if (newScore > highScore)
        {
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(Application.dataPath + "/saveFile.json", json);
        }
        LoadData();
    }


    private class PlayerData
    {
     public int _highScore;
    }
    
}

