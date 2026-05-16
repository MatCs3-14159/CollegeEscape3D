using UnityEngine;

public class PlayerTrashInventory : MonoBehaviour
{
    [Header("Inventory State")]
    public bool hasTrash;

    public void PickUpTrash()
    {
        hasTrash = true;
    }

    public void RemoveTrash()
    {
        hasTrash = false;
    }
}
