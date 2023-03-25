using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldItem))]
public class Pickup : Interactable
{
    WorldItem item;

    protected override void Start()
    {
        item = GetComponent<WorldItem>();
        base.Start();
    }

    public override void OnInteract(GameObject player)
    {
        player.GetComponent<Inventory>().AddItem(item.item);
        Destroy(gameObject);
    }
}