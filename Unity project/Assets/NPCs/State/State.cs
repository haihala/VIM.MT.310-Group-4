using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract State OnUpdate(GameObject gameObject);
    public virtual void OnEnter(GameObject gameObject)
    {
        Debug.Log(this);
    }
    public virtual void OnExit(GameObject gameObject) { }
}
