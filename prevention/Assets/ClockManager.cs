using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    float s;
    bool timeRunning;
    public Text field;
    public GameObject clockGO;

    void Start()
    {
        clockGO.SetActive(false);
        Events.OnTimeInit += OnTimeInit;
        Events.OnGameDone += OnGameDone;
        Events.OnTimeout += OnTimeout;
    }
    private void OnDestroy()
    {
        Events.OnTimeInit -= OnTimeInit;
        Events.OnGameDone -= OnGameDone;
        Events.OnTimeout -= OnTimeout;
    }
    void OnTimeout()
    {
        OnGameDone();
    }
    void OnGameDone()
    {
        timeRunning = false;
        clockGO.SetActive(false);
    }
    void OnTimeInit(float t)
    {
        clockGO.SetActive(true);
        timeRunning = true;
    }
    void Update()
    {
        if (timeRunning)
            s += Time.deltaTime;
        field.text = SetFormated(s);
    }
    public string SetFormated(float s)
    {
        string text = "";
        int sec = (int)s;
        if (sec < 10)
            text = "00:0" + sec;
        else if (sec < 60)
            text = "00:" + sec;
        else
        {
            int min = (int)(sec / 60);
            sec -= (min * 60);
            if (sec < 10)
                text = "0" + min + ":0" + sec;
            else
                text = "0" + min + ":" + sec;

        }

        return text;
    }
}
