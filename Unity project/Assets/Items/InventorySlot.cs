using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    Image background;

    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshProUGUI label;

    public void Set(InventoryItem item)
    {
        icon.sprite = item.icon;
        label.text = item.displayName;
        UnHighlight();
    }

    // TODO: How should highlighted slots work?
    public void Highlight()
    {
        background.color = new Color(0, 0, 0, 0.8f);
    }

    public void UnHighlight()
    {
        background.color = new Color(0, 0, 0, 0.4f);
    }
}
