using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        GetComponent<Collider>().enabled = false;
    }

    public void Activate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        GetComponent<Collider>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        bool collidingWithPlayer = other.gameObject.layer == 3;
        if (collidingWithPlayer)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
