using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public GameObject panel;
    public Image bar;
    bool isOn;
    float value;
    float totalTime;

    void Start()
    {
        Events.OnTimeInit += OnTimeInit;
        Events.OnGameDone += OnGameDone;
        panel.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnTimeInit -= OnTimeInit;
        Events.OnGameDone -= OnGameDone;
    }
    void OnGameDone()
    {
        Reset();
    }
    void OnTimeInit(float _totalTime)
    {
        print("init time: " + _totalTime);
        panel.SetActive(true);
        value = 0;
        isOn = true;
        totalTime = _totalTime;
    }
    private void Update()
    {
        if (!isOn)
            return;
        value += Time.deltaTime;
        if(value >= totalTime)
        {
            Events.OnTimeout();
            Reset();
            return;
        }
        bar.fillAmount = 1 - (value / totalTime);
    }
    private void Reset()
    {
        panel.SetActive(false);
        isOn = false;
    }
    public float GetValue()
    {
        return bar.fillAmount;
    }
}
