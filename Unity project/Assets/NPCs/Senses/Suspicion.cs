using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Suspicion
{
    public Vector3 point;
    public float intensity;

    public Suspicion(Vector3 point, float intensity)
    {
        this.point = point;
        this.intensity = intensity;
    }
}
