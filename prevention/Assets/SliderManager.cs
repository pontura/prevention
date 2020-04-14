using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Animation anim;
    SlidersManager manager;
    bool isPlaying;
    public float totalTime;
    public string helperText;

    void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void Init(SlidersManager manager)
    {
        this.manager = manager;
        gameObject.SetActive(true);
        Game.Instance.helperManager.Init(this.gameObject, helperText);
    }
    float lastTimer;
    private void Update()
    {
        if(!isPlaying)
            return;
        manager.SetAnimationProgress(anim[anim.clip.name].time);
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
        if (manager == null)
            return;
        manager.SetPlaying(false);
        print("OnOut");
        anim["on"].speed = 0;
    }
    float scoreValue = 0;
    public int dir;
    public void SetValue(float v)
    {
        if((scoreValue == 0 && v>0) || (scoreValue == 1 && v < 1))
            Events.OnGamePartAnim(dir);
        if (dir == 0 && v == 1 )
        {
            dir = 1;
            manager.AddScore();            
        }
        else if (dir == 1 && v == 0)
        {
            dir = 0;
            manager.AddScore();
        }
        scoreValue = v;
        float value = v * totalTime;
        print(dir + " " + anim.clip.name + " value: " + value);
        anim[anim.clip.name].time = value;
        anim[anim.clip.name].speed = 0;
        anim.Play(anim.clip.name);
    }
}
