using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public static BackgroundMusic Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            player = GetComponent<AudioSource>();
            baseVolume = player.volume;
        }
    }
    float baseVolume;

    [SerializeField]
    float fadeTime;
    AudioSource player;
    AudioClip next;
    float lastSwitch = -Mathf.Infinity;


    // Update is called once per frame
    void Update()
    {
        float timeSinceLastSwitch = Time.time - lastSwitch;

        if (timeSinceLastSwitch <= fadeTime)
        {
            // Ramp down
            player.volume = baseVolume * (1 - timeSinceLastSwitch / fadeTime);
        }
        else if (timeSinceLastSwitch <= 2 * fadeTime)
        {
            // Change clip in silence
            player.volume = 0;
            if (next != null)
            {
                player.clip = next;
                player.Play();
                next = null;
            }
        }
        else if (timeSinceLastSwitch <= 3 * fadeTime)
        {
            // Ramp up
            player.volume = baseVolume * (timeSinceLastSwitch / fadeTime - 2);
        }
        else
        {
            player.volume = baseVolume;
        }
    }

    public void Change(AudioClip clip)
    {
        if (clip != player.clip)
        {
            next = clip;
            lastSwitch = Time.time;
        }
    }
}
