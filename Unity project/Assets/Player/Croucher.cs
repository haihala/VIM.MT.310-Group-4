using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Croucher : MonoBehaviour
{
    public bool crouching;

    float standingHeight;
    [SerializeField]
    float crouchingHeight;
    [SerializeField]
    float crouchingVolume;
    [SerializeField]
    float crouchingStepInterval;

    CharacterController characterController;
    AudioSource stepSFX;
    FootstepSFX stepEmitter;
    Vector3 shift;

    float defaultVolume;
    float defaultStepInterval;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        standingHeight = GetComponent<CharacterController>().height;
        shift = transform.up * (standingHeight - crouchingHeight) / 2;
        stepSFX = GetComponent<AudioSource>();
        defaultVolume = stepSFX.volume;
        stepEmitter = GetComponent<FootstepSFX>();
        defaultStepInterval = stepEmitter.interval;
    }

    public void OnInput(InputAction.CallbackContext value)
    {
        bool newCrouching = value.ReadValue<float>() > 0;

        if (newCrouching != crouching)
        {
            if (newCrouching)
            {
                // Crouched down
                characterController.height = crouchingHeight;
                characterController.center -= shift;
                stepSFX.volume = crouchingVolume;
                stepEmitter.interval = crouchingStepInterval;
            }
            else
            {
                // Standing up
                characterController.height = standingHeight;
                characterController.center += shift;
                stepSFX.volume = defaultVolume;
                stepEmitter.interval = defaultStepInterval;
            }
            crouching = newCrouching;
        }
    }
}
