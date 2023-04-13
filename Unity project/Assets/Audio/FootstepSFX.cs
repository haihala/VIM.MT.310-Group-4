using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSFX : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> clips;
    public float interval;

    AudioSource player;
    Vector3 positionLastFrame;
    float buildup;

    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool moving = (transform.position - positionLastFrame).magnitude > 0.1 * Time.deltaTime;
        positionLastFrame = transform.position;

        if (moving)
        {
            buildup += Time.deltaTime;

            if (buildup > interval)
            {
                // Play
                int index = Random.Range(0, clips.Count);
                player.clip = clips[index];
                player.Play();
                buildup = 0;
            }
        }
        else
        {
            print("not moving");
            buildup = 0;
        }
    }
}
