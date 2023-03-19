using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Sense : MonoBehaviour
{
    [SerializeField]
    protected float bundleDistance = 3;
    [SerializeField]
    protected float decay = 1;
    [SerializeField]
    protected float intensityGain = 1;
    [SerializeField]
    protected float maxIntensity = 100;

    public List<Suspicion> suspicions = new List<Suspicion>();

    protected Suspicion GameObjectToSuspicion(GameObject go)
    {
        return new Suspicion(go.transform.position, 1);
    }

    protected void UpdateSuspicions(List<Suspicion> newActive)
    {
        foreach (Suspicion incoming in newActive)
        {
            bool merged = false;

            foreach (Suspicion old in suspicions)
            {
                if ((old.point - incoming.point).magnitude < bundleDistance)
                {
                    old.point = incoming.point;
                    old.intensity = Mathf.Min(
                        old.intensity + (incoming.intensity + intensityGain) * Time.deltaTime,
                        maxIntensity
                    );
                    merged = true;
                    break;
                }
            }

            if (!merged)
            {
                suspicions.Add(incoming);
            }
        }

        foreach (Suspicion existing in suspicions)
        {
            existing.intensity = Mathf.Max(0, existing.intensity - decay * Time.deltaTime);
        }

        suspicions = (
            from suspicion in suspicions
            where suspicion.intensity > 0
            select suspicion
        ).ToList();
    }

    public List<Suspicion> Suspicions()
    {
        return suspicions;
    }

    public void Discard(Vector3 point)
    {
        suspicions = (
            from suspicion in suspicions
            where (suspicion.point - point).magnitude > bundleDistance
            select suspicion
        ).ToList();
    }
}
