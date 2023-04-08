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

    public override bool OnInteract(GameObject player, InventoryItem selectedItem)
    {
        player.GetComponent<Inventory>().AddItem(item.item);
        Destroy(gameObject);
        return true;
    }
}
