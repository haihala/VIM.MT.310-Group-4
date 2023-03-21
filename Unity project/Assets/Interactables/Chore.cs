using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : Interactable
{
    [SerializeField]
    float timeToCompletion = 1;
    [SerializeField]
    GameObject modelWhenDone;
    float? startedAt;

    public override void OnInteract(GameObject player)
    {
        // TODO: Lock player into animation
        startedAt = Time.time;
    }


    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        if (timeToCompletion < Time.time - startedAt)
        {
            // Done
            GameObject obj = Instantiate(modelWhenDone);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
