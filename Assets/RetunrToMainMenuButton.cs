using UnityEngine;
using UnityEngine.SceneManagement;

public class RetunrToMainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
