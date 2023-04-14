using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationUpdater : MonoBehaviour
{
    public CharacterController characterController;
    public Transform modelTransform;
    public Animator modelAnimator;
    public RuntimeAnimatorController crouchAnimations;
    public RuntimeAnimatorController idleAnimations;
    public RuntimeAnimatorController runAnimations;
    public RuntimeAnimatorController jumpAnimations;
    public RuntimeAnimatorController fallAnimations;
    public RuntimeAnimatorController deathAnimations;

    public float modelRotationSpeed;

    PlayerMover playerMover;
    Croucher croucher;

    void Start()
    {
        croucher = GetComponent<Croucher>();
        playerMover = GetComponent<PlayerMover>();
    }

    void Update()
    {
        if (modelAnimator.runtimeAnimatorController == deathAnimations)
        {
            if (modelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                SceneManager.LoadScene("Defeat");
            }
            return;
        }

        bool crouching = croucher.crouching;
        bool moving = playerMover.horizontalVelocity != Vector3.zero;
        if (moving)
        {
            modelTransform.rotation = Quaternion.Lerp(
                modelTransform.rotation,
                Quaternion.LookRotation(playerMover.horizontalVelocity, Vector3.up),
                modelRotationSpeed * Time.deltaTime
            );
        }

        modelAnimator.speed = 1;
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
        else if (moving && crouching)
        {
            // Sneaking
            modelAnimator.runtimeAnimatorController = crouchAnimations;
        }
        else if (moving && !crouching)
        {
            // Running
            modelAnimator.runtimeAnimatorController = runAnimations;
        }
        else if (!moving && crouching)
        {
            // Crouch idle
            modelAnimator.runtimeAnimatorController = crouchAnimations;
            modelAnimator.speed = 0;
            // Instead of a crouch idle state, just stop the sneak animation
            // Not the best approach,  but it's pretty good
        }
        else if (!moving && !crouching)
        {
            // Standing
            modelAnimator.runtimeAnimatorController = idleAnimations;
        }

    }

    public void Die()
    {
        modelAnimator.runtimeAnimatorController = deathAnimations;
        modelAnimator.applyRootMotion = true;
    }
}
