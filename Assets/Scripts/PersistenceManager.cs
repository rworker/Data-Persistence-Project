using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;

    public string Name;
    public List<int> HighScores;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public List<int> HighScores;
    }

    public int GetHighestScore(List<int> scores)
    {
        int highestScore = scores[0];
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] > highestScore)
                highestScore = scores[i];
        }
        return highestScore;
    }

    public int GetLowestScore(List<int> scores)
    {
        int lowestScore = scores[0];
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] < lowestScore)
                lowestScore = scores[i];
        }
        return lowestScore;
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.HighScores = HighScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScores = data.HighScores;
        }

    }
}
