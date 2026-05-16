using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollegeGameManager : MonoBehaviour
{
    public static CollegeGameManager Instance;

    [Header("Win Requirements")]
    public int coinsNeededToUnlockGate = 5;

    [Header("Current Progress")]
    public int currentCoins;

    [Header("UI")]
    public TMP_Text coinsText;
    public TMP_Text messageText;
    public GameObject winPanel;

    [Header("Gate")]
    public GateDoor mainGate;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        UpdateUI();
        ShowMessage("Collect trash and put it in the dustbin.");
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateUI();

        if (currentCoins >= coinsNeededToUnlockGate)
        {
            ShowMessage("Main gate unlocked!");

            if (mainGate != null)
            {
                mainGate.UnlockGate();
            }
        }
        else
        {
            int remaining = coinsNeededToUnlockGate - currentCoins;
            ShowMessage("You earned coins. Need " + remaining + " more.");
        }
    }

    public bool HasEnoughCoins()
    {
        return currentCoins >= coinsNeededToUnlockGate;
    }

    public void WinGame()
    {
        ShowMessage("You escaped the college. You win!");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    private void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + currentCoins + " / " + coinsNeededToUnlockGate;
        }
    }
}
