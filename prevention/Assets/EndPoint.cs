using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    Animation anim;
    public int id;
    int num;
    void Start()
    {
        anim = GetComponent<Animation>();
        SetAnim();
        Events.SliderScore += SliderScore;
    }
    private void OnDestroy()
    {
        Events.SliderScore -= SliderScore;
    }
    void SliderScore()
    {
        num++;
        SetAnim();
    }
    void SetAnim()
    {
        if (num > 1)
            num = 0;

        if (num == id)
            anim.Play("end");
        else
            anim.Play("idle");
    }
}
