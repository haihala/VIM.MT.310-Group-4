using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    float activeoutlineThickness = 6;
    [SerializeField]
    Color activeoutlineColor = Color.yellow;

    [SerializeField]
    float inActiveoutlineThickness = 4;
    [SerializeField]
    Color inActiveoutlineColor = Color.white;
    Outline outline;

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.enabled = true;
        UnHighlight();
    }

    public virtual void Highlight()
    {
        outline.OutlineColor = activeoutlineColor;
        outline.OutlineWidth = activeoutlineThickness;
    }

    public virtual void UnHighlight()
    {
        outline.OutlineColor = inActiveoutlineColor;
        outline.OutlineWidth = inActiveoutlineThickness;
    }

    public abstract void OnInteract(GameObject player);
}
