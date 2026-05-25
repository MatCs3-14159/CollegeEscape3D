using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainGameSceneName = "MainGame";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
