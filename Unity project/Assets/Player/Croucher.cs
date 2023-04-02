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

    CharacterController characterController;
    Vector3 shift;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        standingHeight = GetComponent<CharacterController>().height;
        shift = transform.up * (standingHeight - crouchingHeight) / 2;
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
            }
            else
            {
                // Standing up
                characterController.height = standingHeight;
                characterController.center += shift;
            }
            crouching = newCrouching;
        }
    }
}
