using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraPivotVertical;
    public Transform cameraPivotHorizontal;

    public float movementSpeed;
    public float cameraSensitivityHorizontal;
    public float cameraSensitivityVertical;
    public float jumpImpulse;
    public float gravity;
    public float cameraClamp;

    Vector2 inputMovement;
    Vector2 cameraRotation;
    Vector3 velocity;

    float verticalRot;
    float horizontalRot;

    void Start()
    {
        verticalRot = cameraPivotVertical.localRotation.eulerAngles.x;
        horizontalRot = cameraPivotHorizontal.localRotation.eulerAngles.y;
    }

    void Update()
    {
        // Movement
        velocity = new Vector3(0, velocity.y, 0);
        velocity += cameraPivotHorizontal.forward * movementSpeed * inputMovement.y;
        velocity += cameraPivotHorizontal.right * movementSpeed * inputMovement.x;
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Rotation
        horizontalRot += cameraRotation.x * cameraSensitivityHorizontal * Time.deltaTime;
        verticalRot -= cameraRotation.y * cameraSensitivityVertical * Time.deltaTime;
        verticalRot = Mathf.Clamp(verticalRot, -cameraClamp, cameraClamp);

        cameraPivotVertical.localRotation = Quaternion.Euler(verticalRot, 0, 0);
        cameraPivotHorizontal.localRotation = Quaternion.Euler(0, horizontalRot, 0);
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
