using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public List<Collider> intersectingColliders;
    public List<int> layers;

    void OnTriggerEnter(Collider other)
    {
        if (layers.Contains(other.gameObject.layer))
        {
            intersectingColliders.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (intersectingColliders.Contains(other))
        {
            intersectingColliders.Remove(other);
        }
    }
}
