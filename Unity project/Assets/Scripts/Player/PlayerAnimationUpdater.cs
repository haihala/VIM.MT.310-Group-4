using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class PlayerAnimationUpdater : MonoBehaviour
{
    public PlayerMover playerMover;
    public CharacterController characterController;
    public Transform modelTransform;
    public Animator modelAnimator;
    public AnimatorController idleAnimations;
    public AnimatorController runAnimations;
    public AnimatorController jumpAnimations;
    public AnimatorController fallAnimations;

    public float modelRotationSpeed;


    void Update()
    {
        bool moving = playerMover.horizontalVelocity != Vector3.zero;
        if (moving)
        {
            modelTransform.rotation = Quaternion.Lerp(
                modelTransform.rotation,
                Quaternion.LookRotation(playerMover.horizontalVelocity, Vector3.up),
                modelRotationSpeed * Time.deltaTime
            );
        }

        if (!characterController.isGrounded)
        {
            if (playerMover.verticalVelocity.y > 0)
            {
                // Going up
                modelAnimator.runtimeAnimatorController = jumpAnimations;
            }
            else
            {
                // Coming down
                modelAnimator.runtimeAnimatorController = fallAnimations;
            }
        }
        else if (moving)
        {
            // Running
            modelAnimator.runtimeAnimatorController = runAnimations;
        }
        else
        {
            // Standing
            modelAnimator.runtimeAnimatorController = idleAnimations;
        }
    }
}
