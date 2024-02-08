using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;

    public Timer Timer;
    public Enemy enemy;
    public Player playerController;
    public AudioSource BackGroundMusic;

    public TMP_Text finalScoreText;

    public event UnityAction OnGameOver;

    public GameObject pauseMenu;
    public GameObject loseMenu;

    private bool isPaused = false;

    public int FinalScore { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        playerController.OnPlayerDied += EndSession;
    }

    public void Start()
    {
        StartSession();
        BackGroundMusic.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void StartSession()
    {
        playerController.gameObject.transform.position = playerController.PlayerSpawnPoint.position;
        enemy.gameObject.transform.position = enemy.SpawnPoint.position;

        playerController.StartIntro();
        enemy.StartIntro();
        Spawner.Instance.StartSpawning();
    }
    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }
    private void EndSession()
    {
        OnGameOver?.Invoke();

        loseMenu.SetActive(true);
    
        FinalScore = Timer.Score;
        finalScoreText.text = "Your result: " + FinalScore;

        CheckAndSaveBestScore();
    }
    private void CheckAndSaveBestScore()
    {
        int bestScore = GameManager.Instance.BestScore;
        if (FinalScore > bestScore)
        {
            GameManager.Instance.SetBestScore(FinalScore);
            Debug.Log("Best score set");
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void OnDisable()
    {
        playerController.OnPlayerDied -= EndSession;
    }
}
