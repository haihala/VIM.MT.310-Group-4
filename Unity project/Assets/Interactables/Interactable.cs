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
    float activeOutlineThickness = 6;
    [SerializeField]
    float inactiveOutlineThickness = 4;


    // For decals
    [SerializeField]
    float activeMaterialThickness = 0.2f;
    [SerializeField]
    float inActiveMaterialThickness = 0.1f;

    // For both
    [SerializeField]
    Color activeoutlineColor = Color.yellow;
    [SerializeField]
    Color inActiveoutlineColor = Color.white;

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
                outline.OutlineWidth = activeOutlineThickness;
                outline.OutlineMode = Outline.Mode.OutlineAll;
            }

            if (decalProjector)
            {
                decalProjector.material.SetColor("_OutlineColor", activeoutlineColor);
                decalProjector.material.SetFloat("_OutlineThickness", activeMaterialThickness);
            }
        }
        else
        {
            if (outline)
            {
                outline.OutlineColor = inActiveoutlineColor;
                outline.OutlineWidth = inactiveOutlineThickness;
                outline.OutlineMode = Outline.Mode.OutlineVisible;
            }

            if (decalProjector)
            {
                decalProjector.material.SetColor("_OutlineColor", inActiveoutlineColor);
                decalProjector.material.SetFloat("_OutlineThickness", inActiveMaterialThickness);
            }
        }
    }

    public abstract void OnInteract(GameObject player, InventoryItem item);
}
