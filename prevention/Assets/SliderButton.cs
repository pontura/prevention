using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderButton : MonoBehaviour
{
    SliderManager sliderManager;
    Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        sliderManager = gameObject.GetComponentInParent<SliderManager>();
       // OnOut();
    }
    public void OnOver()
    {
        anim.Play("sliderOver");
        sliderManager.OnOver();
    }
    public void OnOut()
    {
        anim.Play("sliderIdle");
        sliderManager.OnOut();
    }
}
