using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderButton : MonoBehaviour
{
    SliderManager sliderManager;
    Animation anim;
    bool isOn;

    void Start()
    {
        anim = GetComponent<Animation>();
        sliderManager = gameObject.GetComponentInParent<SliderManager>();
    }    
    public void SetOn(bool _isOn)
    {
        anim[anim.clip.name].normalizedTime = 0;
        if (_isOn)
            anim.Play("sliderOver");
        else
            anim.Play("sliderIdle");
        

    }
}
