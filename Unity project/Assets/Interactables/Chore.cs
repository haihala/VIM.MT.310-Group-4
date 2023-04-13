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
    protected GameObject modelWhenDone;
    [SerializeField]
    protected GameObject particlesWhenDone;
    protected float? interactionStartedAt;
    Interacter player;
    AudioSource sfx;


    public override bool OnInteract(GameObject player, InventoryItem tool)
    {
        if (toolRequirements.Count == 0 || toolRequirements.Contains(tool))
        {
            interactionStartedAt = Time.time;
            this.player = player.GetComponent<Interacter>();
            this.player.StartInteracting(this);
            if (sfx != null)
            {
                sfx.Play();
            }
            return true;
        }
        return false;
    }


    protected override void Start()
    {
        sfx = GetComponent<AudioSource>();
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
            GameObject model = Instantiate(modelWhenDone);
            model.transform.position = transform.position;
        }
        if (particlesWhenDone)
        {
            GameObject particles = Instantiate(particlesWhenDone);
            particles.transform.position = transform.position;
        }
        if (sfx != null)
        {
            sfx.Stop();
        }

        Task task = GetComponent<Task>();
        if (task != null)
        {
            TaskManager.Instance.MarkComplete(task);
        }
        Destroy(gameObject);
    }
}
