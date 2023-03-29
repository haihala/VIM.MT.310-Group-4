using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBaseScene : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("Base", LoadSceneMode.Additive);
        Destroy(gameObject);
    }
}
