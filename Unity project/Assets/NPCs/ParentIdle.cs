using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentIdle : MonoBehaviour
{
    [SerializeField]
    Transform couchTarget;

    [SerializeField]
    Transform fridgeTarget;

    [SerializeField]
    Transform TV;

    [SerializeField]
    Transform fridge;

    NavMeshAgent agent;
    Transform lookAtTarget;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Routine());
    }

    void Update()
    {
        bool arrived = agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.1;
        if (lookAtTarget != null && arrived)
        {
            Vector3 lookPos = lookAtTarget.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        }
    }

    IEnumerator Routine()
    {
        while (true)
        {
            GoToTV();
            yield return new WaitForSeconds(35);
            GoToFridge();
            yield return new WaitForSeconds(25);
        }
    }

    void GoToFridge()
    {
        agent.SetDestination(fridgeTarget.position);
        lookAtTarget = fridge;
    }

    void GoToTV()
    {
        agent.SetDestination(couchTarget.position);
        lookAtTarget = TV;
    }

    void OnEnable()
    {
        if (lookAtTarget == TV)
        {
            GoToTV();
        }
        else if (lookAtTarget == fridge)
        {
            GoToFridge();
        }
    }
}
