using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : Sense
{
    List<Suspicion> newEvents = new List<Suspicion>();

    void Start()
    {
        SoundCueSystem.Instance.AddListener(Hear);
    }

    void FixedUpdate()
    {
        UpdateSuspicions(newEvents);
        newEvents.Clear();
    }

    void Hear(Vector3 position, float maxDistance)
    {
        float intensity = maxDistance / (position - transform.position).magnitude;
        if (intensity > 1)
        {
            newEvents.Add(new Suspicion(position, intensity));
        }
    }
}
