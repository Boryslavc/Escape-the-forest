using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text ScoreText;

    private float timer;
    private bool gameIsOn;

    public int Score { get; private set; }

    // session manager instance initialization takes place in awake,
    // so need to subscribe in start
    // not another awake
    private void Start()
    {
        gameIsOn = true;
        ScoreText.text = "Score: 0";
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
    private void DisplayTime() => ScoreText.text = "Score: " + Score;

    public void FinishTask() => gameIsOn = false;
    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= FinishTask;
    }

}
