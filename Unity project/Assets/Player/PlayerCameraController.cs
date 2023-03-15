using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class PlayerCameraController : MonoBehaviour
{
    public Transform horizontalPivot;
    public Transform verticalPivot;

    public float horizontalSensitivity;
    public float verticalSensitivity;
    public float verticalClamp;

    Vector2 cameraRotation;

    float verticalRot;
    float horizontalRot;

    void Start()
    {
        verticalRot = verticalPivot.localRotation.eulerAngles.x;
        horizontalRot = horizontalPivot.localRotation.eulerAngles.y;
    }

    void FixedUpdate()
    {
        horizontalRot += cameraRotation.x * horizontalSensitivity * Time.deltaTime;
        verticalRot -= cameraRotation.y * verticalSensitivity * Time.deltaTime;
        verticalRot = Mathf.Clamp(verticalRot, -verticalClamp, verticalClamp);

        verticalPivot.localRotation = Quaternion.Euler(verticalRot, 0, 0);
        horizontalPivot.localRotation = Quaternion.Euler(0, horizontalRot, 0);
    }


    public void OnLookAround(InputAction.CallbackContext value)
    {
        cameraRotation = value.ReadValue<Vector2>();
    }
}
