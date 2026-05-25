using UnityEngine;

public class Dustbin : MonoBehaviour, IInteractable
{
    [Header("Reward")]
    public int bonusCoinsPerTrash = 1;

    public string GetInteractionText()
    {
        return "Press E to deposit trash for bonus coins";
    }

    public void Interact(PlayerTrashInventory playerInventory)
    {
        if (playerInventory == null)
        {
            return;
        }

        if (!playerInventory.hasTrash)
        {
            CollegeGameManager.Instance.ShowMessage("Pick up trash first.");
            return;
        }

        playerInventory.RemoveTrash();
        CollegeGameManager.Instance.AddCoins(bonusCoinsPerTrash);

        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.AddTrashDeposit();
        }

        if (ObjectiveManager.Instance != null && ObjectiveManager.Instance.AreObjectivesCompleted())
        {
            CollegeGameManager.Instance.ShowMessage("Trash deposited. Objectives complete. Main gate unlocked!");
        }
        else
        {
            CollegeGameManager.Instance.ShowMessage("Trash deposited. You earned 1 bonus coin.");
        }
    }
}
