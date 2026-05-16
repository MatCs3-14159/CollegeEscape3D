using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public LayerMask interactLayer = -1;

    [Header("UI")]
    public TMP_Text interactionText;

    private PlayerTrashInventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerTrashInventory>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        IInteractable interactable = GetCurrentInteractable();

        if (interactionText != null)
        {
            interactionText.text = interactable != null ? interactable.GetInteractionText() : "";
        }

        if (interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact(playerInventory);
        }
    }

    private IInteractable GetCurrentInteractable()
    {
        if (playerCamera == null)
        {
            return null;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            return hit.collider.GetComponentInParent<IInteractable>();
        }

        return null;
    }
}
