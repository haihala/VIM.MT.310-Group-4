using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    public ChoreProgressBar progressBar;
    [SerializeField]
    float range = 1;
    Interactable focusItem;
    Chore activeChore;

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
            print("Focus change");
            print(focusItem);
            print(newFocus);
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

        if (activeChore)
        {
            progressBar.SetProgress(activeChore.GetProgress());
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

    public bool Interacting()
    {
        return activeChore != null;
    }

    public void StartInteracting(Chore target)
    {
        activeChore = target;
        progressBar.SetProgress(0);
    }

    public void EndInteracting()
    {
        activeChore = null;
        progressBar.SetProgress(0);
    }

}
