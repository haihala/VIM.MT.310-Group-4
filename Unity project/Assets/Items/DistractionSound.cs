using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionSound : MonoBehaviour
{
    public float volume;
    void OnCollisionEnter(Collision collision)
    {
        SoundCueSystem.Instance.Invoke(transform.position, GetComponent<Rigidbody>().velocity.magnitude * volume);
    }
}
