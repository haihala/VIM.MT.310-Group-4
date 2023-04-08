using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : Interactable
{
    [SerializeField]
    List<InventoryItem> toolRequirements;

    [SerializeField]
    float timeToCompletion = 1;
    [SerializeField]
    GameObject modelWhenDone;
    protected float? interactionStartedAt;
    Interacter player;

    public override bool OnInteract(GameObject player, InventoryItem tool)
    {
        if (toolRequirements.Count == 0 || toolRequirements.Contains(tool))
        {
            interactionStartedAt = Time.time;
            this.player = player.GetComponent<Interacter>();
            this.player.StartInteracting(this);
            return true;
        }
        return false;
    }


    protected override void Start()
    {
        base.Start();
    }

    protected virtual void FixedUpdate()
    {
        if (GetProgress() > 1)
        {
            ChoreDone();

            player.EndInteracting();
        }
    }

    public float GetProgress()
    {
        if (interactionStartedAt == null) return 0;
        float timeUsed = Time.time - (float)interactionStartedAt;
        return timeUsed / timeToCompletion;
    }

    virtual protected void ChoreDone()
    {
        // Done
        if (modelWhenDone)
        {
            GameObject obj = Instantiate(modelWhenDone);
            obj.transform.position = transform.position;
        }

        Destroy(gameObject);
    }
}
