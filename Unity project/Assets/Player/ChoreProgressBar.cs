using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoreProgressBar : MonoBehaviour
{
    [SerializeField]
    RectTransform pane;

    public void SetProgress(float progress)
    {
        float width = progress * GetComponent<RectTransform>().sizeDelta.x;
        pane.sizeDelta = new Vector2(width, pane.sizeDelta.y);
    }

    void Start()
    {
        SetProgress(0);
    }

    void Update()
    {
        transform.LookAt(GetComponent<Canvas>().worldCamera.transform);
    }
}
