using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField]
    Light lightSource;

    public override bool OnInteract(GameObject player, InventoryItem item)
    {
        lightSource.enabled = !lightSource.enabled;
        return true;
    }
}
