using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScene : MonoBehaviour
{

    void Start()
    {
        Invoke("Delayed", 5);
    }

    void Delayed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }
}
