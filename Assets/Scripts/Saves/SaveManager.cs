using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : IDisposable 
{ 
    private static string saveFilePath;

    public const string timeInGameKey = "timeInGame";
    public const string timeCyclesCountKey = "timeCyclesCount";

    public SaveManager()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "gameTimeData.json");

        Debug.Log(saveFilePath);

        //ClearSaveFile();

        // Fill the file with some dates
        //FillWithSampleData();
        //SaveGameTime();

        //SaveLastGameData();
        //LoadGamesData();
    }

    public void Dispose()
    {
        SaveLastGameData();
    }

    public void SaveGameTime()
    {
        List<GameTimeData> gameTimeDataList = LoadGameTimeData();

        GameTimeData gameTimeData = new GameTimeData
        {
            DateTimeString = DateTime.Now.ToString("o") // ISO 8601 format
        };

        gameTimeDataList.Add(gameTimeData);

        string json = JsonUtility.ToJson(new GameTimeDataList { GameTimes = gameTimeDataList }, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadGamesData()
    {
        List<GameTimeData> gameTimeDataList = LoadGameTimeData();

        foreach (var gameTimeData in gameTimeDataList)
        {
            string time = gameTimeData.DateTimeString;
            Debug.Log(gameTimeData.ToString());
            Debug.Log("Time spent in game: " + PlayerPrefs.GetString(time + timeInGameKey));
            Debug.Log("Cycles count: " + PlayerPrefs.GetString(time + timeCyclesCountKey));
        }
    }

    public void ClearSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.WriteAllText(saveFilePath, string.Empty);
            Debug.Log("Save file cleared.");
        }
    }

    private void SaveLastGameData()
    {
        List<GameTimeData> gameTimeDataList = LoadGameTimeData();

        if (gameTimeDataList.Count > 0)
        {
            GameTimeData lastGameTimeData = gameTimeDataList[gameTimeDataList.Count - 1];
            DateTime startTime = DateTime.Parse(lastGameTimeData.DateTimeString);
            DateTime endTime = DateTime.Now;
            TimeSpan timeSpent = endTime - startTime;

            string time = lastGameTimeData.DateTimeString;

            PlayerPrefs.SetString(time + timeInGameKey, timeSpent.ToString(@"mm\:ss"));
            PlayerPrefs.SetString(time + timeCyclesCountKey, PlayerPrefs.GetString("DishesDelivered")); // Replace with actual cycle count if needed

            Debug.Log("Time spent in game: " + PlayerPrefs.GetString(time + timeInGameKey));
            Debug.Log("Cycles count: " + PlayerPrefs.GetString(time + timeCyclesCountKey));
        }
    }


    public List<GameTimeData> LoadGameTimeData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameTimeDataList gameTimeDataList = JsonUtility.FromJson<GameTimeDataList>(json);
            return gameTimeDataList?.GameTimes ?? new List<GameTimeData>();
        }
        return new List<GameTimeData>();
    }

    [Serializable]
    public class GameTimeData
    {
        public string DateTimeString; // Use string for storing ISO 8601 format

        public override string ToString()
        {
            DateTime dateTime = DateTime.Parse(DateTimeString);
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }
    }

    [Serializable]
    private class GameTimeDataList
    {
        public List<GameTimeData> GameTimes;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
