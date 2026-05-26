using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -6f);
    [SerializeField] private float followSmoothness = 10f;
    [SerializeField] private float lookHeight = 1.5f;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + target.rotation * offset;
        float minimumCameraHeight = target.position.y + 1f;

        if (desiredPosition.y < minimumCameraHeight)
        {
            desiredPosition.y = minimumCameraHeight;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmoothness * Time.deltaTime);

        Vector3 lookTarget = target.position + Vector3.up * lookHeight;
        transform.LookAt(lookTarget);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
