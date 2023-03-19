using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : State
{
    public override State OnUpdate(GameObject gameObject)
    {
        TotalSenses detection = gameObject.GetComponent<TotalSenses>();
        if (!detection.IsSuspicious())
        {
            return new PatrolState();
        }

        List<Suspicion> suspicions = detection.Suspicions();
        if (suspicions.Count > 0)
        {
            Suspicion topSuspicion = null;

            foreach (Suspicion suspicion in suspicions)
            {
                if (topSuspicion == null || suspicion.intensity > topSuspicion.intensity)
                {
                    topSuspicion = suspicion;
                }
            }

            gameObject.GetComponent<Investigator>().Investigate(topSuspicion.point);
        }

        return this;
    }

    public override void OnEnter(GameObject gameObject)
    {
        base.OnEnter(gameObject);
        gameObject.GetComponent<Investigator>().enabled = true;
    }

    public override void OnExit(GameObject gameObject)
    {
        base.OnExit(gameObject);
        gameObject.GetComponent<Investigator>().enabled = false;
    }
}
