﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    static Game mInstance = null;
    public DraggerManager draggerManager;
    public SlidersManager slidersManager;

    public static Game Instance
    {
        get
        {
            if (mInstance == null)
            {
                Debug.LogError("Algo llama a Game antes de inicializarse");
            }
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Done");
    }
}
