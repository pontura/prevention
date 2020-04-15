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
        Events.OnStep += OnStep;
    }
    private void OnDestroy()
    {
        Events.OnStep -= OnStep;
    }
    void OnStep()
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
