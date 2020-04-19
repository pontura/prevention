using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject ready;

    public void Init(bool isOn)
    {
        if (isOn)
            ready.SetActive(true);
        else
            ready.SetActive(false);
    }
}
