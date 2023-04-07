using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    public float extraInteractionRange;
    [SerializeField]
    float activeoutlineThickness = 6;
    [SerializeField]
    Color activeoutlineColor = Color.yellow;

    [SerializeField]
    float inActiveoutlineThickness = 4;
    [SerializeField]
    Color inActiveoutlineColor = Color.white;
    Outline outline;
    bool highlighted;

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.enabled = true;
    }

    public void Highlight()
    {
        highlighted = true;
    }

    public void UnHighlight()
    {
        highlighted = false;
    }

    void Update()
    {
        if (highlighted)
        {
            outline.OutlineColor = activeoutlineColor;
            outline.OutlineWidth = activeoutlineThickness;
        }
        else
        {
            outline.OutlineColor = inActiveoutlineColor;
            outline.OutlineWidth = inActiveoutlineThickness;
        }
    }

    public abstract void OnInteract(GameObject player, InventoryItem item);
}
