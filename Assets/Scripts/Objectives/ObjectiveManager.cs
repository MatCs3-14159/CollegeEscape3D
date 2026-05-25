using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("Objective Requirements")]
    [SerializeField] private int requiredCoins = 10;
    [SerializeField] private int requiredTrashDeposits = 5;

    [Header("Current Progress")]
    [SerializeField] private int currentCoins;
    [SerializeField] private int trashDeposited;

    [Header("Scene References")]
    [SerializeField] private GateDoor mainGate;
    [SerializeField] private TMP_Text objectiveText;

    private bool objectivesCompleted;

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
        UpdateObjectiveUI();
    }

    public void SetCoinProgress(int coins)
    {
        currentCoins = coins;
        CheckObjectives();
        UpdateObjectiveUI();
    }

    public void AddTrashDeposit()
    {
        trashDeposited++;
        CheckObjectives();
        UpdateObjectiveUI();
    }

    public bool AreObjectivesCompleted()
    {
        return objectivesCompleted;
    }

    private void CheckObjectives()
    {
        if (objectivesCompleted)
        {
            return;
        }

        bool hasEnoughCoins = currentCoins >= requiredCoins;
        bool hasEnoughDeposits = trashDeposited >= requiredTrashDeposits;

        if (hasEnoughCoins && hasEnoughDeposits)
        {
            objectivesCompleted = true;

            if (mainGate != null)
            {
                mainGate.UnlockGate();
            }

            if (CollegeGameManager.Instance != null)
            {
                CollegeGameManager.Instance.ShowMessage("Objectives complete. Main gate unlocked!");
            }
        }
    }

    private void UpdateObjectiveUI()
    {
        if (objectiveText == null)
        {
            return;
        }

        objectiveText.text =
            "Coins: " + currentCoins + " / " + requiredCoins +
            "\nTrash Deposited: " + trashDeposited + " / " + requiredTrashDeposits;
    }
}
