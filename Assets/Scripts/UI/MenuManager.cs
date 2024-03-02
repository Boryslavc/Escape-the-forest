using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text BestScoreText;

    private void Start()
    {
        int score = GameManager.Instance.BestScore;
        BestScoreText.text = "Best Score:" + score;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        GameManager.Instance.SaveGameInfo();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
