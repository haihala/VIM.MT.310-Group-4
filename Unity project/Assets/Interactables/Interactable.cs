using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    Outline outline;

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
    }

    public virtual void Highlight()
    {
        outline.enabled = true;
    }

    public virtual void UnHighlight()
    {
        outline.enabled = false;
    }

    public abstract void OnInteract(GameObject player);
}
