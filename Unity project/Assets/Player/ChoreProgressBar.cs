using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoreProgressBar : MonoBehaviour
{
    Slider slider;

    public void SetProgress(float progress)
    {
        if (progress == 0)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
            slider.value = progress;
        }
    }

    void Start()
    {
        slider = GetComponent<Slider>();
        SetProgress(0);
    }
}
