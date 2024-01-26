using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text bestScoreText;

    private void Start()
    {
        // can it be called before GameManager called LoadInfo method, prob should change the logic
        int sc = GameManager.Instance.BestScore;
        bestScoreText.text = "Best Score:" + sc;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
