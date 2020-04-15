using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Animation anim;
    SlidersManager manager;
    bool isPlaying;
    public string helperText;
    public SliderButton sliderButton;

    public virtual void Start()
    {
        anim = GetComponent<Animation>();
    }
    public virtual void Init(SlidersManager manager)
    {
        this.manager = manager;
        gameObject.SetActive(true);
    }
    public void SetHelper(bool isOn)
    {
        if (isOn)
            Game.Instance.helperManager.Init(sliderButton.gameObject, helperText);
        else
            Game.Instance.helperManager.SetOff();
    }
}
