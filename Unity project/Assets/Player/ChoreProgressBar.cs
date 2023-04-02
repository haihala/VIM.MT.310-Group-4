using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoreProgressBar : MonoBehaviour
{
    [SerializeField]
    RectTransform pane;
    float maxWidth;

    public void SetProgress(float progress)
    {
        pane.sizeDelta = new Vector2(progress * maxWidth, pane.sizeDelta.y);
    }

    void Start()
    {
        maxWidth = GetComponent<RectTransform>().sizeDelta.x;
        SetProgress(0);
    }
}
