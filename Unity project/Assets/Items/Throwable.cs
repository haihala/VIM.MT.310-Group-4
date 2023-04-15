using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Throwable")]
public class Throwable : InventoryItem
{
    public float horizontalVelocity;
    public float verticalVelocity;

    public override void Use(GameObject player)
    {
        base.Use(player);
        player.GetComponent<Inventory>().ThrowSelectedItem(horizontalVelocity, verticalVelocity);
    }
}
