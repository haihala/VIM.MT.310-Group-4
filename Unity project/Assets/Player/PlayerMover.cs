using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class PlayerMover : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraPivotHorizontal;

    public float movementSpeed;
    public float jumpImpulse;
    public float gravity;

    // Set internally
    public Vector3 horizontalVelocity;
    public Vector3 verticalVelocity;
    Vector2 inputMovement;

    float horizontalRot;

    void Start()
    {
        horizontalRot = cameraPivotHorizontal.localRotation.eulerAngles.y;
    }

    void Update()
    {
        horizontalVelocity = new Vector3(0, 0, 0);
        horizontalVelocity += cameraPivotHorizontal.forward * movementSpeed * inputMovement.y;
        horizontalVelocity += cameraPivotHorizontal.right * movementSpeed * inputMovement.x;
        if (!characterController.isGrounded)
        {
            verticalVelocity.y -= gravity;
        }

        characterController.Move((horizontalVelocity + verticalVelocity) * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (characterController.isGrounded && value.ReadValue<float>() > 0)
        {
            verticalVelocity.y = jumpImpulse;
        }
    }
}
