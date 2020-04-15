using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public GameObject enter;
    public GameObject anim;

    void Start()
    {
       anim.SetActive(false);
    }

    public void OnClicked()
    {
        anim.SetActive(true);
        enter.SetActive(false);
        Invoke("GotoGame", 4);
    }
    void GotoGame()
    {
        Data.Instance.LoadScene("Game");
    }
}
