using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;

    [SerializeField] private Timer Timer;
    [SerializeField] private Player playerController;
    [SerializeField] private Enemy enemy;
    [SerializeField] private AudioSource BackGroundMusic;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loseMenu;

    public TMP_Text finalScoreText;
    public event UnityAction OnGameStarted;
    public event UnityAction OnGameOver;

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
        Time.timeScale = 1;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void StartSession()
    {
        OnGameStarted?.Invoke();

        playerController.StartIntro();
    }

    public void PauseGame()
    {
        if (isPaused)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;

        pauseMenu.SetActive(!isPaused);
        isPaused = !isPaused;
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
