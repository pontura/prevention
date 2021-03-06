﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    Animation anim;
    public int id;
    int num;
    SlliderPointByPoint slider;
    public GameObject directionAsset;
    public Image buttonImage;

    void Start()
    {
        Events.OnSliderChangeDirection += OnSliderChangeDirection;
    }
    void OnDestroy()
    {
        Events.OnSliderChangeDirection -= OnSliderChangeDirection;
    }
    void OnSliderChangeDirection(bool up)
    {
        if (up)
            directionAsset.transform.localEulerAngles = new Vector3(0, 0, 0);
        else
            directionAsset.transform.localEulerAngles = new Vector3(0, 0, 180);
    }
    
    public void Init(SlliderPointByPoint slider, int id)
    {        
        this.id = id;
        this.slider = slider;
        anim = GetComponent<Animation>();
        SetState(false);

    }
    public void SetOver()
    {
        Events.OnStartTimer();
        if (slider != null)
            slider.SetOver(this);
    }
    public void SetState(bool isActive)
    {
        buttonImage.raycastTarget = isActive;
        if (isActive)
            anim.Play("end");
        else
            anim.Play("idle");
    }




    //void Start()
    //{
    //    anim = GetComponent<Animation>();
    //    SetAnim();
    //    Events.OnStep += OnStep;
    //}
    //private void OnDestroy()
    //{
    //    Events.OnStep -= OnStep;
    //}
    //void OnStep()
    //{
    //    num++;
    //    SetAnim();
    //}
    //void SetAnim()
    //{
    //    if (num > 1)
    //        num = 0;

    //    if (num == id)
    //        anim.Play("end");
    //    else
    //        anim.Play("idle");
    //}


}
