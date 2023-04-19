using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State current;

    void Start()
    {
        current = new DefaultState();
        current.OnEnter(gameObject);
    }

    void Update()
    {
        State next = current?.OnUpdate(gameObject);

        if (next != null && next != current)
        {
            SwitchState(next);
        }
    }

    void SwitchState(State next)
    {
        current?.OnExit(gameObject);
        current = next;
        current.OnEnter(gameObject);
    }
}
