using UnityEngine;

public class GateDoor : MonoBehaviour, IInteractable
{
    [Header("Gate Parts")]
    public GameObject closedGateObject;
    public GameObject openGateObject;

    [Header("State")]
    public bool isUnlocked;

    private void Start()
    {
        SetGateVisuals();
    }

    public void UnlockGate()
    {
        isUnlocked = true;
        SetGateVisuals();
    }

    public string GetInteractionText()
    {
        if (isUnlocked)
        {
            return "Press E to escape";
        }

        return "Gate locked. Need more coins.";
    }

    public void Interact(PlayerTrashInventory playerInventory)
    {
        if (!isUnlocked)
        {
            CollegeGameManager.Instance.ShowMessage("The main gate is locked. Earn more coins first.");
            return;
        }

        CollegeGameManager.Instance.WinGame();
    }

    private void SetGateVisuals()
    {
        if (closedGateObject != null)
        {
            closedGateObject.SetActive(!isUnlocked);
        }

        if (openGateObject != null)
        {
            openGateObject.SetActive(isUnlocked);
        }
    }
}
