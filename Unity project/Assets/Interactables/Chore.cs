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

    public override void OnInteract(GameObject player, InventoryItem target)
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
        if (GetProgress() > 1)
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

    public float GetProgress()
    {
        if (startedAt == null) return 0;
        float timeUsed = Time.time - (float)startedAt;
        return timeUsed / timeToCompletion;
    }
}
