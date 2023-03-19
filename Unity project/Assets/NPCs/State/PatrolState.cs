using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public override State OnUpdate(GameObject gameObject)
    {
        if (gameObject.GetComponent<TotalSenses>().IsSuspicious())
        {
            return new InvestigateState();
        }
        else
        {
            return this;
        }
    }

    public override void OnEnter(GameObject gameObject)
    {
        base.OnEnter(gameObject);
        Patroller patroller = gameObject.GetComponent<Patroller>();
        patroller.enabled = true;
        patroller.Continue();
    }

    public override void OnExit(GameObject gameObject)
    {
        base.OnExit(gameObject);
        gameObject.GetComponent<Patroller>().enabled = false;
    }
}
