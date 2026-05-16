using UnityEngine;

public class TrashItem : MonoBehaviour, IInteractable
{
    [Header("Reward")]
    public int coinsOnPickup = 1;

    public string GetInteractionText()
    {
        return "Press E to pick up trash";
    }

    public void Interact(PlayerTrashInventory playerInventory)
    {
        if (playerInventory == null)
        {
            return;
        }

        if (playerInventory.hasTrash)
        {
            CollegeGameManager.Instance.ShowMessage("You are already carrying trash. Put it in the dustbin first.");
            return;
        }

        playerInventory.PickUpTrash();
        gameObject.SetActive(false);

        CollegeGameManager.Instance.AddCoins(coinsOnPickup);

        if (CollegeGameManager.Instance.HasEnoughCoins())
        {
            CollegeGameManager.Instance.ShowMessage("Trash picked up. You earned 1 coin. Main gate unlocked!");
        }
        else
        {
            CollegeGameManager.Instance.ShowMessage("Trash picked up. You earned 1 coin. Deposit it for a bonus coin.");
        }
    }
}
