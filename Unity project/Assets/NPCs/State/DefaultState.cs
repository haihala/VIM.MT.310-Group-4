using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : State
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
        gameObject.GetComponent<ParentIdle>().enabled = true;
    }

    public override void OnExit(GameObject gameObject)
    {
        base.OnExit(gameObject);
        gameObject.GetComponent<ParentIdle>().enabled = false;
    }
}
