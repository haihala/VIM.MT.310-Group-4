using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehavior : MonoBehaviour
{
    public List<Transform> targets;
    int targetIndex = -1;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void GotoNextPoint()
    {
        targetIndex = (targetIndex + 1) % targets.Count;
        agent.destination = targets[targetIndex].position;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}
