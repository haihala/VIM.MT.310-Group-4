using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Investigator : MonoBehaviour
{
    NavMeshAgent agent;
    TotalSenses senses;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        senses = GetComponent<TotalSenses>();
    }

    void Update()
    {
        if ((agent.destination - transform.position).magnitude < 1)
        {
            // At the investigation point
            senses.Discard(agent.destination);
        }
    }

    public void Investigate(Vector3 point)
    {
        agent.destination = point;
    }
}
