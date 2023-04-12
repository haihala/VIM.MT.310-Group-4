using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int points = 50;

    void Start()
    {
        TaskManager.Instance.Register(this);
    }
}
