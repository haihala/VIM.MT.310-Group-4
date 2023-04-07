using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;

    // Called when using without target
    virtual public void Use(GameObject player)
    {
        Console.WriteLine("Using {0}", displayName);
    }
}
