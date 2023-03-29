using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField]
    Light lightSource;

    public override void OnInteract(GameObject player)
    {
        lightSource.enabled = !lightSource.enabled;
    }
}
