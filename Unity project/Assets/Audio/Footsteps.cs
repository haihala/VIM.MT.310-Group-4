using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField]
    float audibleVolumeMultiplier;
    [SerializeField]
    float soundCueVolumeMultiplier;
    [SerializeField]
    float crouchDampening = 0.5f;
    [SerializeField]
    List<AudioClip> clips;

    [SerializeField]
    float strideLength;
    float buildup;

    Vector3 positionLastFrame;
    AudioSource player;
    Croucher croucher;
    PlayerMover playerMover;
    NavMeshAgent agent;

    void Start()
    {
        player = GetComponent<AudioSource>();
        croucher = GetComponent<Croucher>();
        playerMover = GetComponent<PlayerMover>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float velocity = GetVelocity();
        if (velocity > 0.1)
        {
            if (buildup <= 0)
            {
                MakeStep(velocity);
            }

            buildup -= velocity * Time.deltaTime;
        }
        else
        {
            buildup = 0;
        }
    }


    float GetVelocity()
    {
        if (playerMover != null)
        {
            return playerMover.horizontalVelocity.magnitude;
        }
        else if (agent != null)
        {
            return agent.desiredVelocity.magnitude;
        }
        else
        {
            float vel = (transform.position - positionLastFrame).magnitude / Time.deltaTime;
            positionLastFrame = transform.position;
            return vel;
        }
    }


    void MakeStep(float velocity)
    {
        float volume = velocity * ((croucher != null && croucher.crouching) ? crouchDampening : 1);
        SoundCueSystem.Instance.Invoke(transform.position, volume * soundCueVolumeMultiplier);
        int index = Random.Range(0, clips.Count);
        player.PlayOneShot(clips[index], volume * audibleVolumeMultiplier);
        buildup = strideLength;
    }
}
