using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraPivot;

    public float movementSpeed;
    public float cameraSpeed;
    public float jumpImpulse;
    public float gravity;
    public float cameraClamp;

    Vector2 inputMovement;
    Vector2 cameraRotation;
    Vector3 velocity;

    float rotX;
    float rotY;

    void Start()
    {
        rotX = cameraPivot.localRotation.eulerAngles.x;
    }

    void Update()
    {
        // Movement
        velocity = new Vector3(0, velocity.y, 0);
        // This does rely on the facing of the player object, which is rotated accordingly,
        // but once a model has been added, it will look weird as the model will rotate in place when you turn the camera.
        velocity += transform.right * movementSpeed * inputMovement.x;
        velocity += transform.forward * movementSpeed * inputMovement.y;
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Rotation
        transform.Rotate(0, cameraRotation.x * cameraSpeed * Time.deltaTime, 0);
        rotX -= cameraRotation.y * cameraSpeed * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -cameraClamp, cameraClamp);

        cameraPivot.localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
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
