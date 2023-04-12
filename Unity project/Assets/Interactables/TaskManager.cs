using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    private static TaskManager _instance;
    public static TaskManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            slider.value = 0;
        }
    }

    [SerializeField]
    Slider slider;

    // These are for debugging and not meant to be assigned to
    [SerializeField]
    List<Task> registered;

    [SerializeField]
    List<Task> complete;

    public void Register(Task task)
    {
        registered.Add(task);
    }

    public void MarkComplete(Task task)
    {
        // Because of the washing machine, this is really tricky operation.
        // The things in registered list aren't stricly the same, so just play it a bit fuzzy.
        complete.Add(task);
        slider.value = CompletionPercentage;
    }

    int RegisteredPoints
    {
        get
        {
            int total = 0;
            foreach (Task task in registered)
            {
                total += task.points;
            }
            return total;
        }
    }

    int CompletedPoints
    {
        get
        {
            int total = 0;
            foreach (Task task in complete)
            {
                total += task.points;
            }
            return total;
        }
    }

    public float CompletionPercentage
    {
        get
        {
            return (float)CompletedPoints / (float)RegisteredPoints;
        }
    }
}
