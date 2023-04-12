using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string scene;


    public void Go()
    {
        SceneManager.LoadScene(scene);
    }
}
