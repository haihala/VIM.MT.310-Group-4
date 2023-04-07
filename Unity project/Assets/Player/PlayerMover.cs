using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    public float crouchingSpeed;
    public float standingSpeed;
    public float jumpImpulse;
    public float gravity;

    [SerializeField]
    InventoryItem cowboyHat;
    [SerializeField]
    float cowboyHatSpeedBoost;

    public Transform cameraPivotHorizontal;
    Interacter interacter;
    CharacterController characterController;
    Croucher croucher;
    Inventory inventory;


    // Set internally
    public Vector3 horizontalVelocity;
    public Vector3 verticalVelocity;
    Vector2 inputMovement;
    bool jumping;

    float horizontalRot;

    void Start()
    {
        horizontalRot = cameraPivotHorizontal.localRotation.eulerAngles.y;
        characterController = GetComponent<CharacterController>();
        croucher = GetComponent<Croucher>();
        interacter = GetComponent<Interacter>();
        inventory = GetComponent<Inventory>();
    }

    void FixedUpdate()
    {
        if (interacter.Interacting())
        {
            // Don't move if interacting with something
            // Not a perfect solution
            return;
        }

        horizontalVelocity = new Vector3(0, 0, 0);

        float movementSpeed = croucher.crouching ? crouchingSpeed : standingSpeed;

        if (inventory.equipment == cowboyHat)
        {
            movementSpeed += cowboyHatSpeedBoost;
        }

        horizontalVelocity += cameraPivotHorizontal.forward * movementSpeed * inputMovement.y;
        horizontalVelocity += cameraPivotHorizontal.right * movementSpeed * inputMovement.x;

        if (!characterController.isGrounded)
        {
            verticalVelocity.y -= gravity * Time.deltaTime;
        }
        else if (jumping)
        {
            verticalVelocity.y = jumpImpulse;
        }

        characterController.Move((horizontalVelocity + verticalVelocity) * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        jumping = value.ReadValue<float>() > 0;
    }
}
