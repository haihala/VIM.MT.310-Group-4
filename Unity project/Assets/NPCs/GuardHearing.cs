using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHearing : MonoBehaviour
{
    public bool hearingPlayer = false;
    private float lastHeard;

    void Start()
    {
        SoundCueSystem.Instance.guards.AddListener(Hear);
    }

    void FixedUpdate()
    {
        if (hearingPlayer && lastHeard + 0.1 < Time.time)
        {
            // Stop hearing the player after a while
            hearingPlayer = false;
        }
    }

    void Hear(Vector3 position, float maxDistance)
    {
        if (maxDistance > (position - transform.position).magnitude)
        {
            hearingPlayer = true;
            lastHeard = Time.time;
        }
    }
}
