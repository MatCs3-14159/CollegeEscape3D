using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public LayerMask interactLayer = -1;

    [Header("Nearby Interaction")]
    public float nearbyInteractRadius = 2.5f;

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
        IInteractable nearbyInteractable = GetNearestNearbyInteractable();

        if (nearbyInteractable != null)
        {
            return nearbyInteractable;
        }

        if (playerCamera == null)
        {
            return null;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, interactDistance, interactLayer);
        hits = hits.OrderBy(hit => hit.distance).ToArray();

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.transform.IsChildOf(transform))
            {
                continue;
            }

            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                return interactable;
            }
        }

        return null;
    }

    private IInteractable GetNearestNearbyInteractable()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, nearbyInteractRadius, interactLayer);

        return nearbyColliders
            .Where(colliderHit => !colliderHit.transform.IsChildOf(transform))
            .Select(colliderHit => colliderHit.GetComponentInParent<IInteractable>())
            .Where(interactable => interactable != null)
            .OrderBy(interactable => Vector3.Distance(transform.position, ((MonoBehaviour)interactable).transform.position))
            .FirstOrDefault();
    }
}
