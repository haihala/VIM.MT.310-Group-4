using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WashingMachineState
{
    Empty,
    Busy,
    Done,
}

public class WashingMachine : Chore
{
    [SerializeField]
    InventoryItem cleanHamper;

    WashingMachineState wms = WashingMachineState.Empty;
    float started;
    [SerializeField]
    float washingTime;

    [SerializeField]
    float shakeStrength;
    [SerializeField]
    Vector3 shakeFrequences;
    [SerializeField]
    Vector3 shakeOffsets;
    Vector3 shakeShift;

    ParticleSystem doneParticles;
    Task beingWashed;

    public override bool OnInteract(GameObject player, InventoryItem tool)
    {
        Inventory inventory = player.GetComponent<Inventory>();

        switch (wms)
        {
            case WashingMachineState.Empty:
                if (base.OnInteract(player, tool))
                {
                    // Consume the laundry hamper
                    inventory.RemoveSelectedItem();
                    beingWashed = tool.prefab.GetComponent<Task>();
                    return true;
                }
                return false;
            // Can't interact while machine is running
            case WashingMachineState.Busy:
                return false;
            case WashingMachineState.Done:
                {
                    inventory.AddItem(cleanHamper);
                    wms = WashingMachineState.Empty;
                    if (beingWashed)
                    {
                        TaskManager.Instance.MarkComplete(beingWashed);
                        beingWashed = null;
                    }
                    return true;
                }
            // This shoulnd't ever happen
            default: return false;
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        HandleShake();

        if (wms == WashingMachineState.Busy)
        {
            if (started + washingTime < Time.time)
            {
                wms = WashingMachineState.Done;
                EmitParticles();
            }
        }
    }

    void HandleShake()
    {
        transform.position -= shakeShift;
        shakeShift = wms == WashingMachineState.Busy ? new Vector3(
            Mathf.Sin(Time.time * shakeFrequences.x + shakeOffsets.x) * shakeStrength,
            Mathf.Sin(Time.time * shakeFrequences.y + shakeOffsets.y) * shakeStrength,
            Mathf.Sin(Time.time * shakeFrequences.z + shakeOffsets.z) * shakeStrength
        ) : Vector3.zero;
        transform.position += shakeShift;
    }

    void EmitParticles()
    {
        if (doneParticles == null)
        {
            GameObject obj = Instantiate(particlesWhenDone);
            obj.transform.position = transform.position;
            doneParticles = obj.transform.GetChild(0).GetComponent<ParticleSystem>();
        }
        else
        {
            doneParticles.Play();
        }
    }

    protected override void ChoreDone()
    {
        if (wms == WashingMachineState.Empty)
        {
            wms = WashingMachineState.Busy;
            started = Time.time;
            interactionStartedAt = null;
        }
    }
}
