using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderButton : MonoBehaviour
{
    SliderManager sliderManager;
    Animation anim;
    public Scrollbar scrollBar;

    void Start()
    {
        anim = GetComponent<Animation>();
        sliderManager = gameObject.GetComponentInParent<SliderManager>();
    }
    public void OnOver()
    {
        anim.Play("sliderOver");
        sliderManager.OnOver();
        SetHelper(false);
    }
    public void OnOut()
    {
        anim.Play("sliderIdle");
        sliderManager.OnOut();
        SetHelper(true);
    }
    float lastValue = 10000;
    void Update()
    {
        if (scrollBar == null)
            return;
        if (scrollBar.value != lastValue)
        {
            SetHelper(false);
            anim.Play("sliderOver");
            lastValue = scrollBar.value;
            sliderManager.SetValue(lastValue);
        }
        else
        {
            anim.Play("sliderIdle");
        }
    }
    void SetHelper(bool isOn)
    {
        if (isOn)
            Game.Instance.helperManager.Init(this.gameObject, sliderManager.helperText);
        else
            Game.Instance.helperManager.SetOff();
    }
}
