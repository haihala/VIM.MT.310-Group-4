using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        bool collidingWithPlayer = other.gameObject.layer == 3;
        if (collidingWithPlayer)
        {
            BackgroundMusic.Instance.Change(clip);
        }
    }
}
