using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraPivotVertical;
    public Transform cameraPivotHorizontal;
    public Transform modelTransform;
    public Animator modelAnimator;
    public AnimatorController idleAnimations;
    public AnimatorController runAnimations;

    public float movementSpeed;
    public float cameraSensitivityHorizontal;
    public float cameraSensitivityVertical;
    public float jumpImpulse;
    public float gravity;
    public float cameraClamp;
    public float modelRotationSpeed;

    Vector2 inputMovement;
    Vector2 cameraRotation;
    Vector3 horizontalVelocity;
    Vector3 verticalVelocity;

    float verticalRot;
    float horizontalRot;

    void Start()
    {
        verticalRot = cameraPivotVertical.localRotation.eulerAngles.x;
        horizontalRot = cameraPivotHorizontal.localRotation.eulerAngles.y;
    }

    void Update()
    {
        UpdateMovement();
        UpdateCamera();
    }

    void UpdateMovement()
    {
        horizontalVelocity = new Vector3(0, 0, 0);
        horizontalVelocity += cameraPivotHorizontal.forward * movementSpeed * inputMovement.y;
        horizontalVelocity += cameraPivotHorizontal.right * movementSpeed * inputMovement.x;
        if (!characterController.isGrounded)
        {
            verticalVelocity.y -= gravity;
        }

        characterController.Move((horizontalVelocity + verticalVelocity) * Time.deltaTime);

        if (horizontalVelocity.magnitude != 0)
        {
            modelAnimator.runtimeAnimatorController = runAnimations;

            modelTransform.rotation = Quaternion.Lerp(
                modelTransform.rotation,
                Quaternion.LookRotation(horizontalVelocity, Vector3.up),
                modelRotationSpeed * Time.deltaTime
            );
        }
        else
        {
            modelAnimator.runtimeAnimatorController = idleAnimations;
        }
    }

    void UpdateCamera()
    {
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
            verticalVelocity.y = jumpImpulse;
        }
    }
}
