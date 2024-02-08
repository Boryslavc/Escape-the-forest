using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private bool gameIsOn;

    public TMP_Text scoreText;
    public int Score { get; private set; }

    // session manager instance initialization takes place in awake,
    // so need to subscribe in start
    // not another awake
    private void Start()
    {
        gameIsOn = true;
        scoreText.text = "Score: 0";
        SessionManager.Instance.OnGameOver += FinishTask;
    }
    
    private void Update()
    {
        CountTime();
    }

    private void CountTime()
    {
        if (gameIsOn)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                Score += 2;
                DisplayTime();
                timer = 0;
            }
        }
    }
    private void DisplayTime() => scoreText.text = "Score: " + Score;

    public void FinishTask() => gameIsOn = false;
    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= FinishTask;
    }

}
