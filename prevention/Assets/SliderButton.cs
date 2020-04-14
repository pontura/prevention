using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderButton : MonoBehaviour
{
    SliderManager sliderManager;
    Animation anim;
    public Scrollbar scrollBar;
    bool isOver;
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
            SetOver(true);

            lastValue = scrollBar.value;
            sliderManager.SetValue(lastValue);
        }
        else
        {
            SetOver(false);            
        }
    }
    void SetHelper(bool isOn)
    {
        if (isOn)
            Game.Instance.helperManager.Init(this.gameObject, sliderManager.helperText);
        else
            Game.Instance.helperManager.SetOff();
    }
    void SetOver(bool _isOver)
    {       
        if (isOver != _isOver)
        {
            if(_isOver)
                anim.Play("sliderOver");
            else
                anim.Play("sliderIdle");
        }
        isOver = _isOver;
    }
}
