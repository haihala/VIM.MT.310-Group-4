using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCueSystem : MonoBehaviour
{
    // Singleton boilerplate
    private static SoundCueSystem _instance;
    public static SoundCueSystem Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            this.guards = new SoundCue();
        }
    }

    public SoundCue guards;
}
