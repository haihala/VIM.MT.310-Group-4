using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> contents;
    public GameObject slotPrefab;
    public Transform inventoryBar;


    void Awake()
    {
        contents = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item)
    {
        contents.Add(item);

        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(inventoryBar, false);
        InventorySlot slot = obj.GetComponent<InventorySlot>();
        slot.Set(item);
    }
}
