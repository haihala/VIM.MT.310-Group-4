using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    int seconds = 300;
    TextMeshProUGUI text;
    SceneChanger sceneChanger;
    int target;

    void Start()
    {
        target = (int)Time.time + seconds;
        text = GetComponent<TextMeshProUGUI>();
        sceneChanger = GetComponent<SceneChanger>();
    }

    void Update()
    {
        int value = target - (int)Time.time;
        text.text = value.ToString();
        if (value <= 0)
        {
            sceneChanger.Go();
        }
    }
}
