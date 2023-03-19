using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    public List<GameObject> intersectingColliders = new List<GameObject>();
    public List<int> layers;


    void OnTriggerEnter(Collider other)
    {
        if (layers.Contains(other.gameObject.layer))
        {
            intersectingColliders.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (intersectingColliders.Contains(other.gameObject))
        {
            intersectingColliders.Remove(other.gameObject);
        }
    }

    void Update()
    {
        UpdateSuspicions(
            intersectingColliders
                .ConvertAll(GameObjectToSuspicion)
        );
    }
}
