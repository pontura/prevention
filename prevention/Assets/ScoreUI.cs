using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameObject panel;
    public Image bar;
    bool isOn;
    float value;
    float lastValue;
    void Start()
    {
        Events.OnTimeInit += OnTimeInit;
        Events.OnGameProgress += OnGameProgress;
        Events.OnGameDone += OnGameDone;
        panel.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnTimeInit -= OnTimeInit;
        Events.OnGameProgress -= OnGameProgress;
        Events.OnGameDone -= OnGameDone;
    }
    void OnTimeInit(float a)
    {
        value = lastValue = bar.fillAmount = 1;
        isOn = true;
        panel.SetActive(true);
    }
    void OnGameDone()
    {
        bar.fillAmount = 1;
        isOn = false;
        Reset();
    }
    void OnGameProgress(float value)
    {
        print("OnGameProgress " + value);
        this.value = 1-value;
    }
    private void Update()
    {
        if (!isOn)
            return;
        float v = Mathf.Lerp(lastValue, value, 0.1f);
        lastValue = bar.fillAmount;
        bar.fillAmount = v;
    }
    void Reset()
    {
        panel.SetActive(false);
    }
}
