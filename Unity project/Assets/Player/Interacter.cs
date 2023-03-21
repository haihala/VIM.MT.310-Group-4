using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    [SerializeField]
    float range = 1;
    Interactable focusItem;

    void FixedUpdate()
    {
        Interactable newFocus = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Interactable item in GameObject.FindObjectsOfType<Interactable>())
        {
            float distance = (transform.position - item.gameObject.transform.position).magnitude;

            bool inRange = distance < range;
            bool closestObject = distance < shortestDistance;
            if (inRange && closestObject)
            {
                shortestDistance = distance;
                newFocus = item;
            }
        }


        if (newFocus != focusItem)
        {
            // Change in focus
            if (focusItem)
            {
                // If we had something focused, unfocus that
                focusItem.UnHighlight();
            }
            focusItem = newFocus;
            if (newFocus)
            {
                // If we have a new thing focused, focus on that
                // Could also be that the only focusable thing went out of range,
                // in which case we don't highlight anything
                focusItem.Highlight();
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (focusItem && value.ReadValue<float>() > 0)
        {
            focusItem.OnInteract(gameObject);
            focusItem = null;   // Fixes a bug on higher refresh rates
        }
    }
}
