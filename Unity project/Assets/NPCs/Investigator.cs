using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Investigator : MonoBehaviour
{
    NavMeshAgent agent;
    TotalSenses senses;

    float distanceLastFrame;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        senses = GetComponent<TotalSenses>();
    }

    void Update()
    {
        float distance = (agent.destination - transform.position).magnitude;
        if (distance >= distanceLastFrame)
        {
            // Not getting closer, discard point
            senses.Discard(agent.destination);
        }
        distanceLastFrame = distance;
    }

    public void Investigate(Vector3 point)
    {
        agent.destination = point;
        distanceLastFrame = Mathf.Infinity;
    }

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Defeat");
    }
}
