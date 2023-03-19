using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCueSystem : MonoBehaviour
{
    private static SoundCueSystem _instance;
    public static SoundCue Instance { get { return _instance.soundEvents; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            this.soundEvents = new SoundCue();
        }
    }

    public SoundCue soundEvents;
}
