using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Animation anim;
    SlidersManager manager;
    bool isPlaying;

    void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void Init(SlidersManager manager)
    {
        this.manager = manager;
        gameObject.SetActive(true);
    }
    float lastTimer;
    private void Update()
    {
        if(!isPlaying)
            return;
        manager.SetAnimationProgress(anim["on"].time);
    }
    public void OnOver()
    {
        isPlaying = true;
        manager.SetPlaying(true);
        print("over");
        anim["on"].speed = 1;
        anim.Play();
    }
    public void OnOut()
    {
        isPlaying = false;
        manager.SetPlaying(false);
        print("OnOut");
        anim["on"].speed = 0;
    }
}
