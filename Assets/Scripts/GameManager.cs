using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int BestScore { get; private set; } = 0;
    public void SetBestScore(int bestScore)
    {
        BestScore = bestScore;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        LoadGameInfo();
    }

    public void SaveGameInfo()
    {
        GameInfo dataTosafe = new GameInfo();
        dataTosafe.bestScore = BestScore;

        string jsonData = JsonUtility.ToJson(dataTosafe);
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        File.WriteAllText(path, jsonData);
        Debug.Log("Data saved");
    }

    private void LoadGameInfo()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(jsonData);

            this.BestScore = gameInfo.bestScore;
        }
        Debug.Log("Data loaded");
    }
    public void LoadMainMenu()
    {
        //called just once per session so it's fine
        SessionManager sessionManager = GameObject.FindObjectOfType<SessionManager>();

        if (sessionManager != null && sessionManager.score > BestScore)
        {
            BestScore = sessionManager.score;
            Debug.Log("best score saved");
        }

        SceneManager.LoadScene(0);
    }
    [Serializable]
    public class GameInfo
    {
        public int bestScore;
    }
}
