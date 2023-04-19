using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentAnimationUpdater : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [SerializeField]
    float scaler = 4;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // We multiply by two to make the parent not run
        animator.SetFloat("Speed", Mathf.Clamp01(agent.velocity.magnitude / scaler));
    }
}
