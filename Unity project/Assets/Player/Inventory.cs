using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public float pickupRange;
    public List<InventoryItem> contents;
    public GameObject slotPrefab;
    public Transform inventoryBar;

    WorldItem focusItem;

    void Awake()
    {
        contents = new List<InventoryItem>();
    }

    void Update()
    {
        float shortestDistance = pickupRange;
        WorldItem newFocus = null;

        foreach (WorldItem item in GameObject.FindObjectsOfType<WorldItem>())
        {
            float distance = (transform.position - item.gameObject.transform.position).magnitude;
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                newFocus = item;
            }
        }

        if (newFocus)
        {
            // Item is within reach
            focusItem = newFocus;
        }
    }

    public void OnPickup(InputAction.CallbackContext value)
    {
        if (focusItem)
        {
            contents.Add(focusItem.item);

            GameObject obj = Instantiate(slotPrefab);
            obj.transform.SetParent(inventoryBar, false);
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slot.Set(focusItem.item);

            Destroy(focusItem.gameObject);
            focusItem = null;
        }
    }
}
