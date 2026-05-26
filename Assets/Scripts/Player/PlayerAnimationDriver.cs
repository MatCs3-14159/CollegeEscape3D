using StarterAssets;
using UnityEngine;

public class PlayerAnimationDriver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private StarterAssetsInputs starterInputs;
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("Animation State Names")]
    [SerializeField] private string idleStateName = "Idle";
    [SerializeField] private string walkStateName = "Walk";
    [SerializeField] private string runStateName = "Run";
    [SerializeField] private string jumpStateName = "Jump";

    [Header("Settings")]
    [SerializeField] private float runSpeedThreshold = 5f;
    [SerializeField] private float crossFadeTime = 0.15f;

    private string currentStateName;

    private void Awake()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        if (starterInputs == null)
        {
            starterInputs = GetComponent<StarterAssetsInputs>();
        }

        if (firstPersonController == null)
        {
            firstPersonController = GetComponent<FirstPersonController>();
        }

        if (characterAnimator == null)
        {
            characterAnimator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (characterAnimator == null || characterController == null || starterInputs == null)
        {
            return;
        }

        string nextStateName = GetAnimationStateName();
        PlayAnimation(nextStateName);
    }

    private string GetAnimationStateName()
    {
        bool isGrounded = firstPersonController == null || firstPersonController.Grounded;
        bool isMoving = starterInputs.move.sqrMagnitude > 0.01f;
        float horizontalSpeed = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z).magnitude;

        if (!isGrounded)
        {
            return jumpStateName;
        }

        if (!isMoving || horizontalSpeed < 0.1f)
        {
            return idleStateName;
        }

        if (starterInputs.sprint || horizontalSpeed >= runSpeedThreshold)
        {
            return runStateName;
        }

        return walkStateName;
    }

    private void PlayAnimation(string stateName)
    {
        if (currentStateName == stateName)
        {
            return;
        }

        characterAnimator.CrossFade(stateName, crossFadeTime);
        currentStateName = stateName;
    }
}
