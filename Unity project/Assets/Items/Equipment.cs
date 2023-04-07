using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment")]
public class Equipment : InventoryItem
{
    public override void Use(GameObject player)
    {
        base.Use(player);
        player.GetComponent<Inventory>().EquipSelected();
    }
}
