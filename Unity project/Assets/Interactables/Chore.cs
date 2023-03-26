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
    Interacter player;

    public override void OnInteract(GameObject player)
    {
        startedAt = Time.time;
        this.player = player.GetComponent<Interacter>();
        this.player.StartInteracting(this);
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
            if (modelWhenDone)
            {
                GameObject obj = Instantiate(modelWhenDone);
                obj.transform.position = transform.position;
            }
            Destroy(gameObject);

            player.EndInteracting();
        }
    }
}
