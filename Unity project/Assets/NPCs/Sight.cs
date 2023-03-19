using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public List<GameObject> visibleTargets;
    public List<GameObject> targets;
    public List<int> ignoredLayers;

    public float viewDistance;
    public float viewAngle;
    int layerMask;

    void Start()
    {
        foreach (int layer in ignoredLayers)
        {
            layerMask += 1 << layer;
        }

        layerMask = ~layerMask;
    }

    void Update()
    {
        visibleTargets = targets.FindAll(Visible);
    }

    bool Visible(GameObject target)
    {
        Vector3 delta = target.transform.position - transform.position;

        if (delta.magnitude > viewDistance)
        {
            return false;
        }

        if (Vector3.Angle(delta, transform.forward) > viewAngle)
        {
            return false;
        }

        // Is there something blocking the view
        if (Physics.Raycast(transform.position, delta.normalized, delta.magnitude, layerMask))
        {
            return false;
        }

        return true;
    }
}
