using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundEmitter : MonoBehaviour
{
    public float runningDistance;
    public float sneakingDistance;
    public float velocityThreshold;
    PlayerMover playerMover;
    Croucher croucher;

    void Start()
    {
        playerMover = GetComponent<PlayerMover>();
        croucher = GetComponent<Croucher>();
    }

    void FixedUpdate()
    {
        if (playerMover.horizontalVelocity.magnitude > velocityThreshold)
        {
            // Make sound
            float volume = croucher.crouching ? sneakingDistance : runningDistance;
            SoundCueSystem.Instance.Invoke(transform.position, runningDistance);
        }
    }
}
