using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("trig");
        print(other.name);
        print(other.tag);
        if (other.tag == "Door")
        {
            other.GetComponentInParent<Door>().Open(transform.position);
        }
    }
}
