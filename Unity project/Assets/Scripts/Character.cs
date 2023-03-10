using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerInput playerInput;

    public float movementSpeed;
    public float cameraSpeed;
    public float jumpImpulse;
    public float gravity;

    Vector2 inputMovement;
    Vector2 cameraRotation;
    Vector3 velocity;

    void Update()
    {
        velocity = new Vector3(0, velocity.y, 0);
        velocity += transform.right * movementSpeed * inputMovement.x;
        velocity += transform.forward * movementSpeed * inputMovement.y;
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity;
        }

        characterController.Move(velocity * Time.deltaTime);
        transform.Rotate(0, cameraSpeed * cameraRotation.x, 0);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector2>();
    }


    public void OnLookAround(InputAction.CallbackContext value)
    {
        cameraRotation = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (characterController.isGrounded && value.ReadValue<float>() > 0)
        {
            velocity.y = jumpImpulse;
        }
    }
}
