using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public GameObject panel;
    public Image bar;
    bool isOn;
    bool isReadyToStart;
    float value;
    float totalTime;

    void Start()
    {
        bar.fillAmount = 1;
        Events.OnTimeInit += OnTimeInit;
        Events.OnGameDone += OnGameDone;
        Events.OnStartTimer += OnStartTimer;
        panel.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnTimeInit -= OnTimeInit;
        Events.OnGameDone -= OnGameDone;
        Events.OnStartTimer -= OnStartTimer;
    }
    void OnGameDone()
    {
        Reset();
    }
    void OnTimeInit(float _totalTime)
    {
        isReadyToStart = true;
        print("init time: " + _totalTime);
        panel.SetActive(true);
        value = 0;
        bar.fillAmount = 1;
        totalTime = _totalTime;
    }
    void OnStartTimer()
    {
        if(isReadyToStart)
            isOn = true;
    }
    private void Update()
    {
        if (!isOn)
            return;
        value += Time.deltaTime;
        if(value >= totalTime)
        {
            isOn = false;
            Events.OnTimeout();
            Reset();
            return;
        }
        bar.fillAmount = 1 - (value / totalTime);
    }
    private void Reset()
    {
        isReadyToStart = false;
        panel.SetActive(false);
        isOn = false;
    }
    public float GetValue()
    {
        return bar.fillAmount;
    }
}
