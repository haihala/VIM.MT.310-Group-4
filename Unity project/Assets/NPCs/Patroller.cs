using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    public List<Transform> targets;
    int targetIndex = 0;

    NavMeshAgent agent;

    void Awake()
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

    public void Continue()
    {
        agent.destination = targets[targetIndex].position;
    }
}
