using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Equipment equipment;
    [SerializeField]
    List<WorldItem> displayItems;

    [SerializeField]
    Transform model;
    [SerializeField]
    float dropDistance = 0.5f;
    [SerializeField]
    List<InventoryItem> contents;
    [SerializeField]
    GameObject slotPrefab;
    [SerializeField]
    Transform inventoryBar;

    int? selectedIndex;  // Has an invalid value before the first item

    [SerializeField]
    [Tooltip("Unity likes to duplicate inputs, so for event actions, we should have a cooldown to de-duplicate")]
    float interactionCooldown;
    float lastScroll;
    int scrollDirection;
    float lastDrop;
    bool drop;

    void Start()
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

        if (selectedIndex == null)
        {
            SelectSlot(0);
        }
    }

    void SelectSlot(int index)
    {
        // Wrap around
        if (index < 0)
        {
            index = contents.Count - 1;
        }
        else if (index >= contents.Count)
        {
            index = 0;
        }
        selectedIndex = index;

        // When dropping an item, Unity despawns it after the update loop
        // This means if we highlight the item, we highlight the wrong item
        // To fix this, use indexOffset. 0 when the lengths match and 1 when we need to offset by one
        int indexOffset = inventoryBar.childCount - contents.Count;
        for (int i = 0; i < contents.Count; i++)
        {
            InventorySlot slot = inventoryBar.GetChild(i + indexOffset).GetComponent<InventorySlot>();

            if (i == index)
            {
                slot.Highlight();
            }
            else
            {
                slot.UnHighlight();
            }
        }
    }
    void FixedUpdate()
    {
        HandleScrolling();
        HandleDropping();
    }

    void HandleScrolling()
    {
        if (scrollDirection != 0 && contents.Count > 0 && lastScroll + interactionCooldown < Time.time)
        {
            SelectSlot((int)selectedIndex + scrollDirection);
            lastScroll = Time.time;
        }

        scrollDirection = 0;
    }

    void HandleDropping()
    {
        if (drop && contents.Count > 0 && lastDrop + interactionCooldown < Time.time)
        {
            GameObject obj = Instantiate(SelectedItem().prefab);
            obj.transform.position = transform.position + dropDistance * model.forward;

            RemoveSelectedItem();

            lastDrop = Time.time;
        }

        drop = false;
    }

    public InventoryItem SelectedItem()
    {
        if (selectedIndex != null)
        {
            return contents[(int)selectedIndex];
        }
        return null;
    }

    InventoryItem RemoveSelectedItem()
    {
        int index = (int)selectedIndex;
        InventoryItem item = contents[index];

        contents.RemoveAt(index);
        Destroy(inventoryBar.GetChild(index).gameObject);

        if (contents.Count > 0)
        {
            SelectSlot(index);
        }
        else
        {
            selectedIndex = null;
        }
        return item;
    }

    public void EquipSelected()
    {
        Equipment newEquip = (Equipment)RemoveSelectedItem();

        if (equipment != null)
        {
            AddItem(equipment);
        }
        equipment = newEquip;

        foreach (WorldItem displayItem in displayItems)
        {
            displayItem.gameObject.SetActive(equipment == displayItem.item);
        }
    }

    public void OnNextItem(InputAction.CallbackContext value)
    {
        scrollDirection = 1;
    }

    public void OnPreviousItem(InputAction.CallbackContext value)
    {
        scrollDirection = -1;
    }

    public void OnDropItem(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0)
        {
            drop = true;
        }
    }
}
