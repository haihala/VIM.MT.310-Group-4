using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSenses : MonoBehaviour
{
    [SerializeField]
    float alertness;
    [SerializeField]
    float suspiciousThreshold;


    Sense[] senses;

    void Start()
    {
        senses = GetComponents<Sense>();
    }

    void FixedUpdate()
    {
        alertness = 0;
        foreach (Suspicion suspicion in Suspicions())
        {
            alertness += suspicion.intensity;
        }
    }

    public bool IsSuspicious()
    {
        return alertness > suspiciousThreshold;
    }

    public List<Suspicion> Suspicions()
    {
        List<Suspicion> suspicions = new List<Suspicion>();

        foreach (Sense sense in senses)
        {
            foreach (Suspicion suspicion in sense.Suspicions())
            {
                suspicions.Add(suspicion);
            }
        }

        return suspicions;
    }

    public void Discard(Vector3 point)
    {
        // Called when the guard has investigated a point
        foreach (Sense sense in senses)
        {
            sense.Discard(point);
        }
    }
}
