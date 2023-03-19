using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundEmitter : MonoBehaviour
{
    public float audibleDistance;
    public float velocityThreshold;
    PlayerMover playerMover;

    void Start()
    {
        playerMover = GetComponent<PlayerMover>();
    }

    void FixedUpdate()
    {
        if (playerMover.horizontalVelocity.magnitude > velocityThreshold)
        {
            SoundCueSystem.Instance.guards.Invoke(transform.position, audibleDistance);
        }
    }
}
