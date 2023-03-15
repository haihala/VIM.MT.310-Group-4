using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshProUGUI label;

    public void Set(InventoryItem item)
    {
        icon.sprite = item.icon;
        label.text = item.displayName;
    }
}
