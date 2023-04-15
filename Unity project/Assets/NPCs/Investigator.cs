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
        float distance = (agent.destination - transform.position).magnitude;
        if (distance <= 0.1)
        {
            // Not getting closer, discard point
            senses.Discard(agent.destination);
        }
    }

    public void Investigate(Vector3 point)
    {
        agent.destination = point;
    }

    void OnTriggerEnter(Collider other)
    {
        bool collidingWithPlayer = other.gameObject.layer == 3;
        if (collidingWithPlayer)
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.GetComponent<PlayerAnimationUpdater>().Die();
        }
    }
}
