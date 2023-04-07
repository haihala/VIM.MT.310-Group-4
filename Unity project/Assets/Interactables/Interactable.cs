using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    public float extraInteractionRange;

    // For outlines
    [SerializeField]
    float activeoutlineThickness = 6;
    [SerializeField]
    float inActiveoutlineThickness = 4;
    [SerializeField]
    Color activeoutlineColor = Color.yellow;
    [SerializeField]
    Color inActiveoutlineColor = Color.white;

    // For decals
    [SerializeField]
    Material inactiveMaterial;
    [SerializeField]
    Material activeMaterial;

    Outline outline;
    DecalProjector decalProjector;
    bool highlighted;

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }

        decalProjector = GetComponent<DecalProjector>();
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
            if (outline)
            {
                outline.OutlineColor = activeoutlineColor;
                outline.OutlineWidth = activeoutlineThickness;
                outline.OutlineMode = Outline.Mode.OutlineAll;
            }

            if (decalProjector)
            {
                decalProjector.material = activeMaterial;
            }
        }
        else
        {
            if (outline)
            {
                outline.OutlineColor = inActiveoutlineColor;
                outline.OutlineWidth = inActiveoutlineThickness;
                outline.OutlineMode = Outline.Mode.OutlineVisible;
            }

            if (decalProjector)
            {
                decalProjector.material = inactiveMaterial;
            }
        }
    }

    public abstract void OnInteract(GameObject player, InventoryItem item);
}
