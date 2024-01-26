using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;

    [SerializeField] private float scoreMultiplier = 1f;
    [SerializeField] private float timer;
    [SerializeField] private bool gameIsOn = false;

    public Background background;
    public Spawner spawner;
    public Enemy enemy;
    public Player playerController;
    public AudioSource BackGroundMusic;

    public TMP_Text scoreText;
    public TMP_Text finalScore;
    public int score { get; private set; }

    public GameObject pauseMenu;
    public GameObject loseMenu;

    private bool isPaused = false;

    private void OnEnable()
    {
        playerController.OnPlayerDied += EndSession;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void Start()
    {
        StartSession();
        BackGroundMusic.Play();
    }

    private void Update()
    {
        if (gameIsOn)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                // prob should make an progressional incrementation of scoreMultiplier
                // but nobody is gonna play it that long
                if (score > 120)
                    scoreMultiplier = 2f;
                score += (int)(3 * scoreMultiplier);
                scoreText.text = "Score: " + score;
                timer = 0;
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void StartSession()
    {
        // separate UI logic
        score = 0;
        scoreText.text = "Score: 0";
        loseMenu.SetActive(false);

        playerController.gameObject.transform.position = playerController.PlayerSpawnPoint.position;
        enemy.gameObject.transform.position = enemy.SpawnPoint.position;

        playerController.StartIntro();
        enemy.StartIntro();
        Spawner.Instance.StartSpawningFeathers();
        
        background.shouldRoll = true;
        gameIsOn = true;
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
        background.shouldRoll = false;

        enemy.StopShooting();

        loseMenu.SetActive(true);
        finalScore.text = "Score:" + score;

        Spawner.Instance.StopSpawningFeathers();

        int bestScore = GameManager.Instance.BestScore;
        if (score > bestScore)
        {
            GameManager.Instance.SetBestScore(score);
            Debug.Log("Best score set");
        }
        Debug.Log("game is on false");
        gameIsOn = false;
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
