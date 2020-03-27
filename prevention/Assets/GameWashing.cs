using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWashing : MonoBehaviour
{
    Animator anim;

    public int id;
    public states state;
    public enum states
    {
        INTRO,
        GAME1,
        GAME2,
        VERTICAL1_INTRO,
        VERTICAL1,
        VERTICAL2_INTRO,
        VERTICAL2,
        CIRCULOS_INTRO,
        CIRCULOS,
        PULGAR1_INTRO,
        PULGAR1,
        PULGAR2_INTRO,
        PULGAR2,
        PUNIO1_INTRO,
        PUNIO1,
        PUNIO2_INTRO,
        PUNIO2,
        OUTRO
    }
    void Start()
    {
        anim = GetComponent<Animator>();        
        Events.ItemsListDestroyerDone += ItemsListDestroyerDone;
        Events.OnGestureVertical += OnGestureVertical;
        Events.OnGestureHorizontal += OnGestureHorizontal;
        ChangeState();
    }
    void ChangeState()
    {       
        state = (states)id;
        switch (id)
        {
            case 0:
                anim.Play("intro"); 
                Invoke("ChangeState", 4);
                break;
            case 1:
                anim.Play("handwash1_idle");
                break;
            case 2:
                anim.Play("handwash1_rotation");                 
                break;
            case 3:
                Events.OnDrag(false, "soap");
                anim.Play("handwash2_intro");
                Invoke("ChangeState", 2);
                break;
            case 4:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            case 5:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
                anim.Play("handwash2_transition1");
                Invoke("ChangeState", 1.6f);
                break;
            case 6:                
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            case 7:
                anim.Play("handwash3_intro");
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
                break;
            case 8:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            case 9:
                anim.Play("handwash4_intro");
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
                break;
            case 10:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            case 11:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
                anim.Play("handwash4_transition1");
                Invoke("ChangeState", 1.6f);
                break;
            case 12:
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            case 13:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
                anim.Play("handwash5_intro");
                Invoke("ChangeState", 1.6f);
                break;
            case 14:
                Events.OnGestureActive(GesturesManager.types.SLIDE_HORIZONTAL, true);
                break;
            case 15:
                SetTotalValues(12);
                Events.OnGestureActive(GesturesManager.types.SLIDE_HORIZONTAL, false);
                anim.Play("handwash5_transition1");
                Invoke("ChangeState", 1);
                break;
            case 16:
                Events.OnGestureActive(GesturesManager.types.SLIDE_HORIZONTAL, true);
                break;
            case 17:
                Events.OnGestureActive(GesturesManager.types.SLIDE_HORIZONTAL, false);
                anim.Play("outro");
                Invoke("ChangeState", 1);
                break;
            case 18:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
                break;
        }
        id++;
    }
    void SetTotalValues(int _total)
    {
        totalSteps = _total;
        step = 0;
    }
    int itemsDone = 0;
    void ItemsListDestroyerDone(ItemsListDestroyer i)
    {
        itemsDone++;
        if (itemsDone >= 2)
        {
            itemsDone = 0;
            ChangeState();
        }           
    }
    public int totalSteps;
    public int step;
    void OnGestureVertical(GesturesManager.verticalTypes type)
    {
        step++;
        if (state == states.VERTICAL1)
        {
            
            if (type == GesturesManager.verticalTypes.UP)
                anim.Play("handwash2_step1_a");
            else if (type == GesturesManager.verticalTypes.DOWN)
                anim.Play("handwash2_step1_b");
        }
        else if (state == states.VERTICAL2)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.Play("handwash2_step2_a");
            else if (type == GesturesManager.verticalTypes.DOWN)
                anim.Play("handwash2_step2_b");
        }
        else if (state == states.CIRCULOS)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.Play("handwash3_a");
            else if (type == GesturesManager.verticalTypes.DOWN)
                anim.Play("handwash3_b");
        }
        else if (state == states.PULGAR1)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.CrossFade("handwash4_step1", 0.2f, 0, 0);
        }
        else if (state == states.PULGAR2)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.CrossFade("handwash4_step2", 0.2f, 0,  0);
        }
        if (step > totalSteps)
            ChangeState();
    }
    void OnGestureHorizontal(GesturesManager.horizontalTypes type)
    {
        step++;
        if (state == states.PUNIO1)
        {
            if (type == GesturesManager.horizontalTypes.RIGHT)
                anim.CrossFade("handwash5_step1", 0.2f, -1, 0);
        }
        else if (state == states.PUNIO2)
        {
            if (type == GesturesManager.horizontalTypes.RIGHT)
                anim.CrossFade("handwash5_step2", 0.2f, -1, 0);
        }
        if (step > totalSteps)
            ChangeState();
    }
}
